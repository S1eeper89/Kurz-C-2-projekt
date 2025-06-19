using System;
using RPGGame.Models;
using RPGGame.Core;

namespace RPGGame.UI
{
    /// <summary>
    /// Hlavní menu hry. Nabízí spuštění nové hry, načtení uložené hry nebo ukončení.
    /// </summary>
    public static class Menu
    {
        /// <summary>
        /// Zobrazí hlavní menu a zpracuje výběr uživatele.
        /// </summary>
        public static void MainMenu()
        {
            bool running = true;
            while (running)
            {
                Console.Clear();
                Console.WriteLine("1) Nová hra");
                Console.WriteLine("2) Načíst hru");
                Console.WriteLine("3) Konec");
                Console.Write("Vyber možnost: ");

                var input = Console.ReadKey(true).Key;
                switch (input)
                {
                    case ConsoleKey.D1:
                    case ConsoleKey.NumPad1:
                        Console.Clear();
                        Console.WriteLine("Zadej jméno:");
                        var name = Console.ReadLine();
                        var player = new Player(name, 100, 20, 10);
                        new GameSimulation(player).GameLoop();
                        break;
                    case ConsoleKey.D2:
                    case ConsoleKey.NumPad2:
                        var state = SaveLoadManager.LoadGame("save.xml");
                        if (state == null)
                        {
                            Console.WriteLine("Načtení hry selhalo. Stiskni prosím klávesu pro návrat do menu");
                            Console.ReadKey();
                        }
                        else
                        {
                            var loadedPlayer = state.Player;
                            new GameSimulation(loadedPlayer, state).GameLoop();
                        }
                        break;
                    case ConsoleKey.D3:
                    case ConsoleKey.NumPad3:
                        running = false;
                        break;
                }
            }
        }
    }
}
