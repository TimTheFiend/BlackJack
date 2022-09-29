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


        public TheHouse() {
            deck = new Deck();
        }



        public void GameplayLoop() {
            bool isStillRunning = true;

            while (isStillRunning) {
                switch (phase) {
                    case GamePhase.NONE:
                        GamePhaseNone(out isStillRunning);
                        break;
                    case GamePhase.BET:
                        GamePhaseBet(out isStillRunning);
                        break;
                    case GamePhase.SHUFFLE:
                        GamePhaseShuffle(out isStillRunning);
                        break;
                    case GamePhase.DEAL:
                        GamePhaseDeal(out isStillRunning);
                        break;
                    case GamePhase.PLAY:
                        GamePhasePlay(out isStillRunning);
                        break;
                    case GamePhase.SETTLEMENT:
                        GamePhaseSettlement(out isStillRunning);
                        break;
                }
            }

            Console.WriteLine("-----ENDING GAMEPLAYLOOP-----");
        }

        
        private BlackJackAction HandlePlayerInput(List<BlackJackAction> actions, params string[] textToPlayer) {
            char exitGameInput = 'q';
            foreach (string text in textToPlayer) {
                ConsoleWriter.Writeline(text);
            }

            //Print actions to player
            for (int i = 0; i < actions.Count; i++) {
                ConsoleWriter.WriteActionToPlayer($"{i + 1}) - {actions[i]}");
            }
            ConsoleWriter.ExitGameOption();
            ConsoleWriter.AskUserInput();

            //Read userinput
            while (true) {
                var initialInput = Console.ReadKey(true).KeyChar;
                if (int.TryParse(initialInput.ToString(), out int userInput)) {
                    if (userInput > 0 && userInput <= actions.Count) {
                        return actions[userInput - 1];
                    }
                }
                //Closes application.
                if (char.ToUpperInvariant(initialInput) == char.ToUpperInvariant(exitGameInput)) {
                    ConsoleWriter.OnBlackJackExit();
                    Environment.Exit(0);
                }

                ConsoleWriter.InvalidInput();
            }
        }


        #region GamePhase management
        //TODO
        private void GamePhaseNone(out bool isStillRunning) {
            isStillRunning = true;
        }

        private void GamePhaseBet(out bool isStillRunning) {
            isStillRunning = true;
        }

        private void GamePhaseShuffle(out bool isStillRunning) {
            isStillRunning = true;
        }

        private void GamePhaseDeal(out bool isStillRunning) {
            isStillRunning = true;
        }

        private void GamePhasePlay(out bool isStillRunning) {
            isStillRunning = true;
        }

        private void GamePhaseSettlement(out bool isStillRunning) {
            isStillRunning = true;
        }
        #endregion GamePhase management
    }
}
