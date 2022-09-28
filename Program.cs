// See https://aka.ms/new-console-template for more information
using BlackJack.BicycleCards;


Deck deck = new Deck();
//deck.PrintDebug();


Card card1 = deck.cards[0];     // Ace
Card card2 = deck.cards[10];    // Jack
Card card3 = deck.cards[2];     // 3
Card card4 = deck.cards[13];    // Ace



Console.WriteLine(card1);
Console.WriteLine(card2);
Console.WriteLine(card3);
Console.WriteLine(card4);
var hehe = Card.CalculateCardValue(card1, card2, card3, card4);
var hehe2 = Card.CalculateCardValue(card1, card3, card4);

Console.WriteLine(hehe);
Console.WriteLine(hehe2);