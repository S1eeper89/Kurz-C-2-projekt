using System.Collections.Generic;
using RPGGame.Models;

namespace RPGGame.Core
{
    public class GameState
    {
        public Player Player { get; set; }
        public (int X, int Y) PlayerPosition { get; set; }
        public List<MonsterState> Monsters { get; set; }
        public List<ItemState> Items { get; set; }
        public int MapWidth { get; set; }
        public int MapHeight { get; set; }
    }

    // Pomocné třídy pro ukládání stavů monster a itemů:
    public class MonsterState
    {
        public string Name { get; set; }
        public int Health { get; set; }
        public int MaxHealth { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
    }

    public class ItemState
    {
        public string Type { get; set; } // např. "Potion", "Weapon"
        public string Name { get; set; }
        public string Description { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        // další pole podle typu itemu (např. HealAmount, AttackBonus)
        public int? HealAmount { get; set; }
        public int? AttackBonus { get; set; }
    }
}