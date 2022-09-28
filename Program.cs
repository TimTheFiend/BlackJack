// See https://aka.ms/new-console-template for more information
using BlackJack;


Deck deck = new Deck();
//deck.PrintDebug();

while (deck.cards.Count > 0) {
ConsoleWriter.WriteCard(deck.DrawCard());
}



//// OLD Test
//Card newCard = new Card(CardSuit.Hearts, CardValue.Jack);
//Card newCard2 = new Card(CardSuit.Hearts, CardValue.Jack, false);
//Console.WriteLine(newCard.getSuitAndValue);
//Console.WriteLine(newCard.getValueAndSuit);
//Console.WriteLine(newCard2.getSuitAndValue);
//Console.WriteLine(newCard2.getValueAndSuit);

//ConsoleWriter.Writeline("Hello world");

//List<CardPrintout> printouts = new List<CardPrintout>();
//printouts.Add(CardPrintout.Printout("Hello"));
//printouts.Add(CardPrintout.PrintoutCard("World", CardPrintout.CardRed));
//printouts.Add(CardPrintout.Printout("paaaaaaaaaaaaaaaaaaaaaaaaaaaain"));
//printouts.Add(CardPrintout.PrintoutCard("haha jk", CardPrintout.CardBlack));
//Console.WriteLine();
//Console.WriteLine();
//Console.WriteLine();
//Console.WriteLine();
//ConsoleWriter.Writeline(printouts.ToArray());