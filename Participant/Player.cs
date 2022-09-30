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
        public string FOO = "hahah";

        public object Clone() {
            return this.MemberwiseClone();
        }

        public Player() {
            wallet = Wallet.StartingWallet;
        }

        public override string ToString() {
            return "PLAYER";
        }

        /// <summary>
        /// Creates a clone of itself, to be used when <see cref="BlackJackAction.SPLIT_PAIRS"/>.
        /// </summary>
        /// <returns>Clone of <c>this</c> with one of the cards from the original hand.</returns>
        public Player OnSplittingPairs() {
            Player split = (Player)this.Clone();
            Card splitPair = hand.GetSplitPair();

            split.EmptyHand();
            split.hand.AddCard(splitPair);

            return split;
        }


        public int getBalance => wallet.balance;

        public string getBalanceString => "$" + getBalance;


        public List<BlackJackAction> GetPlayerActions {
            get {
                List<BlackJackAction> playerActions = new List<BlackJackAction>();
                playerActions.Add(BlackJackAction.STAND);
                playerActions.Add(BlackJackAction.HIT);
                if (hand.CanSplitPairs) playerActions.Add(BlackJackAction.SPLIT_PAIRS);
                if (hand.CanDoubleDown) playerActions.Add(BlackJackAction.DOUBLE_DOWN);

                return playerActions;
            }
        }

        public List<BlackJackAction> GetRegularActions {
            get {
                return new List<BlackJackAction>() {
                    BlackJackAction.STAND,
                    BlackJackAction.HIT
                };
            }
        }
    }
}
