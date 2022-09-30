using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack.Printing
{
    public static class UIHandler
    {
        private static ConsoleColor colorDefault = ConsoleColor.White;
        private static ConsoleColor colorMoney = ConsoleColor.Green;
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

        
        public static void ClearUI() {
            startBalanceIndex = -1;
            balanceStringLength = -1;

            Console.Clear();
        }

        
        public static void DrawPlayerHand(string output) {
            
            int left = 30;
            int top = 0;
            
            int _left = Console.CursorLeft;
            int _top = Console.CursorTop;

            Console.SetCursorPosition(left, top);
            Console.Write("PLAYER: " + output);
            Console.GetCursorPosition();
            Console.SetCursorPosition(_left, _top);
        }

        #region Draw Balance Functions
        public static void UpdateBalance(int playerBalance, int playerBet = 0) {
            int totalLength = balanceTextWidth + playerBalance.ToString().Length;

            // Check if balance width has changed.
            if (balanceStringLength != totalLength) {
                balanceStringLength = totalLength;
                startBalanceIndex = DrawBalanceArea();
            }

            DrawBalanceAndBet(playerBalance, playerBet);

        }

        private static void DrawBalanceAndBet(int playerBalance, int playerBet) {
            Console.ForegroundColor = colorMoney;
            //Prints the player's $ Balance.
            Console.SetCursorPosition(startBalanceIndex, playerBalanceRowIndex);
            Console.Write(playerBalance);

            //Prints the player's $ bet; defaults to 0
            Console.SetCursorPosition(Console.CursorLeft - playerBet.ToString().Length, playerBetRowIndex);
            Console.Write(playerBet);
            Console.SetCursorPosition(0, 5);

            Console.ForegroundColor = colorDefault;
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

        #endregion Draw Balance Functions
    }
}
