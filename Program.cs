// See https://aka.ms/new-console-template for more information

using BlackJack;
using BlackJack.BicycleCards;
using BlackJack.GameLogic;
using BlackJack.Participant;
using BlackJack.Printing;
using System.Diagnostics;

bool debuggingGameManager = true;
bool debuggingUIHandler = false;


if (debuggingUIHandler) {
    UIMoneyDrawer.UpdateBalance(20000000, 6500);
    
}

Console.WriteLine();
Console.WriteLine();
Console.WriteLine();
Console.WriteLine();
Console.WriteLine();
if (debuggingGameManager) {
    GameManager gm = new GameManager();

    gm.PhaseHandler();
}