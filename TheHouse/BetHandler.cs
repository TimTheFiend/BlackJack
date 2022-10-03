using BlackJack.Participant;
using BlackJack.Printing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack.TheHouse
{
    public static class BetHandler
    {
        private static readonly int betLow = 10;
        private static readonly int betMid = 50;
        private static readonly int betHigh = 100;

        private static readonly string strBet = ") $";

        public static Player PlayerBet(Player player) {
            UIPrinter.CursorToDrawableArea();
            
            int[] bets = new int[] {
                betLow,
                betMid,
                betHigh
            };
            int amountAvailableBets = 0;

            int playerBet = 0;

            Console.WriteLine("How much you wanna bet, huh? (Enter to quit)");



            for (int i = 0; i < bets.Length; i++) {
                if (player.getBalance < bets[i]) {
                    continue;
                }
                Console.WriteLine($"{i}{strBet}{bets[i]}");
                amountAvailableBets++;
            }

            if (amountAvailableBets < 1) {
                return player;
            }

            while (player.getBalance > 0) {
                var userInput = Console.ReadKey(true);

                if (userInput.Key == ConsoleKey.Enter) {
                    Environment.Exit(0);
                }

                if (int.TryParse(userInput.KeyChar.ToString(), out int parsedUserInput)) {
                    if (parsedUserInput > 0 && parsedUserInput < amountAvailableBets) {
                        /* If Player wants to undo a bet */
                        int inputBet = bets[parsedUserInput - 1];
                        
                        if (userInput.Modifiers == ConsoleModifiers.Shift && playerBet - inputBet > 0) {
                            player.wallet.AddAmount(inputBet);
                            playerBet -= inputBet;
                            UIMoneyDrawer.DrawPlayerBalance(player.getBalance, playerBet);
                            continue;
                        }

                        if (player.getBalance - inputBet < 0) {
                            continue;
                        }
                    }
                }

            }
            return player;
        }
    }
}
