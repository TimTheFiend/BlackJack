using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{
    public static class ConsoleWriter
    {
        public static void Writeline(string output) {
            ResetConsoleColor();
            Console.WriteLine(output);
        }

        public static void WriteCard(Card card) {
            Console.ForegroundColor = ((int)card.Suit) % 2 == 0 ? CardPrintout.CardBlack : CardPrintout.CardRed;
            Console.WriteLine(card.getValueAndSuit);

            ResetConsoleColor();
        }
        public static void WriteCard(params Card[] cards) {
            foreach (Card card in cards) {
                Console.ForegroundColor = card.getColor;
                Console.Write(card.getValueAndSuit);
            }
            Console.WriteLine();
            ResetConsoleColor();
        }

        public static void Writeline(CardPrintout cardPrintout) {
            Console.ForegroundColor = cardPrintout.Color;
            Console.WriteLine(cardPrintout.TextOutput);

            ResetConsoleColor();
        }


        // Note: Does not add spaces between prints.
        public static void Writeline(params CardPrintout[] cardPrintouts) {
            foreach (CardPrintout item in cardPrintouts) {
                Console.ForegroundColor = item.Color;
                Console.Write(item.TextOutput);
            }
            Console.WriteLine();
            ResetConsoleColor();
        }

        private static void ResetConsoleColor() {
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
