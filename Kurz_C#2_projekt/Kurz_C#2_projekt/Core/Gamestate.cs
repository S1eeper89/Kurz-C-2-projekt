using System.Collections.Generic;
using RPGGame.Models;

namespace RPGGame.Core
{
    /// <summary>
    /// Ukládá kompletní stav hry včetně hráče, pozice, monster, předmětů a rozměrů mapy.
    /// Slouží pro ukládání a načítání hry.
    /// </summary>
    public class GameState
    {
        /// <summary>
        /// Hráč a jeho aktuální stav.
        /// </summary>
        public Player Player { get; set; }

        /// <summary>
        /// Pozice hráče na mapě (souřadnice X, Y).
        /// </summary>
        public (int X, int Y) PlayerPosition { get; set; } //xxxxxx // nahradit za public class       

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
        public int X { get; set; }
        public int Y { get; set; }
    }

    /// <summary>
    /// Uchovává informace o jednom předmětu pro potřeby ukládání a načítání.
    /// </summary>
    public class ItemState
    {
        public string Type { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int? HealAmount { get; set; }
        public int? AttackBonus { get; set; }
    }
}
