using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack.Participant
{
    public struct Wallet
    {
        //int balance;
        public int balance { get; private set; }


        public Wallet(int amount) {
            this.balance = amount;
        }

        public static Wallet StartingWallet => new Wallet(500);  //Arbritary number

        /// <summary>
        /// When <see cref="Player"/> wins, this adds the winning amount to their balance.
        /// </summary>
        /// <param name="amount">The amount won.</param>
        public void AddAmount(int amount) {
            this.balance += amount;
        }

        public int AttemptBet(int betAmount) {
            if (balance - betAmount < 0) {
                betAmount = balance;
            }
            balance -= betAmount;
            return betAmount;
        }

        public bool IsBroke => balance <= 0;
    }
}
