using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack.Printing
{
    public static class UIBetDrawer
    {
        /// <summary>
        /// Cleans the DrawableArea, and writes instructions to the player.
        /// </summary>
        public static void StartBetPhase() {
            UIPrinter.CursorToDrawableArea();
            Console.WriteLine("How much do you want to bet?");

        }

        /// <summary>
        /// Cleans the line the player is writing to, and reprints instructions.
        /// </summary>
        public static void RepeatBetPhase() {
            UIPrinter.ResetLine(UIPrinter.GetCursorTop -1);
            StartBetPhase();
        }
    }
}
