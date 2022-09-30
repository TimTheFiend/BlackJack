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

        public bool isBust => hand.isBust;
        public int getHandValue => hand.getTotalHandValue;
        //Redundant
        public List<Card> getHand => hand.getHand;

        

        /// <summary>
        /// Re-initialises <see cref="hand"/> and <see cref="cards"/>.
        /// </summary>
        public void InitialiseForNewRound() {
            hand = new Hand();
            //cards = new List<Card>();
        }

        public virtual BlackJackAction HandlePlayerTurn() {
            throw new NotImplementedException();
        }

        //TODO
        public virtual void Hit(Card newCard) {
            hand.AddCard(newCard);
        }

        public void DebugPrintHand() {
            hand.DebugPrint();
        }

        //TODO: Need implementation of GameLogic/Manager class
        public void Stand() {
            
        }

        public void EmptyHand() {
            hand = new Hand();
        }

        public override string ToString() {

            return "UNNAMED BASEPLAYER";
        }
    }
}
