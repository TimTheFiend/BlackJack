using BlackJack.BicycleCards;
using BlackJack.GameLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{
    public class TheHouse
    {
        public bool IsPlayerTurn { get; private set; } = true;
        private GamePhase phase { get; set; } = GamePhase.NONE;
        private Deck deck;



        public TheHouse() {
            deck = new Deck();
        }

    }
}
