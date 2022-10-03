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

        /// <summary>
        /// <c>true</c> if there's more than 0 in <see cref="balance"/>; otherwise <c>false</c>
        /// </summary>
        public bool IsBroke => balance <= 0;
        
        
        /// <summary>
        /// Creates a new wallet object.
        /// </summary>
        /// <param name="amount">The player's starting amount.</param>
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


        /// <summary>
        /// Attempts to bet the full amount; if the amount is bigger than <see cref="balance"/> the amount will be capped.
        /// </summary>
        /// <param name="betAmount">The value the player is betting.</param>
        /// <returns>The actual value the player can bet.</returns>
        public int AttemptBet(int betAmount) {
            if (balance - betAmount < 0) {
                betAmount = balance;
            }
            balance -= betAmount;
            return betAmount;
        }



    }
}
