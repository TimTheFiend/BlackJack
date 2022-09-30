using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack.Printing
{
    public static class UIHandler
    {
        private static ConsoleColor colorPlayerName = ConsoleColor.White;
        private static ConsoleColor colorBalance = ConsoleColor.Green;
        private static ConsoleColor colorWall = ConsoleColor.Yellow;

        #region Unique to Balance
        private static char wallChar = '#';
        private static int startBalanceIndex = -1;
        private static int balanceStringLength = -1;
        private static readonly string balanceText = "Player balance: $";
        private static readonly string betText = "Current Bet:";
        private static int balanceTextWidth => balanceText.Length + 4;

        private static int[] wallRowIndex = new int[] {
            0,
            3
        };
        private static readonly int playerBalanceRowIndex = 1;
        private static readonly int playerBetRowIndex = 2;
        #endregion Unique to Balance

        public static void UpdateBalance(int playerBalance, int playerBet = 0) {
            int totalLength = balanceTextWidth + playerBalance.ToString().Length;

            // Check if balance width has changed.
            if (balanceStringLength != totalLength) {
                balanceStringLength = totalLength;
                startBalanceIndex = DrawBalanceArea();
            }


            //Console.SetCursorPosition(2, playerBalanceRowIndex);
            //Console.Write($"{balanceText}");
            //startBalanceIndex = Console.CursorLeft;
            Console.SetCursorPosition(startBalanceIndex, playerBalanceRowIndex);
            Console.Write(playerBalance);
            
            //Sets the Bet value to be 0
            Console.SetCursorPosition(Console.CursorLeft - playerBet.ToString().Length, playerBetRowIndex);
            Console.Write(playerBet);
            Console.SetCursorPosition(0, 5);
            

        }

        //RETURNS CURSOR POSITION FOR BALANCE AMOUNT
        private static int DrawBalanceArea() {
            Console.ForegroundColor = colorWall;

            //Draw walls in middle
            Console.SetCursorPosition(0, playerBalanceRowIndex);
            Console.Write("# ");
            Console.SetCursorPosition(balanceStringLength - 2, playerBalanceRowIndex);
            Console.Write(" #");
            // TODO refactor
            //Draw walls in middle
            Console.SetCursorPosition(0, playerBetRowIndex);
            Console.Write("# ");
            Console.SetCursorPosition(balanceStringLength - 2, playerBetRowIndex);
            Console.Write(" #");


            for (int i = 0; i < balanceStringLength; i++) {
                foreach (int row in wallRowIndex) {
                    Console.SetCursorPosition(i, row);
                    Console.Write(wallChar);
                }
            }
            Console.SetCursorPosition(2, playerBalanceRowIndex);
            Console.Write(balanceText);
            
            //Bet print
            int cursorSuffixIndex = Console.CursorLeft - 2;
            Console.SetCursorPosition(2, playerBetRowIndex);
            Console.Write(betText);
            Console.SetCursorPosition(cursorSuffixIndex, playerBetRowIndex);
            Console.Write(" $");
            

            return Console.CursorLeft;
        }
    }
}
