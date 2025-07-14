using RPGGame.Models;
using System.Collections.Generic;

namespace RPGGame.Core
{
    /// <summary>
    /// Ukládá kompletní stav hry včetně hráče, pozice, monster, předmětů a rozměrů mapy.
    /// Slouží pro ukládání a načítání hry.
    /// </summary>
    public class GameState
    {
        internal (int X, int Y) PlayerPosition;

        /// <summary>
        /// Hráč a jeho aktuální stav.
        /// </summary>
        public Player Player { get; set; }

        /// <summary>
        /// Pozice hráče na mapě (souřadnice X, Y).
        /// </summary>
        public class position
        {
            public int X { get; private set; }
            public int Y { get; private set; }
        }
        
        /// <summary>
        /// Seznam stavů všech monster na mapě.
        /// </summary>
        public List<MonsterState> Monsters { get; set; }

        /// <summary>
        /// Seznam stavů všech předmětů na mapě.
        /// </summary>
        public List<ItemState> Items { get; set; }

        /// <summary>
        /// Šířka mapy.
        /// </summary>
        public int MapWidth { get; set; }

        /// <summary>
        /// Výška mapy.
        /// </summary>
        public int MapHeight { get; set; }
    }

    /// <summary>
    /// Uchovává informace o jednom monstru pro potřeby ukládání a načítání.
    /// </summary>
    public class MonsterState
    {
        public string Name { get; set; }
        public int Health { get; set; }
        public int MaxHealth { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public Position Position { get; set; }
    }

    /// <summary>
    /// Uchovává informace o jednom předmětu pro potřeby ukládání a načítání.
    /// </summary>
    public class ItemState
    {
        public string Type { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Position Position { get; set; }
        public int? HealAmount { get; set; }
        public int? AttackBonus { get; set; }
    }
}
