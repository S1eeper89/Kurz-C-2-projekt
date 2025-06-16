namespace C_2_VS__Projekt
{
    public enum TileBaseType
    {
        Empty,
        Road
    }

    public enum TileContentType
    {
        None,
        Player,
        Enemy,
        Item,
        Goal
    }

    public class Tile
    {
        public TileBaseType Base { get; set; } = TileBaseType.Road;
        public TileContentType Content { get; set; } = TileContentType.None;
        public Entity Occupant { get; set; } = null;
        public bool IsWalkable => true; // Logiku pohybu řešíme jinde podle obsahu
    }
}
