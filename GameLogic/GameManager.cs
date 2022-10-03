using BlackJack.BicycleCards;
using BlackJack.Participant;
using BlackJack.Printing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack.GameLogic
{
    public class GameManager
    {
        GamePhase currentPhase { get; set; }
        Deck deck;

        Player player;
        Dealer dealer;

        private int bettingPool = 0;

        private static readonly double _drawDelayInSeconds = 1.5;
        public static int drawDelay => (int)_drawDelayInSeconds * 1000; //Milliseconds

        private delegate bool InputDelegate();

        public GameManager() {
            currentPhase = GamePhase.BET;
            //DECK
            deck = new Deck();
            deck.ShuffleDeck();
            //BASEPLAYERS
            dealer = new Dealer();
            player = new Player();
        }

        public void PhaseHandler() {
            //Re-initialize round variables
            while (true) {
                UIBaseDrawer.ResetDrawableArea();
                UIMoneyDrawer.DrawPlayerBalance(player.getBalance, 0);
                HandlePhaseBet();
                HandlePhaseShuffle();
                if (HandlePhaseDeal(out BasePlayer bPlayerBlackJack)) {
                    UIBaseDrawer.ResetDrawableArea();
                    Console.WriteLine(bPlayerBlackJack.ToString() + " has blackjack");
                }
                if (!HandlePhasePlay(out bool playerBusted))
                    return;
                if (!playerBusted) {
                    DealerPlayLoop();
                    HandlePhaseSettlement();

                }

                if (player.wallet.IsBroke)
                    OnPlayerBroke();

                PrepareNewRound();
            }
        }





        #region Bet Phase
        private void HandlePhaseBet() {
            UIBetDrawer.StartBetPhase();
            HandlePlayerBet();
        }

        private void HandlePlayerBet() {
            while (true) {

                string userInput = "";

                //Attempt to read user input.
                while (String.IsNullOrEmpty(userInput)) {
                    userInput = Console.ReadLine();
                }

                //Check if input is valid.
                if (int.TryParse(userInput, out int betAmount)) {
                    if (betAmount <= 0) {
                        continue;
                    }
                    bettingPool = player.wallet.AttemptBet(betAmount);

                    UIMoneyDrawer.DrawPlayerBalance(player.getBalance, bettingPool);

                    UIBaseDrawer.ResetDrawableArea();
                    return;
                }
                else {
                    UIBetDrawer.RepeatBetPhase();
                }
            }
        }
        #endregion

        #region Shuffle Phase
        private void HandlePhaseShuffle() {
            if (deck.isReshuffleNeeded) {
                ConsoleWriter.OnReshuffle();
                deck.ReshuffleDeck();
            }
            deck.ShuffleDeck();
        }
        #endregion

        #region Deal Phase
        /// <summary>
        /// Deals <see cref="Card"/>s from <see cref="GameManager.deck"/> 
        /// </summary>
        private bool HandlePhaseDeal(out BasePlayer basePlayerWithBlackJack) {
            //ConsoleWriter.Writeline("====DEAL====");
            int startingHandSize = 2;

            for (int i = 0; i < startingHandSize; i++) {
                //Player gets card
                player.hand.AddCard(deck.DrawCard());
                UICardDrawer.DrawHand(player);

                Card dealerCard = deck.DrawCard();

                if (dealer.hand.handSize == 0) {
                    dealerCard = dealerCard.SetFaceDown();
                }
                dealer.hand.AddCard(dealerCard);
                UICardDrawer.DrawHand(dealer);
            }

            if (player.hasBlackjack)
                basePlayerWithBlackJack = player;
            else 
                basePlayerWithBlackJack = dealer;

            return player.hasBlackjack || dealer.hasBlackjack;
        }

        #endregion

        #region Play Phase
        //Returns true is game continues; False is game stops.
        private bool HandlePhasePlay(out bool playerBusted) {
            UIBaseDrawer.CursorToDrawableArea();
            if (player.hasBlackjack) {
                Console.WriteLine("YOU HAVE BLACK JACK");
                playerBusted = false;
                return true;
            }
            
            ConsoleWriter.Writeline("=====THE PLAY=====");

            string[] msgs = new string[] {
                "WHAT DO YOU WANT TO DO"
            };

            while (true) {
                BlackJackAction action = HandlePlayerPlay();


                switch (action) {

                    case BlackJackAction.STAND:
                        //END CURRENT PHASE
                        playerBusted = false;
                        return true;
                    case BlackJackAction.HIT:
                        player.hand.AddCard(deck.DrawCard());
                        //ConsoleWriter.WritePlayerHand(player.ToString(), player.hand.getTotalHandValue, player.getHand);
                        //UIMoneyDrawer.DrawPlayerHand(player.hand.getCardsOnHand);
                        UICardDrawer.DrawHand(player);
                        if (player.isBust) {
                            ConsoleWriter.Writeline("You're a buster");
                            playerBusted = true;
                            return true;
                        }
                        continue;
                    default:
                        //END GAME
                        throw new Exception("GameManager.HandlePhasePlay() -> Went to Default");
                }
            }
        }

        private BlackJackAction HandlePlayerPlay() {
            UIBaseDrawer.CursorToDrawableArea();
            List<BlackJackAction> actions = player.GetPlayerActions;

            for (int i = 0; i < actions.Count; i++) {
                ConsoleWriter.WriteActionToPlayer($"{i + 1}) - {actions[i]}");
            }

            while (true) {
                var initialInput = Console.ReadKey(true).KeyChar;
                if (int.TryParse(initialInput.ToString(), out int userInput)) {
                    if (userInput > 0 && userInput <= actions.Count) {
                        return actions[userInput - 1];
                    }
                }
            }
        }

        public bool DealerPlayLoop() {
            //Dealer reveals the facedown card
            dealer.hand.TurnCardFaceUp();

            //ConsoleWriter.WriteCard(dealer.getHand);
            UICardDrawer.DrawHand(dealer);
            while (dealer.canHit) {
                dealer.Hit(deck.DrawCard());
                UICardDrawer.DrawHand(dealer);
            }
            return dealer.isBust;
        }


        #endregion

        #region Settlement Phase

        private void HandlePhaseSettlement() {
            if (player.hand > dealer.hand || dealer.isBust) {
                int payout = 2;
                player.wallet.AddAmount(bettingPool * payout);
                //UIMoneyDrawer.UpdateBalance(player.getBalance);
            }
            UIBaseDrawer.ResetDrawableArea();
            UIMoneyDrawer.DrawPlayerBalance(player.getBalance);
            UICardDrawer.ResetCardDrawer();
        }



        #endregion

        #region Reset Phase

        private void PrepareNewRound() {
            //Empty player hands
            player.EmptyHand();
            dealer.EmptyHand();
            //Reset bettingPool
            bettingPool = 0;

            //UIHandler.UpdateBalance(player.getBalance);

            ResetConsole();
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey(true);

            //ResetConsole();
            ////Clear Console
            //ConsoleWriter.Clear();
        }

        #endregion

        #region Player's dipped in tar and feathers and thrown out of the Casino Phase

        private void OnPlayerBroke() {
            ConsoleWriter.OnBlackJackExit();
            Environment.Exit(0);
        }

        #endregion


        private void ResetConsole() {
            UIMoneyDrawer.DrawPlayerBalance(player.getBalance, bettingPool);
            UICardDrawer.ResetCardDrawer();
            UIBaseDrawer.ResetDrawableArea();

            //UIBaseDrawer.CursorToDrawableArea();
        }
    }
}
