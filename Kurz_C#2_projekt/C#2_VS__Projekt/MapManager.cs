using System;

namespace C_2_VS__Projekt
{
    public class MapManager
    {
        private const int Width = 10;
        private const int Height = 10;
        private Tile[,] grid = new Tile[Width, Height];

        public (int X, int Y) PlayerPosition { get; private set; }

        public MapManager()
        {
            InitMap();
        }

        public void InitMap()
        {
            for (int y = 0; y < Height; y++)
                for (int x = 0; x < Width; x++)
                    grid[x, y] = new Tile { Base = TileBaseType.Road, Content = TileContentType.None };

            PlayerPosition = (0, 0);
            grid[0, 0].Content = TileContentType.Player;

            grid[9, 9].Content = TileContentType.Goal;

            grid[2, 3].Content = TileContentType.Enemy;
            grid[4, 6].Content = TileContentType.Enemy;
            grid[7, 2].Content = TileContentType.Enemy;

            grid[1, 1].Content = TileContentType.Item;
            grid[3, 5].Content = TileContentType.Item;
            grid[8, 8].Content = TileContentType.Item;
        }

        public void Render()
        {
            Console.Clear();
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    var tile = grid[x, y];
                    Console.Write(tile.Content switch
                    {
                        TileContentType.None => ".",
                        TileContentType.Player => "P",
                        TileContentType.Enemy => "E",
                        TileContentType.Item => "I",
                        TileContentType.Goal => "X",
                        _ => "?"
                    });
                }
                Console.WriteLine();
            }
        }

        public void MovePlayer(Direction direction)
        {
            var (x, y) = PlayerPosition;
            int newX = x, newY = y;

            switch (direction)
            {
                case Direction.North: newY--; break;
                case Direction.South: newY++; break;
                case Direction.West: newX--; break;
                case Direction.East: newX++; break;
            }

            if (newX < 0 || newY < 0 || newX >= Width || newY >= Height)
            {
                Console.WriteLine("Nemůžeš jít tímto směrem!");
                return;
            }

            var destination = grid[newX, newY];
            if (!destination.IsWalkable)
            {
                Console.WriteLine("Cesta je zablokovaná!");
                return;
            }

            // Reakce na obsah cílové dlaždice
            switch (destination.Content)
            {
                case TileContentType.Enemy:
                    Console.WriteLine("Nalezen nepřítel! Probíhá souboj...");
                    // TODO: Spustit souboj – dočasná simulace
                    Console.WriteLine("Nepřítel poražen!");
                    break;

                case TileContentType.Item:
                    Console.WriteLine("Našel jsi předmět! Přidán do inventáře.");
                    // TODO: Přidat do inventáře hráče
                    break;

                case TileContentType.Goal:
                    Console.WriteLine("Našel jsi princeznu! Vyhrál jsi hru!");
                    Environment.Exit(0);
                    break;
            }

            grid[x, y].Content = TileContentType.None;
            destination.Content = TileContentType.Player;
            PlayerPosition = (newX, newY);
        }
    }
}