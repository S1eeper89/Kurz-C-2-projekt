﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using RPGGame.Models;


namespace RPGGame.Core
{
    /// <summary>
    /// Výsledek pohybu hráče po mapě (pro GameSimulation).
    /// </summary>
    public enum MoveResult
    {
        None,
        PlayerDied,
        PlayerEscaped,
        PlayerWonFight,
        PlayerWonGame
    }

    /// <summary>
    /// Řídí mapu, pohyb hráče a veškerou interakci s entitami na mapě.
    /// </summary>
    public class MapManager
    {
        private Tile[,] _grid;
        private Dictionary<(int, int), Item> _items = new();
        private Player _player;

        /// <summary>
        /// Aktuální pozice hráče na mapě.
        /// </summary>
        public Position PlayerPosition { get; private set; }

        /// <summary>
        /// Šířka mapy.
        /// </summary>
        public int Width => _grid.GetLength(0);

        /// <summary>
        /// Výška mapy.
        /// </summary>
        public int Height => _grid.GetLength(1);

        /// <summary>
        /// Konstruktor mapy – nová hra.
        /// </summary>
        public MapManager(Player player, MapContent content, int width = 10, int height = 10)
        {
            _player = player;
            _grid = new Tile[width, height];

            for (int y = 0; y < height; y++)
                for (int x = 0; x < width; x++)
                    _grid[x, y] = new Tile();

            // Startovní pozice hráče
            PlayerPosition = new Position(0, 0);
            _grid[PlayerPosition.X, PlayerPosition.Y].HasPlayer = true;

            // Nastavení cíle
            _grid[width - 1, height - 1].Type = TileType.Goal;

            foreach (var (position, monster) in content.Monsters)
                _grid[position.X, position.Y].Occupant = monster;
                 
            foreach (var (position, item) in content.Items)
            {
                _items[(position.X, position.Y)] = item;
                _grid[position.X, position.Y].Type = TileType.Item;
            }
        }

        /// <summary>
        /// Konstruktor mapy – načtení uložené hry.
        /// </summary>
        public MapManager(Player player, GameState state)
        {
            _player = player;
            _grid = new Tile[state.MapWidth, state.MapHeight];
            _items = new Dictionary<(int, int), Item>();

            for (int y = 0; y < state.MapHeight; y++)
                for (int x = 0; x < state.MapWidth; x++)
                    _grid[x, y] = new Tile();

            PlayerPosition = state.PlayerPosition;


            _grid[PlayerPosition.X, PlayerPosition.Y].HasPlayer = true;
            // Nastavení cíle
            _grid[state.MapWidth - 1, state.MapHeight - 1].Type = TileType.Goal;

            // Monstra
            foreach (var m in state.Monsters)
            {
                var monster = new Monster(m.Name, m.MaxHealth, m.Attack, m.Defense);
                monster.ReceiveDamage(m.MaxHealth - m.Health);
                _grid[m.Position.X, m.Position.Y].Occupant = monster;
            }

            // Předměty
            foreach (var i in state.Items)
            {
                Item item = null;
                if (i.Type == "Potion")
                    item = new Potion(i.Name, i.Description, i.HealAmount ?? 0);
                else if (i.Type == "Weapon")
                    item = new Weapon(i.Name, i.Description, i.AttackBonus ?? 0);

                if (item != null)
                {
                    _items[(i.Position.X, i.Position.Y)] = item;
                    _grid[i.Position.X, i.Position.Y].Type = TileType.Item;
                }
            }
        }

        /// <summary>
        /// Vykreslí aktuální stav mapy do konzole.
        /// </summary>
        public void RenderMap()
        {
            Console.Clear();
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    if (PlayerPosition.X == x && PlayerPosition.Y == y)
                        Console.Write("P ");
                    else if (_grid[x, y].Occupant is Monster)
                        Console.Write("M ");
                    else if (_grid[x, y].Type == TileType.Item)
                        Console.Write("I ");
                    else if (_grid[x, y].Type == TileType.Goal)
                        Console.Write("G ");
                    else
                        Console.Write(". ");
                }
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Pokusí se přesunout hráče do zvoleného směru a provede všechny interakce.
        /// </summary>
        /// <returns>Výsledek tahu - pro návrat do menu ve GameSimulation.</returns>
        public MoveResult MovePlayer(Direction direction)
        {
            int newX = PlayerPosition.X;
            int newY = PlayerPosition.Y;
            switch (direction)
            {
                case Direction.North: newY--; break;
                case Direction.South: newY++; break;
                case Direction.West: newX--; break;
                case Direction.East: newX++; break;
            }

            if (newX >= 0 && newX < Width && newY >= 0 && newY < Height)
            {
                // Odznač předchozí pozici
                PlayerPosition = new Position(newX, newY);
                _grid[PlayerPosition.X, PlayerPosition.Y].HasPlayer = true;



                var tile = _grid[newX, newY];

                // 1. Encounter s monstrem
                if (tile.Occupant is Monster monster && monster.IsAlive)
                {
                    var survived = BattleManager.RunBattle(_player, monster);
                    if (!monster.IsAlive)
                    {
                        tile.Occupant = null;
                        Console.WriteLine($"Porazil jsi {monster.Name}!");
                        Console.ReadKey(true);
                        return MoveResult.PlayerWonFight;
                    }
                    if (!survived)
                    {
                        if (_player.Health <= 0)
                        {
                            Console.WriteLine("Byl jsi poražen!");
                            Console.ReadKey(true);
                            return MoveResult.PlayerDied;
                        }
                        else
                        {
                            Console.WriteLine("Utekl jsi z boje!");
                            Console.ReadKey(true);
                            return MoveResult.PlayerEscaped;
                        }
                    }
                }

                // 2. Seber předmět
                if (_items.ContainsKey((newX, newY)))
                {
                    var item = _items[(newX, newY)];
                    _player.AddItemToInventory(item);
                    _items.Remove((newX, newY));
                    tile.Type = TileType.Empty;
                    Console.WriteLine($"Sebral jsi předmět: {item.Name}.");
                    Console.ReadKey(true);
                }

                // 3. Cíl
                if (tile.Type == TileType.Goal)
                {
                    Console.Clear();
                    Console.WriteLine("GRATULUJI! Dostal ses do cíle!");
                    Console.ReadKey(true);
                    return MoveResult.PlayerWonGame;
                }
            }
            return MoveResult.None;
        }

        /// <summary>
        /// Vrátí všechny monstra na mapě.
        /// </summary>
        public IEnumerable<(Monster Monster, Position Position)> GetMonsters() =>
            _grid.Cast<Tile>().Select((tile, index) => (tile, index))
                .Where(t => t.tile.Occupant is Monster)
                .Select(t => ((Monster)t.tile.Occupant, new Position (t.index % Width, t.index / Width)));

        /// <summary>
        /// Vrátí všechny předměty na mapě.
        /// </summary>
        public IEnumerable<(Item Item, Position Position)> GetItems()

            => _items.Select(i => (i.Value, new Position(i.Key.Item1, i.Key.Item2)));
    }
}
