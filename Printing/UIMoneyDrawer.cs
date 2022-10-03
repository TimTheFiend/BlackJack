using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack.Printing
{
    public static class UIMoneyDrawer
    {
        private static char wallChar = '#';
        private static readonly string playerBalance = "Player balance: $";
        private static readonly string playerBet = "Current Bet:";

        /* Console.CursorTop position for floor and ceiling. */
        private static int[] wallRowIndex = new int[] {
            0,
            3
        };
        /* Console.CursorTop position for Balance and Bet. */
        private static readonly int playerBalanceRowIndex = 1;
        private static readonly int playerBetRowIndex = 2;

        
        /// <summary>
        /// Clears the upper 4 lines in the console.
        /// </summary>
        public static void OnClear() {
            UIPrinter.ResetLines(wallRowIndex[0], wallRowIndex[1]);
        }



        #region Draw Balance Functions

        /// <summary>
        /// Draws the player's balance and bet within a square comprised of "#".
        /// </summary>
        /// <param name="balanceAmount"><see cref="Participant.Player"/>'s total balance.</param>
        /// <param name="betAmount"><see cref="Participant.Player"/>'s current bet, defaults to 0.</param>
        public static void DrawPlayerBalance(int balanceAmount, int betAmount = 0) {
            /* Clean the area to there's no text overlapping, and set color for consistency. */
            OnClear();
            UIPrinter.Color = UIPrinter.White;

            /* Figure out how far to draw the "roof" and "floor" of the square. */
            string _playerBalanceText = $"# {playerBalance}{balanceAmount} #";

            for (int i = 0; i < _playerBalanceText.Length; i++) {
                foreach (int row in wallRowIndex) {
                    UIPrinter.SetCursor(row, i, wallChar);
                }
            }

            /* Find the Console.CursorLeft position for the dollarsign, so bet is consistent. */
            int dollarSignIndex = $"# {playerBalance}".Length - 1;

            /* Print Balance information */
            UIPrinter.SetCursor(playerBalanceRowIndex, 0, _playerBalanceText);
            
            /* Get Bet Print location */
            int balanceAmountPosition = UIPrinter.GetCursorLeft - 2 - betAmount.ToString().Length; //-2 = " #"; -.Length for last digit position

            /* Player Bet */
            UIPrinter.ResetLine(playerBetRowIndex);
            UIPrinter.SetCursor(playerBetRowIndex, 0, $"# {playerBet}");
            UIPrinter.SetCursor(playerBetRowIndex, dollarSignIndex, "$");
            UIPrinter.SetCursor(playerBetRowIndex, balanceAmountPosition, $"{betAmount} #");
        }

        #endregion Draw Balance Functions
    }
}
