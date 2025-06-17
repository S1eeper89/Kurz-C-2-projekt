using System;
using System.Collections.Generic;
using RPGGame.Models;
using RPGGame.Core;

namespace RPGGame.Core
{
    /// <summary>
    /// Spravuje mapu a interakci hráče s entitami a předměty.
    /// </summary>
    public class MapManager
    {
        private const int Width = 10;
        private const int Height = 10;
        private Tile[,] _grid;
        private Dictionary<(int X, int Y), Item> _items;

        public (int X, int Y) PlayerPosition { get; private set; }
        private Player _player;

        public MapManager(Player player)
        {
            _player = player;
            _grid = new Tile[Width, Height];
            _items = new Dictionary<(int, int), Item>();
            InitMap();
        }

        private void InitMap()
        {
            // Inicializace mapy
            for (int y = 0; y < Height; y++)
                for (int x = 0; x < Width; x++)
                    _grid[x, y] = new Tile();

            // Startovní pozice hráče
            PlayerPosition = (0, 0);
            _grid[0, 0].HasPlayer = true;

            // Cíl
            _grid[9, 9].Type = TileType.Goal;

            // Předměty
            PlaceItem(1, 1, new Potion("Lektvar zdraví", "Obnoví 20 HP", 20));
            PlaceItem(3, 5, new Potion("Mega lektvar", "Obnoví 50 HP", 50));

            // Monstra
            _grid[2, 3].Occupant = new Monster("Goblin", 30, 10, 2);
            _grid[4, 6].Occupant = new Monster("Ork", 50, 12, 4);
        }

        private void PlaceItem(int x, int y, Item item)
        {
            _items[(x, y)] = item;
            _grid[x, y].Type = TileType.Item;
        }

        public void RenderMap()
        {
            Console.Clear();
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    var cell = _grid[x, y];
                    char symbol = cell.Type switch
                    {
                        TileType.Goal => 'X',
                        TileType.Item => 'I',
                        _ when cell.HasPlayer => 'P',
                        _ when cell.Occupant != null => 'E',
                        _ => '.'
                    };
                    Console.Write(symbol);
                }
                Console.WriteLine();
            }
        }

        public void MovePlayer(Direction dir)
        {
            var (x, y) = PlayerPosition;
            int newX = x, newY = y;
            switch (dir)
            {
                case Direction.North: newY--; break;
                case Direction.South: newY++; break;
                case Direction.West: newX--; break;
                case Direction.East: newX++; break;
            }
            if (newX < 0 || newY < 0 || newX >= Width || newY >= Height)
                return;

            // Odstranění hráče z původní pozice
            _grid[x, y].HasPlayer = false;

            // Interakce na nové pozici
            var targetCell = _grid[newX, newY];

            // Souboj
            if (targetCell.Occupant is Monster monster)
            {
                monster.ReceiveDamage(_player.Attack);
                if (!monster.IsAlive)
                {
                    Console.WriteLine($"Zabil jsi {monster.Name}!");
                    targetCell.Occupant = null;
                }
                else
                {
                    _player.ReceiveDamage(monster.Attack);
                    Console.WriteLine($"{monster.Name} útočí a bere ti {monster.Attack - _player.Defense} HP!");
                }
                Console.ReadKey();
            }
            // Seber položku
            else if (_items.TryGetValue((newX, newY), out var item))
            {
                Console.WriteLine($"Našel jsi předmět: {item.Name} - {item.Description}");
                _player.AddItemToInventory(item);
                _items.Remove((newX, newY));
                Console.ReadKey();
            }
            // Cíl
            else if (targetCell.Type == TileType.Goal)
            {
                Console.WriteLine("Našel jsi cíl! Vyhrál jsi!");
                Environment.Exit(0);
            }

            // Nastavení hráče na novou pozici
            PlayerPosition = (newX, newY);
            _grid[newX, newY].HasPlayer = true;
        }
    }
}