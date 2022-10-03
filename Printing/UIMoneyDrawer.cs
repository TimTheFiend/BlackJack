using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack.Printing
{
    public static class UIMoneyDrawer
    {
        private static ConsoleColor colorDefault = ConsoleColor.White;
        private static ConsoleColor colorMoney = ConsoleColor.Green;
        private static ConsoleColor colorWall = ConsoleColor.Yellow;

        #region Unique to Balance
        private static char wallChar = '#';
        private static int startBalanceIndex = -1;
        private static int balanceStringLength = -1;
        private static readonly string playerBalance = "Player balance: $";
        private static readonly string playerBet = "Current Bet:";
        private static int balanceTextWidth => playerBalance.Length + 4;

        private static int[] wallRowIndex = new int[] {
            0,
            3
        };
        private static readonly int playerBalanceRowIndex = 1;
        private static readonly int playerBetRowIndex = 2;
        #endregion Unique to Balance

        
        public static void OnClear() {
            UIBaseDrawer.ResetLines(wallRowIndex[0], wallRowIndex[1]);
        }



        #region Draw Balance Functions

        public static void DrawPlayerBalance(int balanceAmount, int betAmount = 0) {
            OnClear();
            UIBaseDrawer.Color = UIBaseDrawer.White;
            string _playerBalanceText = $"# {playerBalance}{balanceAmount.ToString()} #";

            for (int i = 0; i < _playerBalanceText.Length; i++) {
                foreach (int row in wallRowIndex) {
                    UIBaseDrawer.SetCursor(row, i, wallChar);
                }
            }


            int dollarSignIndex = $"# {playerBalance}".Length - 1;


            /* Print Balance information */
            UIBaseDrawer.SetCursor(playerBalanceRowIndex, 0, _playerBalanceText);
            
            /* Get Bet Print location */
            int balanceAmountPosition = UIBaseDrawer.GetCursorLeft - 2 - betAmount.ToString().Length; //-2 = " #"; -.Length for last digit position

            /* Player Bet */
            UIBaseDrawer.ResetLine(playerBetRowIndex);
            UIBaseDrawer.SetCursor(playerBetRowIndex, 0, $"# {playerBet}");
            UIBaseDrawer.SetCursor(playerBetRowIndex, dollarSignIndex, "$");
            UIBaseDrawer.SetCursor(playerBetRowIndex, balanceAmountPosition, $"{betAmount} #");
        }

        #endregion Draw Balance Functions
    }
}
