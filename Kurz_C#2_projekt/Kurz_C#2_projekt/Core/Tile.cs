using RPGGame.Models;

namespace RPGGame.Core
{
    /// <summary>Typ obsahu jednotlivého políčka na mapě.</summary>
    public enum TileType
    {
        Empty,  // prázdné pole
        Road,   // průchozí dlaždice
        Enemy,  // políčko s nepřítelem
        Item,   // políčko s předmětem
        Goal    // cíl/exit
    }

    public class Tile
    {
        public TileType Type { get; set; } = TileType.Empty;
        public bool HasPlayer { get; set; }
        public Entity Occupant { get; set; }
    }
}
