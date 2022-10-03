using BlackJack.BicycleCards;
using BlackJack.Printing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack.Participant
{
    public class BasePlayer
    {
        public Hand hand;
       

        public BasePlayer() {
            hand = new Hand();
            //cards = new List<Card>();
        }


        /// <summary>
        /// Returns <c>true</c> if <see cref="hand"/>'s total is above 21.
        /// </summary>
        public bool isBust => hand.isBust;

        /// <summary>
        /// Returns <see cref="hand"/>'s total value.
        /// </summary>
        public int getHandValue => hand.getTotalHandValue;
        
        /// <summary>
        /// Returns <see cref="Card"/> on <see cref="hand"/>.
        /// </summary>
        public List<Card> getHand => hand.getHand;
    
        
        /// <summary>
        /// Returns <c>true</c> if the dealt cards equals 21.
        /// </summary>
        public bool hasBlackjack => hand.hasBlackJack;


        /// <summary>
        /// Override is required.
        /// </summary>
        public virtual BlackJackAction HandlePlayerTurn() {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Adds a <see cref="Card"/> to <see cref="BasePlayer"/>'s <see cref="hand"/>.
        /// </summary>
        /// <param name="newCard"><see cref="Card"/> from <see cref="Deck"/></param>
        public virtual void Hit(Card newCard) {
            hand.AddCard(newCard);
        }


        /// <summary>
        /// Re-initialises <see cref="hand"/> and <see cref="cards"/>.
        /// </summary>
        public void EmptyHand() {
            hand = new Hand();
        }

        public override string ToString() {

            return "UNNAMED BASEPLAYER";
        }
    }
}
