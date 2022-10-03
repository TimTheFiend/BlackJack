using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack.Printing
{
    public static class UIPrinter
    {

        /* Colors */
        public static ConsoleColor White => ConsoleColor.White;
        public static ConsoleColor Green => ConsoleColor.Green;
        public static ConsoleColor Blue => ConsoleColor.Blue;

        /* How far to reset a line. */
        private static readonly int ResetLength = 90;

        /// <summary>
        /// Sets the <see cref="Console.CursorLeft"/> value.
        /// </summary>
        public static int CursorLeft {
            set {
                Console.CursorLeft = value;
            }
        }

        /// <summary>
        /// Sets the <see cref="Console.CursorTop"/> value.
        /// </summary>
        public static int CursorTop {
            set {
                Console.CursorTop = value;
            }
        }

        /// <summary>
        /// Sets the <see cref="Console.ForegroundColor"/> value.
        /// </summary>
        public static ConsoleColor Color {
            set {
                Console.ForegroundColor = value;
            }
        }

        /* Getters */
        public static int GetCursorLeft => Console.CursorLeft;
        public static int GetCursorTop => Console.CursorTop;
        
        /* Confines of the DrawableArea */
        public static int GetTopDrawable => 8;
        public static int GetBottomDrawable => 15;

        /// <summary>
        /// Clears the entire Console, and redraws <see cref="UIMoneyDrawer"/>.
        /// </summary>
        public static void Clear() {
            Console.Clear();

            /* Call OnClear functions */
            UIMoneyDrawer.OnClear();
        }


        #region SetCursor functions.
        /// <summary>
        /// Sets the values for <see cref="Console.SetCursorPosition(int, int)"/>, but in a way that makes more sense to me.
        /// </summary>
        /// <param name="top">Row position</param>
        /// <param name="left">Column position, defaults to 0 which is all the way to the left.</param>
        public static void SetCursor(int top, int left = 0) {
            Console.SetCursorPosition(left, top);
        }

        /// <summary>
        /// Sets the values for <see cref="Console.SetCursorPosition(int, int)"/>, and prints text.
        /// </summary>
        /// <param name="top">Row position</param>
        /// <param name="left">Column position</param>
        /// <param name="text">Text to write to console.</param>
        public static void SetCursor(int top, int left, string text) {
            SetCursor(top, left);
            Console.Write(text);
        }

        /// <summary>
        /// Sets the values for <see cref="Console.SetCursorPosition(int, int)"/>, and prints text.
        /// </summary>
        /// <param name="top">Row position</param>
        /// <param name="left">Column position</param>
        /// <param name="text">Char to write to console.</param>
        public static void SetCursor(int top, int left, char text) {
            SetCursor(top, left);
            Console.Write(text);
        }

        /// <summary>
        /// Sets the values for <see cref="Console.SetCursorPosition(int, int)"/>, and prints text.
        /// </summary>
        /// <param name="top">Row position</param>
        /// <param name="left">Column position</param>
        /// <param name="text">int to write to console.</param>
        public static void SetCursor(int top, int left, int text) {
            SetCursor(top, left);
            Console.Write(text.ToString());
        }
        #endregion

        /// <summary>
        /// Sets <see cref="Console.SetCursorPosition(int, int)"/> to be at the top of the Drawable area.
        /// </summary>
        public static void CursorToDrawableArea() {
            SetCursor(GetTopDrawable, 0);
        }

        /// <summary>
        /// Cleans a given line by filling it with spaces, essentially wiping it clean.
        /// </summary>
        /// <param name="top">Line row.</param>
        public static void ResetLine(int top) {
            SetCursor(top, 0, "".PadRight(ResetLength, ' '));
        }

        /// <summary>
        /// Cleans a series of lines by filling it with spaces, essentially wiping it clean.
        /// </summary>
        /// <param name="start">Line where cleaning starts.</param>
        /// <param name="end">Line where cleaning ends, inclusive.</param>
        public static void ResetLines(int start, int end) {
            for (int i = start; i < end + 1; i++) {
                ResetLine(i);
            }
        }

        /// <summary>
        /// Cleans the drawable area, and sets cursor at the top of said area.
        /// </summary>
        public static void ResetDrawableArea() {
            for (int i = GetTopDrawable; i < GetBottomDrawable + 1; i++) {
                ResetLine(i);
            }

            CursorToDrawableArea();
        }


        /// <summary>
        /// Quick dirty function to tell the player they've busted their last nut at this black jack table.
        /// </summary>
        public static void OnBlackJackExit() {
            Clear();
            CursorToDrawableArea();
            ConsoleWriter.Writeline("You're leaving? You came to my black jack table, and you're leaving?");
        }
    }
}
