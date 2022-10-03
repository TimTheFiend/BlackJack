using BlackJack.BicycleCards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack.Participant
{
    public class Dealer : BasePlayer
    {
        private const int minValueStand = 17;
        public bool canHit => hand.getTotalHandValue < minValueStand;

        public override BlackJackAction HandlePlayerTurn() {
            return canHit ? BlackJackAction.HIT : BlackJackAction.STAND;
        }

        public bool CanInsurance => hand.CanInsurance;

        public override void Hit(Card newCard) {
            if (hand.handSize == 0) {
                newCard.SetFaceDown();
            }
            base.Hit(newCard);
        }

        public void HandleBlackJackAction() {
            while (canHit) {
                return;
            }
        }

        public override string ToString() {
            return "DEALER";
        }
    }
}
