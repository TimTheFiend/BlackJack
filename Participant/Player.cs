using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack.Participant
{
    public class Player : BasePlayer
    {
        public Wallet wallet;

        public Player() {
            wallet = Wallet.StartingWallet;
        }

        public override string ToString() {
            return "PLAYER";
        }

        public int getBalance => wallet.balance;

        public string getBalanceString => "$" + getBalance;

        //TODO
        public BlackJackAction[] GetPlayerActions() {
            List<BlackJackAction> playerActions = new List<BlackJackAction>();
            playerActions.Add(BlackJackAction.STAND);
            playerActions.Add(BlackJackAction.HIT);

            //TODO: Check for pairs
            //TODO: Check for double down

            return playerActions.ToArray();
        }
    }
}
