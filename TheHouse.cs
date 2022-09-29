using BlackJack.BicycleCards;
using BlackJack.GameLogic;
using BlackJack.Participant;
using BlackJack.Printing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{
    public class TheHouse
    {
        public bool IsPlayerTurn { get; private set; }  //True if Player; otherwise Dealer
        public Dealer dealer;
        public Player player;
        private GamePhase phase { get; set; }
        private Deck deck;

        public bool isDeckPlayable => !deck.isShuffleNeeded;

        private delegate void ActionHandler();

        public TheHouse() {
            //Set local variables
            IsPlayerTurn = true;
            phase = GamePhase.BET;
            dealer = new Dealer();
            player = new Player();
            //Instantiate deck
            deck = new Deck();

            //NOTE
            deck.ShuffleDeck();
        }

        public void EndTurn() {
            IsPlayerTurn = !IsPlayerTurn;
        }

        //CURRENT PHASE = THE PLAY
        public void DealerPlayLoop() {
            bool IS_DEBUGGING = true;
            if (IS_DEBUGGING) {
                GamePhaseDeal();
            }

            //Dealer reveals the facedown card
            //TODO: CURRENTLY CRASHES BECAUSE THE LOOP SKIPS TO PLAY
            dealer.hand.TurnCardFaceUp();
            ConsoleWriter.WriteCard(dealer.getHand.ToArray());
            while (dealer.canHit) {
                dealer.Hit(deck.DrawCard());
                ConsoleWriter.WriteCard(dealer.getHand.ToArray());
            }
            if (dealer.isBust) {
                ConsoleWriter.Writeline("WHOOPS");
                ConsoleWriter.Writeline($"DEALER: {dealer.getHandValue}");
            }
            else {
                dealer.Stand();
                ConsoleWriter.Writeline("DEALER STANDS");
                ConsoleWriter.Writeline($"DEALER: {dealer.getHandValue}");
            }
        }


        public void GameplayLoop() {
            ActionHandler actionHandler;
            while (true) {
                switch (phase) {
                    //case GamePhase.NONE:
                    //    actionHandler = GamePhaseNone;
                    //    break;
                    case GamePhase.BET:
                        actionHandler = GamePhaseBet;
                        break;
                    case GamePhase.SHUFFLE:
                        actionHandler = GamePhaseShuffle;
                        break;
                    case GamePhase.DEAL:
                        actionHandler = GamePhaseDeal;
                        break;
                    case GamePhase.PLAY:
                        actionHandler = GamePhasePlay;
                        break;
                    case GamePhase.SETTLEMENT:
                        actionHandler = GamePhaseSettlement;
                        break;
                    default:
                        throw new Exception("TheHouse.phase doesn't have a value.\n\tI don't know how, but it really shouldn't be possible.");
                }
                ConsoleWriter.Writeline(phase.ToString());
                actionHandler();
            }
        }

        /// <summary>
        /// Writes instructions to, and handles reading player input. 
        /// </summary>
        /// <param name="actions">Array of possible actions the player can make.</param>
        /// <param name="textToPlayer">Instructions to the player</param>
        /// <returns>Action to be performed.</returns>
        private BlackJackAction HandlePlayerInput(BlackJackAction[] actions, params string[] textToPlayer) {
            char exitGameInput = 'q';
            foreach (string text in textToPlayer) {
                ConsoleWriter.Writeline(text);
            }

            //Print actions to player
            for (int i = 0; i < actions.Length; i++) {
                ConsoleWriter.WriteActionToPlayer($"{i + 1}) - {actions[i]}");
            }
            ConsoleWriter.ExitGameOption();
            ConsoleWriter.AskUserInput();

            //Read userinput
            while (true) {
                var initialInput = Console.ReadKey(true).KeyChar;
                if (int.TryParse(initialInput.ToString(), out int userInput)) {
                    if (userInput > 0 && userInput <= actions.Length) {
                        return actions[userInput - 1];
                    }
                }
                //Closes application.
                if (char.ToUpperInvariant(initialInput) == char.ToUpperInvariant(exitGameInput)) {
                    HandleExitGame();
                }

                ConsoleWriter.InvalidInput();
            }
        }


        #region GamePhase management
        //PHASE_0
        private void GamePhaseNone() {
            #region Formula
            string[] msgs = new string[] {
                "Welcome to the Black Jack table!",
                "Presumably you're here for black jack, still...",
                "What would you like to do?"
            };
            BlackJackAction[] actions = new BlackJackAction[] {
                BlackJackAction.BET
            };
            #endregion

            BlackJackAction playerAction = HandlePlayerInput(actions, msgs);
        }

        //PHASE_1
        //TODO: Unique phase, make seperate handler
        private void GamePhaseBet() {
        }

        //PHASE_2
        //TODO: Doesn't require any input from player
        private void GamePhaseShuffle() {
            if (deck.isShuffleNeeded) {
                ConsoleWriter.OnShuffle();
                deck.ReshuffleDeck();
            }
        }

        //PHASE_3
        //TODO: Handle blackjack + Insurance
        private void GamePhaseDeal() {
            ConsoleWriter.Writeline("====DEAL====");
            int startingHandSize = 2;

            for (int i = 0; i < startingHandSize; i++) {
                //Player gets card
                player.hand.AddCard(deck.DrawCard());
                ConsoleWriter.WriteCard(player.ToString(), player.getHand.ToArray());



                Card dealerCard = deck.DrawCard();

                if (dealer.hand.handSize == 0) {
                    dealerCard = dealerCard.SetFaceDown();
                }
                dealer.hand.AddCard(dealerCard);
                ConsoleWriter.WriteCard(dealer.ToString(), dealer.getHand.ToArray());
            }


        }

        //PHASE_4
        private void GamePhasePlay() {
            string[] msgs = new string[] {
                "CURRENT PHASE = THE_PLAY",
                player.hand.getCardsOnHand
            };
            BlackJackAction[] actions = player.GetPlayerActions();
            
        }

        /*TODO
         * - Calculate who wins
         * - Calculate payout
         */
        private void GamePhaseSettlement() {
        }


        #endregion GamePhase management

        #region Handling BlackJackActions

        /// <summary>
        /// Handles the behavior of <see cref="BlackJackAction.EXIT_GAME"/>.
        /// </summary>
        private void HandleExitGame() {
            ConsoleWriter.OnBlackJackExit();
            Environment.Exit(0);
        }

        #endregion
    }
}
