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
        public bool IsPlayerTurn { get; private set; } = true;
        private GamePhase phase { get; set; } = GamePhase.NONE;
        private Deck deck;
        private delegate void ActionHandler();


        public TheHouse() {
            deck = new Deck();
        }



        public void GameplayLoop() {
            ActionHandler actionHandler;
            while (true) {
                switch (phase) {
                    case GamePhase.NONE:
                        //GamePhaseNone();
                        actionHandler = GamePhaseNone;
                        break;
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
                }
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

        //TODO: Unique phase, make seperate handler
        private void GamePhaseBet() {
        }

        //TODO: Doesn't require any input from player
        private void GamePhaseShuffle() {
        }

        //TODO: Handle blackjack + Insurance
        private void GamePhaseDeal() {
        }

        private void GamePhasePlay() {
            #region Formula
            string[] msgs = new string[] {
                "CURRENT PHASE = THE_PLAY"
            };
            BlackJackAction[] actions = new BlackJackAction[] {
                BlackJackAction.STAND,
                BlackJackAction.HIT
            };
            #endregion
            //TODO: Check if player could DOUBLE_DOWN or SPLIT_PAIRS

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
