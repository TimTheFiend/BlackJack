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
            startBalanceIndex = -1;
            balanceStringLength = -1;
        }



        #region Draw Balance Functions
        public static void UpdateBalance(int playerBalance, int playerBet = 0) {
            int totalLength = balanceTextWidth + playerBalance.ToString().Length;

            balanceStringLength = totalLength;
            DrawBalanceArea();

            DrawBalanceAndBet(playerBalance, playerBet);

        }

        private static void DrawBalanceAndBet(int playerBalance, int playerBet) {
            UIBaseDrawer.Color = colorMoney;
            //Prints the player's $ Balance.
            UIBaseDrawer.SetCursor(playerBalanceRowIndex, startBalanceIndex, playerBalance);

            //Prints the player's $ bet; defaults to 0
            UIBaseDrawer.SetCursor(playerBetRowIndex, Console.CursorLeft - playerBet.ToString().Length, playerBet.ToString());
            //UIBaseDrawer.SetCursor(5, 0);  //TODO ??????

            UIBaseDrawer.Color = colorDefault;
        }

        public static void DrawPlayerBalance(int balanceAmount, int betAmount = 0) {
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
            UIBaseDrawer.SetCursor(playerBetRowIndex, 0, $"# {playerBet}");
            UIBaseDrawer.SetCursor(playerBetRowIndex, dollarSignIndex, "$");
            UIBaseDrawer.SetCursor(playerBetRowIndex, balanceAmountPosition, $"{betAmount} #");

            /* Draw walls */
            DrawBalanceArea();

            return;
            /* $ placement */
            /* Bet Amount */

            /* Write start */
            UIBaseDrawer.SetCursor(playerBetRowIndex, 0, $"# {playerBet}");

            UIBaseDrawer.SetCursor(playerBetRowIndex, 0, $"# {playerBalance}{betAmount} #");

            int cursorSuffixIndex = Console.CursorLeft - 2;
            UIBaseDrawer.SetCursor(playerBetRowIndex, 2, playerBet);
            UIBaseDrawer.SetCursor(playerBetRowIndex, cursorSuffixIndex, " $");
            UIBaseDrawer.SetCursor(playerBetRowIndex, 0, "# ");
            UIBaseDrawer.SetCursor(playerBetRowIndex, balanceStringLength - 2, " #");
            //UIBaseDrawer.SetCursor(playerBalanceRowIndex, balanceStringLength - 2, " #");
        }

        //RETURNS CURSOR POSITION FOR BALANCE AMOUNT
        private static void DrawBalanceArea() {
            UIBaseDrawer.Color = colorWall;

            //Draw walls in middle
            //UIBaseDrawer.SetCursor(playerBalanceRowIndex, 0, $"# {playerBalance}");
            //UIBaseDrawer.SetCursor(playerBalanceRowIndex, balanceStringLength - 2, " #");


            // TODO refactor
            //Draw walls in middle
            //UIBaseDrawer.SetCursor(playerBetRowIndex, 0, "# ");
            //UIBaseDrawer.SetCursor(playerBetRowIndex, balanceStringLength - 2, " #");


            for (int i = 0; i < balanceStringLength; i++) {
                foreach (int row in wallRowIndex) {
                    UIBaseDrawer.SetCursor(row, i, wallChar);
                }
            }
            //UIBaseDrawer.SetCursor(playerBalanceRowIndex, 2, playerBalance);
            
            ////Bet print
            //int cursorSuffixIndex = Console.CursorLeft - 2;
            //UIBaseDrawer.SetCursor(playerBetRowIndex, 2, playerBet);
            //UIBaseDrawer.SetCursor(playerBetRowIndex, cursorSuffixIndex, " $");

        }

        #endregion Draw Balance Functions
    }
}
