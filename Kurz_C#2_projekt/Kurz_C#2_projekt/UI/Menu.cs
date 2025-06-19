using System;
using RPGGame.Models;
using RPGGame.Core;

namespace RPGGame.UI
{
    public static class Menu
    {
        public static void Show()
        {
            bool running = true;
            while (running)
            {
                Console.Clear();
                Console.WriteLine("1) Nová hra");
                Console.WriteLine("2) Načíst hru");
                Console.WriteLine("3) Konec");
                Console.Write("Vyber možnost: ");
                switch (Console.ReadLine())
                {
                    case "1":
                        Console.WriteLine("Zadej jméno:");
                        var name = Console.ReadLine();
                        var player = new Player(name, 100, 20, 10);
                        new GameSimulation(player).GameLoop();
                        break;
                    case "2":
                        var state = SaveLoadManager.LoadGame("save.json");
                        var loadedPlayer = state.Player;
                        new GameSimulation(loadedPlayer, state).GameLoop();
                        break;
                    case "3":
                        running = false;
                        break;
                }
            }
        }
    }
}
