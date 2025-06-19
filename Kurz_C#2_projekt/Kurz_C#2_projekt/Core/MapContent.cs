using RPGGame.Models;

namespace RPGGame.Core
{
    public class MapContent
    {
        public List<(int X, int Y, Monster Monster)> Monsters { get; set; } = new();
        public List<(int X, int Y, Item Item)> Items { get; set; } = new();

        public void AddMonster(int x, int y, Monster monster)
        {
            Monsters.Add((x, y, monster));
        }

        public void AddItem(int x, int y, Item item)
        {
            Items.Add((x, y, item));
        }
    }
}
