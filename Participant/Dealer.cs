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

        /// <summary>
        /// <see cref="Dealer"/> will keep hitting until hand reaches <see cref="minValueStand"/> value.
        /// </summary>
        /// <returns><c>true</c> if hand below <see cref="minValueStand"/>; otherwise <c>false</c>.</returns>
        public override BlackJackAction HandlePlayerTurn() {
            return canHit ? BlackJackAction.HIT : BlackJackAction.STAND;
        }

        /// <summary>
        /// Returns <c>true</c> if the second card in <see cref="Hand"/> is an <see cref="CardValue.Ace"/>; otherwise returns <c>false</c>.
        /// </summary>
        public bool CanInsurance => hand.CanInsurance;

        /// <summary>
        /// Sets the first card in <see cref="Hand"/> to be facedown.
        /// </summary>
        /// <param name="newCard">The drawn card.</param>
        public override void Hit(Card newCard) {
            if (hand.handSize == 0) {
                newCard.SetFaceDown();
            }
            base.Hit(newCard);
        }

        public override string ToString() {
            return "DEALER";
        }
    }
}
