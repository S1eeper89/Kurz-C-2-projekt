using RPGGame.Core;
using RPGGame.Models;
using System;
using System.Collections.Generic;

namespace RPGGame.Core
{
    /// <summary>
    /// Spravuje mapu a interakci hráče s entitami a předměty.
    /// </summary>
    public class MapManager
    {
        private Tile[,] _grid;
        private Dictionary<(int, int), Item> _items;
        private Player _player;

        public (int X, int Y) PlayerPosition { get; private set; }

        // Konstruktor pro načtení stavu hry
        public MapManager(Player player, GameState state)
        {
            _player = player;
            _grid = new Tile[state.MapWidth, state.MapHeight];
            _items = new Dictionary<(int, int), Item>();

            // Inicializace gridu
            for (int y = 0; y < state.MapHeight; y++)
                for (int x = 0; x < state.MapWidth; x++)
                    _grid[x, y] = new Tile();

            // Nastav pozici hráče
            PlayerPosition = state.PlayerPosition;
            _grid[PlayerPosition.X, PlayerPosition.Y].HasPlayer = true;

            // Načti monstra
            foreach (var m in state.Monsters)
            {
                var monster = new Monster(m.Name, m.MaxHealth, m.Attack, m.Defense);
                monster.ReceiveDamage(m.MaxHealth - m.Health);
                _grid[m.X, m.Y].Occupant = monster;
            }

            // Načti itemy
            foreach (var i in state.Items)
            {
                Item item = null;
                if (i.Type == "Potion")
                    item = new Potion(i.Name, i.Description, i.HealAmount ?? 0);
                else if (i.Type == "Weapon")
                    item = new Weapon(i.Name, i.Description, i.AttackBonus ?? 0);

                if (item != null)
                {
                    _items[(i.X, i.Y)] = item;
                    _grid[i.X, i.Y].Type = TileType.Item;
                }
            }
        }
        public MapManager(Player player)
        {
            _player = player;
            // Nastav základní velikost mapy (např. 10x10, jak bylo v původním kódu)
            int width = 10, height = 10;
            _grid = new Tile[width, height];
            _items = new Dictionary<(int, int), Item>();

            // Inicializace gridu
            for (int y = 0; y < height; y++)
                for (int x = 0; x < width; x++)
                    _grid[x, y] = new Tile();

            // Startovní pozice hráče
            PlayerPosition = (0, 0);
            _grid[0, 0].HasPlayer = true;

            // Cíl
            _grid[width - 1, height - 1].Type = TileType.Goal;

            // Předměty
            PlaceItem(1, 1, new Potion("Lektvar zdraví", "Obnoví 20 HP", 20));
            PlaceItem(3, 5, new Potion("Mega lektvar", "Obnoví 50 HP", 50));

            // Monstra
            _grid[2, 3].Occupant = new Monster("Goblin", 30, 10, 2);
            _grid[4, 6].Occupant = new Monster("Ork", 50, 12, 4);
        }

        public MapManager(Player player, MapContent content, int width = 10, int height = 10)
        {
            _player = player;
            _grid = new Tile[width, height];

            for (int y = 0; y < height; y++)
                for (int x = 0; x < width; x++)
                    _grid[x, y] = new Tile();

            PlayerPosition = (0, 0);
            _grid[PlayerPosition.X, PlayerPosition.Y].HasPlayer = true;

            foreach (var (x, y, monster) in content.Monsters)
                _grid[x, y].Occupant = monster;

            foreach (var (x, y, item) in content.Items)
            {
                _items[(x, y)] = item;
                _grid[x, y].Type = TileType.Item;
            }
        }

        private void PlaceItem(int x, int y, Item item)
        {
            _items[(x, y)] = item;
            _grid[x, y].Type = TileType.Item;
        }
        public IEnumerable<(Monster Monster, (int X, int Y) Position)> GetMonsters()
        {
            for (int y = 0; y < _grid.GetLength(1); y++)
            {
                for (int x = 0; x < _grid.GetLength(0); x++)
                {
                    if (_grid[x, y].Occupant is Monster monster)
                        yield return (monster, (x, y));
                }
            }
        }

        public IEnumerable<(Item Item, (int X, int Y) Position)> GetItems()
        {
            foreach (var kvp in _items)
            {
                yield return (kvp.Value, kvp.Key);
            }
        }

        // Property pro šířku a výšku mapy
        public int Width => _grid.GetLength(0);
        public int Height => _grid.GetLength(1);

    }
}
