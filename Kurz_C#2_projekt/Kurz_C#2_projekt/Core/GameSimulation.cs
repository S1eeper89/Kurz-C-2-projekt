using RPGGame.Models;
using RPGGame.UI;
using System;

namespace RPGGame.Core
{
    public class GameSimulation
    {
        private Player _player;
        private MapManager _mapManager;
        private bool _isRunning;

        public GameSimulation(Player player)
        {
            _player = player;
            var content = new MapContent();
            content.AddMonster(2, 3, new Monster("Goblin", 30, 10, 2));
            content.AddMonster(4, 6, new Monster("Ork", 50, 12, 4));
            content.AddItem(1, 1, new Potion("Lektvar zdraví", "Obnoví 20 HP", 20));
            content.AddItem(3, 5, new Potion("Mega lektvar", "Obnoví 50 HP", 50));
            content.AddItem(6, 6, new Weapon("Meč Drakobijec", "Přidá 50 Útok", 50));
            content.AddMonster(7, 7, new Monster("Drak Smak", 50, 99, 10));
            _mapManager = new MapManager(_player, content);
        }

        public GameSimulation(Player player, GameState state)
        {
            _player = player;
            _mapManager = new MapManager(_player, state);
        }

        public void GameLoop()
        {
            _isRunning = true;
            while (_isRunning)
            {
                _mapManager.RenderMap();
                Console.WriteLine($"Player: {_player.Name} HP: {_player.Health}/{_player.MaxHealth}  Útok: {_player.Attack}  Obrana: {_player.Defense}");
                Console.WriteLine("W/A/S/D = Pohyb | I = Inventář | T = Uložit | L = Načíst | M = Menu | Q = Konec");
                var key = Console.ReadKey(true).Key;

                switch (key)
                {
                    case ConsoleKey.W:
                    case ConsoleKey.A:
                    case ConsoleKey.S:
                    case ConsoleKey.D:
                        var dir = key switch
                        {
                            ConsoleKey.W => Direction.North,
                            ConsoleKey.S => Direction.South,
                            ConsoleKey.A => Direction.West,
                            ConsoleKey.D => Direction.East,
                            _ => throw new InvalidOperationException()
                        };
                        var moveResult = _mapManager.MovePlayer(dir);
                        if (moveResult == MoveResult.PlayerDied || (moveResult == MoveResult.PlayerEscaped && _player.Health <= 0))
                            ReturnToMenu();
                        break;

                    case ConsoleKey.I:
                        InventoryView.Show(_player);
                        break;
                    case ConsoleKey.T:
                        SaveLoadManager.SaveGame("save.json", _player, _mapManager);
                        Console.WriteLine("Hra byla uložena.");
                        Console.ReadKey();
                        break;
                    case ConsoleKey.L:
                        var state = SaveLoadManager.LoadGame("save.json");
                        _player = state.Player;
                        _mapManager = new MapManager(_player, state);
                        Console.WriteLine("Hra byla načtena.");
                        Console.ReadKey();
                        break;
                    case ConsoleKey.M:
                        ReturnToMenu();
                        break;
                    case ConsoleKey.Q:
                        _isRunning = false;
                        break;
                }
            }
        }

        /// <summary>
        /// Ukončí aktuální herní smyčku a vrátí se do menu.
        /// </summary>
        private void ReturnToMenu()
        {
            _isRunning = false;
        }
    }
}