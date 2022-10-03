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

        public static void Clear() {
            Console.Clear();

            /* Call OnClear functions */
            UIMoneyDrawer.OnClear();
        }

        public static void SetCursor(int top, int left) {
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

        public static void ResetCursor() {
            SetCursor(0, 0);
        }

        public static int GetCursorLeft => Console.CursorLeft;
        public static int GetCursorTop => Console.CursorTop;
    }
}
