using BlackJack.BicycleCards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack.Participant
{
    public class Player : BasePlayer, ICloneable
    {
        public Wallet wallet;

        public object Clone() {
            return this.MemberwiseClone();
        }

        public Player() {
            wallet = Wallet.StartingWallet;
        }

        public override string ToString() {
            return "PLAYER";
        }

        //TODO
        public Player OnSplittingPairs() {
            Player split = (Player)this.Clone();
            Card splitPair = hand.GetSplitPair();

            split.EmptyHand();
            split.hand.AddCard(splitPair);

            return split;
        }


        public int getBalance => wallet.balance;

        /// <summary>
        /// Returns the <see cref="BlackJackAction"/> that are available based on starting hand.
        /// </summary>
        public List<BlackJackAction> GetPlayerActions {
            get {
                List<BlackJackAction> playerActions = new List<BlackJackAction>();
                playerActions.Add(BlackJackAction.STAND);
                playerActions.Add(BlackJackAction.HIT);
                //if (hand.CanSplitPairs) playerActions.Add(BlackJackAction.SPLIT_PAIRS);
                if (hand.CanDoubleDown) playerActions.Add(BlackJackAction.DOUBLE_DOWN);

                return playerActions;
            }
        }
    }
}
