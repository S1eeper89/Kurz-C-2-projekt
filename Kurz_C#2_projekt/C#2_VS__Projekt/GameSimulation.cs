using System;
using System.IO;

namespace C_2_VS__Projekt
{
    public static class GameSimulation
    {
        private const string SaveFile = "savegame.json";

        public static void Run()
        {
            while (true)
            {
                Console.WriteLine("1) Nová hra");
                Console.WriteLine("2) Načíst hru");
                Console.WriteLine("3) Konec");
                Console.Write("Vyber možnost: ");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1": StartNewGame(); return;
                    case "2": LoadGame(); return;
                    case "3": return;
                    default: Console.WriteLine("Neplatná volba, zkus to znovu.\n"); break;
                }
            }
        }

        private static void StartNewGame()
        {
            Console.Write("Zadej své jméno: ");
            var playerName = Console.ReadLine();

            Console.WriteLine("Vyber obtížnost: 1=Easy, 2=Normal, 3=Hard");
            Difficulty difficulty;
            while (true)
            {
                var input = Console.ReadLine();
                if (input == "1") { difficulty = Difficulty.Easy; break; }
                if (input == "2") { difficulty = Difficulty.Normal; break; }
                if (input == "3") { difficulty = Difficulty.Hard; break; }
                Console.WriteLine("Zadej 1, 2 nebo 3.");
            }

            var player = new Player(100, 20, 5) { Name = playerName, Level = (int)difficulty };
            player.Save(SaveFile);
            Console.WriteLine("Hra vytvořena a uložena. Pokračuj hrou...\n");
            RunGameLoop(player);
        }

        private static void LoadGame()
        {
            if (!File.Exists(SaveFile))
            {
                Console.WriteLine("Žádná uložená hra nenalezena.\n");
                return;
            }
            var player = Player.Load(SaveFile);
            Console.WriteLine($"Načten hráč {player.Name}, úroveň {player.Level}, XP {player.Experience}.\n");
            RunGameLoop(player);
        }

        private static void RunGameLoop(Player player)
        {
            Console.WriteLine($"Vítej {player.Name}! Začni dobrodružství, úroveň: {player.Level}\n");
            var monster = new Monster(50, 10, 2);
            while (monster.IsAlive) player.Attack(monster);
            var gold = monster.RewardGold(); var xp = monster.RewardXP(); player.AddExperience(xp);
            Console.WriteLine($"Získal jsi {gold} zlaťáků a {xp} XP!\n");
            player.Save(SaveFile);
            Console.WriteLine("Postup uložen.");
        }
    }
}