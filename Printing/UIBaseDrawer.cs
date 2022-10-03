using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack.Printing
{
    public static class UIBaseDrawer
    {

        public static ConsoleColor White => ConsoleColor.White;
        public static ConsoleColor Green => ConsoleColor.Green;
        public static ConsoleColor Blue => ConsoleColor.Blue;

        private static readonly int ResetLength = 90;

        public static int CursorLeft {
            set {
                Console.CursorLeft = value;
            }
        }

        public static int CursorTop {
            set {
                Console.CursorTop = value;
            }
        }

        public static ConsoleColor Color {
            set {
                Console.ForegroundColor = value;
            }
        }

        /* Getters */
        public static int GetCursorLeft => Console.CursorLeft;
        public static int GetCursorTop => Console.CursorTop;
        public static int GetTopDrawable => 8;
        public static int GetBottomDrawable => 15;


        public static void Clear() {
            Console.Clear();

            /* Call OnClear functions */
            UIMoneyDrawer.OnClear();
        }

        #region SetCursor
        public static void SetCursor(int top, int left = 0) {
            Console.SetCursorPosition(left, top);
        }

        public static void SetCursor(int top, int left, string text) {
            SetCursor(top, left);
            Console.Write(text);
        }

        public static void SetCursor(int top, int left, char text) {
            SetCursor(top, left);
            Console.Write(text);
        }

        public static void SetCursor(int top, int left, int text) {
            SetCursor(top, left);
            Console.Write(text.ToString());
        }
        #endregion

        public static void CursorToDrawableArea() {
            SetCursor(GetTopDrawable, 0);
        }

        public static void ResetCursor() {
            SetCursor(0, 0);
        }

        public static void ResetLine(int top) {
            SetCursor(top, 0, "".PadRight(ResetLength, ' '));
        }

        public static void ResetLines(int start, int end) {
            for (int i = start; i < end + 1; i++) {
                ResetLine(i);
            }
        }


        public static void ResetDrawableArea() {
            for (int i = GetTopDrawable; i < GetBottomDrawable + 1; i++) {
                ResetLine(i);
            }

            CursorToDrawableArea();
        }
    }
}
