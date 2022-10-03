using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using BlackJack.BicycleCards;
using BlackJack.GameLogic;
using BlackJack.Participant;

namespace BlackJack.Printing
{
    /// <summary>
    /// Has been mostly phased out by classes within ".\Printing".
    /// </summary>
    public static class ConsoleWriter
    {
        private static bool isDebugging = true;


        public static void Write(string output, bool indent = true) {
            ResetConsoleColor();
            Console.Write(indent ? output + "\t" : output);
        }

        public static void Writeline(string output) {
            ResetConsoleColor();
            Console.WriteLine(output);
        }

        public static void AskUserInput() {
            ConsoleWriter.Writeline("-PLEASE SUBMIT YOUR INPUT-");
        }

        public static void InvalidInput() {
            ConsoleWriter.Writeline("*=INVALID INPUT, TRY AGAIN=*");
        }

        public static void WriteActionToPlayer(string output) {
            ConsoleWriter.Writeline($"\t{output}.");
        }

        public static void ExitGameOption() {
            ConsoleWriter.WriteActionToPlayer("Q) - EXIT GAME");
        }

        public static void OnBlackJackExit() {
            ConsoleWriter.Writeline("You're leaving? You came to my black jack table, and you're leaving?");
        }
        public static void OnReshuffle() {
            ConsoleWriter.Writeline("Re-shuffling deck");
        }


        public static void WriteCard(Card card) {
            Console.ForegroundColor = card.getColor;
            Console.WriteLine(card.getValueAndSuit);

            ResetConsoleColor();
        }

        public static void WriteCard(params Card[] cards) {
            foreach (Card card in cards) {
                Console.ForegroundColor = card.getColor;
                Console.Write($"[{card.getValueAndSuit}]");
            }
            Console.WriteLine();
            ResetConsoleColor();
        }

        public static void WriteCard(List<Card> cards) {
            ConsoleWriter.WriteCard(cards.ToArray());
        }

        public static void WriteCard(string player, params Card[] cards) {
            ResetConsoleColor();
            Console.Write($"{player}:\t");
            foreach (Card card in cards) {
                Console.ForegroundColor = card.getColor;
                Console.Write($"[{card.getValueAndSuit}]");
            }
            Console.WriteLine();
            ResetConsoleColor();
        }

        //TODO Move to UIHandler.
        public static void WritePlayerHand(string player, int handValue, List<Card> hand) {
            ResetConsoleColor();
            Console.Write($"{player}:\t");
            Console.Write($"({handValue})\t");
            foreach (Card card in hand) {
                Console.ForegroundColor = card.getColor;
                Console.Write($"[{card.getValueAndSuit}]");
            }
            Console.WriteLine();
            ResetConsoleColor();

            if (isDebugging) {
                return;
            }
            Thread.Sleep(GameManager.drawDelay);
        }


        public static void Clear() {
            ResetConsoleColor();
            Console.Clear();
        }

        public static void OnNewRound() {
            ResetConsoleColor();
            Console.WriteLine("Press enter to continue...");
            Console.ReadLine();
            Console.Clear();
        }

        private static void ResetConsoleColor() {
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
