// See https://aka.ms/new-console-template for more information

using BlackJack;
using BlackJack.BicycleCards;
using BlackJack.Participant;
using BlackJack.Printing;

Hand hand = new Hand();

Deck deck = new Deck();
deck.ShuffleDeck();

TheHouse daHouse = new TheHouse();

while (daHouse.isDeckPlayable) {
    daHouse.DealerPlayLoop();
    daHouse.dealer.EmptyHand();
    daHouse.player.EmptyHand();
    Console.WriteLine("\n");
}
daHouse.DealerPlayLoop();