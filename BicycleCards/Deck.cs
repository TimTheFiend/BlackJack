﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackJack.Printing;

namespace BlackJack.BicycleCards
{
    public class Deck
    {
        public List<Card> cards { get; private set; }
        private int cardsLeft => cards.Count;
        public bool isReshuffleNeeded => cardsLeft < 20;  //Value is arbitrary. The most amount of cards a player can get is 11 without going over 21.

        public Deck()
        {
            GenerateDeck();
        }

        /// <summary>
        /// Initialises <see cref="cards"/>, and adds a standard deck of <see cref="Card"/>-object with a <see cref="CardValue"/> and <see cref="CardSuit"/>.
        /// </summary>
        private void GenerateDeck()
        {
            cards = new List<Card>();

            foreach (CardSuit suit in Enum.GetValues(typeof(CardSuit)))
            {
                foreach (CardValue value in Enum.GetValues(typeof(CardValue)))
                {
                    cards.Add(new Card(suit, value));
                }
            }
        }

        /// <summary>
        /// Fisher-Yates Shuffle algorithm.
        /// </summary>
        public void ShuffleDeck()
        {
            Random random = new Random();

            for (int i = 0; i < cards.Count - 1; i++)
            {
                int pos = random.Next(i, cards.Count);
                Card temp = cards[i];
                cards[i] = cards[pos];
                cards[pos] = temp;
            }
        }

        public void ReshuffleDeck() {
            GenerateDeck();
            //ShuffleDeck();
        }

        //Draws card 
        public Card DrawCard()
        {
            const int pos = 0;
            Card drawnCard = cards.ElementAt(pos);
            cards.RemoveAt(pos);

            UICardDrawer.DrawDeckSize(cardsLeft);

            return drawnCard;
        }


    }
}
