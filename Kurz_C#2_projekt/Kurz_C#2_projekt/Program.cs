using RPGGame.Core;
using System;

namespace RPGGame
{
    class Program
    {
        static void Main(string[] args)
        {
            bool running = true;

            while (running)
            {
                Console.Clear();
                Console.WriteLine("RPG Hra - Hlavní menu");
                Console.WriteLine("1) Nová hra");
                Console.WriteLine("2) Načíst hru");
                Console.WriteLine("3) Ukončit hru");
                Console.Write("Vyber možnost: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        StartNewGame();
                        break;
                    case "2":
                        LoadGame();
                        break;
                    case "3":
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Neplatná volba. Stiskněte klávesu...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        static void StartNewGame()
        {
            GameSimulation gameSimulation = new GameSimulation();
            gameSimulation.Run();
        }

        static void LoadGame()
        {
            // Implementace načítání hry z uloženého souboru
            Console.WriteLine("Funkce načítání hry ještě není implementována.");
            Console.ReadKey();
        }
    }
}