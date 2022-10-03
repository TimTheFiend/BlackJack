using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack.Printing
{
    public static class UIBetDrawer
    {
        public static void StartBetPhase() {
            UIBaseDrawer.CursorToDrawableArea();
            Console.WriteLine("How much do you want to bet?");

        }

        public static void RepeatBetPhase() {
            UIBaseDrawer.ResetLine(UIBaseDrawer.GetCursorTop -1);
            StartBetPhase();
        }
    }
}
