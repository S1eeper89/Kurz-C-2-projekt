using System;
using RPGGame.Core;
using RPGGame.Models;

namespace RPGGame.UI
{
    public static class MainMenu
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
                        var player = new Player("Hráč", 100, 20, 10);
                        new GameSimulation(player).GameLoop();
                        break;
                    case "2":
                        var (p, pos) = SaveLoadManager.LoadGame("save.json");
                        new GameSimulation(p).GameLoop(); // případně poslat i pozici do MapManageru
                        break;
                    case "3":
                        running = false;
                        break;
                }
            }
        }
    }
}
