using BlackJack.BicycleCards;
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
        public List<Card> cards;

        public BasePlayer() {
            hand = new Hand();
            cards = new List<Card>();
        }

        public void InitialiseForNewRound() {
            hand = new Hand();
            cards = new List<Card>();
        }

        //TODO
        public bool Hit(Card newCard) {
            return false;
        }

        //TODO: Need implementation of GameLogic/Manager class
        public void Stand() {

        }
    }
}
