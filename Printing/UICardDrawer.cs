using BlackJack.BicycleCards;
using BlackJack.GameLogic;
using BlackJack.Participant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack.Printing
{
    public static class UICardDrawer
    {
        /* Delay between card draw */
        private static readonly double _drawDelayInSeconds = 1;
        private static int drawDelay => (int)_drawDelayInSeconds * 1000; //Milliseconds

        /* Console Top position */
        private static readonly int rowDeck = 4;
        private static readonly int rowPlayer = 5;
        private static readonly int rowDealer = 6;
        /* Colors */
        private static ConsoleColor colorPlayer = ConsoleColor.Cyan;
        private static ConsoleColor colorDealer = ConsoleColor.Magenta;
        private static ConsoleColor white = ConsoleColor.White;

        private static readonly string cardsRemaining = "Cards remaining:";


        /// <summary>
        /// Writes the remaining cards in the deck to the console.
        /// </summary>
        /// <param name="deckSize">Cards left in deck.</param>
        public static void DrawDeckSize(int deckSize) {
            /* Reset Console information */
            UIPrinter.SetCursor(rowDeck);
            UIPrinter.Color = white;
            /* Write information */
            UIPrinter.CursorTop = rowDeck;
            Console.Write($"{cardsRemaining} {deckSize}");

        }

        /// <summary>
        /// Updates the hand of the player who drew a card.
        /// </summary>
        /// <param name="bPlayer"></param>
        public static void DrawHand(BasePlayer bPlayer) {
            /* Reset CursorLeft */
            ResetCursorLeft();

            int cursorTopPos;
            ConsoleColor cColor;

            if (bPlayer is Player) {
                cursorTopPos = rowPlayer;
                cColor = colorPlayer;
            }
            else {
                cursorTopPos = rowDealer;
                cColor = colorDealer;
            }

            /* Draw name */
            UIPrinter.Color = cColor;
            UIPrinter.SetCursor(cursorTopPos, 0, bPlayer.ToString());


            /* Draw HandValue */
            UIPrinter.Color = white;
            Console.Write($" ({bPlayer.getHandValue}) : ");

            /* Draw Hand */
            foreach (Card card in bPlayer.getHand) {
                UIPrinter.Color = card.getColor;
                Console.Write($"[{card.getValueAndSuit}]");
            }

            Thread.Sleep(drawDelay);
        }


        /// <summary>
        /// Reset Console.CursorLeft position to be on the start of a line line.
        /// </summary>
        private static void ResetCursorLeft() {
            UIPrinter.CursorLeft = 0;
        }

        /// <summary>
        /// Cleans all the lines used by <see cref="UICardDrawer"/>.
        /// </summary>
        public static void ResetCardDrawer() {
            UIPrinter.ResetLine(rowDeck);
            UIPrinter.ResetLine(rowPlayer);
            UIPrinter.ResetLine(rowDealer);
        }
    }
}