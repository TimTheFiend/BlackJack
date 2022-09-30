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

                if (!HandlePhaseBet())
                    return;
                HandlePhaseShuffle();
                HandlePhaseDeal();
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
        private bool HandlePhaseBet() {
            string[] msgs = new string[] {
                $"PLAYER BALANCE:\t{player.getBalanceString}"
            };


            ConsoleWriter.Writeline($"(Balance: {player.wallet.balance})\nNOTE: If your bet exceeds current balance, it'll be considered as an all-in.");
            return HandlePlayerBet();
        }

        private bool HandlePlayerBet() {
            while (true) {
                ConsoleWriter.Writeline("How much do you want to bet?");

                string userInput = "";

                while (String.IsNullOrEmpty(userInput)) {
                    userInput = Console.ReadLine();
                }


                if (String.IsNullOrEmpty(userInput)) {
                    continue;
                }
                if (int.TryParse(userInput, out int betAmount)) {
                    if (betAmount <= 0) {
                        continue;
                    }
                    bettingPool = player.wallet.AttemptBet(betAmount);
                    ConsoleWriter.Writeline($"=====BET ACCEPTED=====\nCurrent balance:\t${player.wallet.balance}\nCurrent bet:\t\t${bettingPool}");
                    return true;
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
        private void HandlePhaseDeal() {
            ConsoleWriter.Writeline("====DEAL====");
            int startingHandSize = 2;

            for (int i = 0; i < startingHandSize; i++) {
                //Player gets card
                player.hand.AddCard(deck.DrawCard());
                ConsoleWriter.WritePlayerHand(player.ToString(), player.hand.getTotalHandValue, player.getHand);

                Card dealerCard = deck.DrawCard();

                if (dealer.hand.handSize == 0) {
                    dealerCard = dealerCard.SetFaceDown();
                }
                dealer.hand.AddCard(dealerCard);
                ConsoleWriter.WritePlayerHand(dealer.ToString(), dealer.hand.getTotalHandValue, dealer.getHand);
            }
        }
        #endregion

        #region Play Phase
        //Returns true is game continues; False is game stops.
        private bool HandlePhasePlay(out bool playerBusted) {
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
                        ConsoleWriter.WritePlayerHand(player.ToString(), player.hand.getTotalHandValue, player.getHand);
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
            BlackJackAction[] actions = new BlackJackAction[] {
                BlackJackAction.STAND,
                BlackJackAction.HIT
            };

            for (int i = 0; i < actions.Length; i++) {
                ConsoleWriter.WriteActionToPlayer($"{i + 1}) - {actions[i]}");
            }

            while (true) {
                var initialInput = Console.ReadKey(true).KeyChar;
                if (int.TryParse(initialInput.ToString(), out int userInput)) {
                    if (userInput > 0 && userInput <= actions.Length) {
                        return actions[userInput - 1];
                    }
                }
            }
        }

        public bool DealerPlayLoop() {
            //Dealer reveals the facedown card
            dealer.hand.TurnCardFaceUp();
            ConsoleWriter.Write("DEALER REVEAL:");
            ConsoleWriter.WriteCard(dealer.getHand);
            while (dealer.canHit) {
                dealer.Hit(deck.DrawCard());
                ConsoleWriter.WritePlayerHand(dealer);
            }
            return dealer.isBust;
        }


        #endregion

        #region Settlement Phase

        //private void HandlePhaseSettlement() {
        //    if (player.hand > dealer.hand) {
        //        int payout = 2;
        //        player.wallet.AddAmount(bettingPool * payout);
        //    }
        //}

        private void HandlePhaseSettlement() {
            if (player.hand > dealer.hand || dealer.isBust) {
                int payout = 2;
                player.wallet.AddAmount(bettingPool * payout);
            }
        }



        #endregion

        #region Reset Phase

        private void PrepareNewRound() {
            //Empty player hands
            player.EmptyHand();
            dealer.EmptyHand();
            //Reset bettingPool
            bettingPool = 0;


            ConsoleWriter.OnNewRound();

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

    }
}
