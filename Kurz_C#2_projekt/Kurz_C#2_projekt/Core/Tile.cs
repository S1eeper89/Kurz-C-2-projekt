using RPGGame.Models;

namespace RPGGame.Core
{
    /// <summary>
    /// Určuje, jaký obsah má jedno políčko na mapě.
    /// </summary>
    public enum TileType
    {
        Empty,  // Prázdné pole
        Road,   // Průchozí dlaždice (momentálně nevyužito)
        Enemy,  // Políčko s nepřítelem (momentálně nevyužito)
        Item,   // Políčko s předmětem
        Goal    // Cíl/exit
    }

    /// <summary>
    /// Jeden čtverec mapy. Může obsahovat hráče, entitu i předmět.
    /// </summary>
    public class Tile
    {
        public TileType Type { get; set; } = TileType.Empty;
        public bool HasPlayer { get; set; }
        public Entity Occupant { get; set; } // Může být null nebo instance Monster

        // Změna: přidán konstruktor pro čistější inicializaci v mapě (nepovinné)
        public Tile() { }
    }
}
