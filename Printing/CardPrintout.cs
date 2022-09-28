using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack.Printing
{
    public struct CardPrintout
    {
        public string TextOutput;
        public ConsoleColor Color;

        public CardPrintout(string textOutput, ConsoleColor color)
        {
            TextOutput = textOutput;
            Color = color;
        }

        public static CardPrintout Printout(string textOutput)
        {
            return new CardPrintout(textOutput, ConsoleColor.White);
        }

        public static CardPrintout PrintoutCard(string textOutput, ConsoleColor color)
        {
            return new CardPrintout(textOutput, color);
        }

        public static ConsoleColor CardRed => ConsoleColor.Red;

        public static ConsoleColor CardBlack => ConsoleColor.DarkBlue;
    }
}
