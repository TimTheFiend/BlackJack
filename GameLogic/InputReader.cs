using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack.GameLogic
{
    public static class InputReader
    {
        private static int currentRow = -1;

        public static int ReadInput() {
            while (true) {
                char input = Console.ReadKey(true).KeyChar;

                if (int.TryParse(input.ToString(), out int inputInt)) {
                    return inputInt;
                }
            }
        }


    }
}
