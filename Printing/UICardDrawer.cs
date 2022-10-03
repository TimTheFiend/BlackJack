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
        private static readonly double _drawDelayInSeconds = 1.5;
        private static int drawDelay => (int)_drawDelayInSeconds * 1000; //Milliseconds

        /* Console Top position */
        private static readonly int rowDeck = 4;
        private static readonly int rowPlayer = 5;
        private static readonly int rowDealer = 6;

        private static ConsoleColor colorPlayer = ConsoleColor.Cyan;
        private static ConsoleColor colorDealer = ConsoleColor.Magenta;
        private static ConsoleColor white = ConsoleColor.White;

        private static readonly string cardsRemaining = "Cards remaining:";


        public static void DrawDeckSize(int deckSize) {
            /* Reset Console information */
            ResetCursorLeft();
            Color = white;


            CursorTop = rowDeck;

            Console.Write($"{cardsRemaining}\t{deckSize}");

        }

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
            CursorTop = cursorTopPos;
            Color = cColor;

            Console.Write(bPlayer.ToString());


            /* Draw HandValue */
            Color = white;

            Console.Write($" ({bPlayer.getHandValue}) : ");

            /* Draw Hand */
            foreach (Card card in bPlayer.getHand) {
                Color = card.getColor;
                Console.Write($"[{card.getValueAndSuit}]");
            }

            Thread.Sleep(drawDelay);
        }

        private static ConsoleColor Color {
            set {
                Console.ForegroundColor = value;
            }
        }

        private static int CursorLeft {
            set {
                Console.CursorLeft = value;
            }
        }

        private static int CursorTop {
            set {
                Console.CursorTop = value;
            }
        }

        private static void ResetCursorLeft() {
            CursorLeft = 0;
        }
    }
}