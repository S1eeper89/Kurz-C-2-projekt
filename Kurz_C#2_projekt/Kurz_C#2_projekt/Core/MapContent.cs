using RPGGame.Models;

namespace RPGGame.Core
{
    /// <summary>
    /// Uchovává definici obsahu mapy – seznam monster a předmětů včetně jejich pozic.
    /// </summary>
    public class MapContent
    {
        /// <summary>
        /// Seznam monster na mapě a jejich pozice.
        /// </summary>
        public List<(int X, int Y, Monster Monster)> Monsters { get; set; } = new();

        /// <summary>
        /// Seznam předmětů na mapě a jejich pozice.
        /// </summary>
        public List<(int X, int Y, Item Item)> Items { get; set; } = new();

        /// <summary>
        /// Přidá monstrum na určenou pozici.
        /// </summary>
        public void AddMonster(int x, int y, Monster monster)
        {
            Monsters.Add((x, y, monster));
        }

        /// <summary>
        /// Přidá předmět na určenou pozici.
        /// </summary>
        public void AddItem(int x, int y, Item item)
        {
            Items.Add((x, y, item));
        }
    }
}
