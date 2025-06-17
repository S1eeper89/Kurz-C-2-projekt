using System;
using RPGGame.Models;
using RPGGame.Core;
using RPGGame.UI;

namespace RPGGame.Core
{
    /// <summary>
    /// Orchestrace herní smyčky: zpracování vstupu, vykreslení, inventář.
    /// </summary>
    public class GameSimulation
    {
        private Player _player;
        private MapManager _mapManager;
        private bool _isRunning;

        public GameSimulation(Player player)
        {
            _player = player;
            _mapManager = new MapManager(_player);
        }

        public void GameLoop()
        {
            _isRunning = true;
            while (_isRunning)
            {
                Console.Clear();
                _mapManager.RenderMap();
                Console.WriteLine($"HP: {_player.Health}/{_player.MaxHealth}  Útok: {_player.Attack}  Obrana: {_player.Defense}");
                Console.WriteLine("W/A/S/D = Pohyb | I = Inventář | Q = Konec");
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
                        _mapManager.MovePlayer(dir);
                        break;

                    case ConsoleKey.I:
                        InventoryView.Show(_player);
                        break;

                    case ConsoleKey.Q:
                        _isRunning = false;
                        break;
                }
            }
        }
    }
}