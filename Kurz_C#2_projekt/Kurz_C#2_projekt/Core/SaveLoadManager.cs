using System.IO;
using System.Text.Json;
using System.Collections.Generic;
using RPGGame.Models;

namespace RPGGame.Core
{
    public static class SaveLoadManager
    {
        public static void SaveGame(string filePath, Player player, MapManager mapManager)
        {
            var state = new GameState
            {
                Player = player,
                PlayerPosition = mapManager.PlayerPosition,
                Monsters = mapManager.GetMonsters().Select(m => new MonsterState
                {
                    Name = m.Monster.Name,
                    Health = m.Monster.Health,
                    MaxHealth = m.Monster.MaxHealth,
                    Attack = m.Monster.Attack,
                    Defense = m.Monster.Defense,
                    X = m.Position.X,
                    Y = m.Position.Y
                }).ToList(),
                Items = mapManager.GetItems().Select(i => new ItemState
                {
                    Type = i.Item is Potion ? "Potion" : "Weapon",
                    Name = i.Item.Name,
                    Description = i.Item.Description,
                    X = i.Position.X,
                    Y = i.Position.Y,
                    HealAmount = (i.Item as Potion)?.HealAmount,
                    AttackBonus = (i.Item as Weapon)?.AttackBonus
                }).ToList(),
                MapWidth = mapManager.Width,
                MapHeight = mapManager.Height
            };

            File.WriteAllText(filePath, JsonSerializer.Serialize(state, new JsonSerializerOptions { WriteIndented = true }));
        }


        public static GameState LoadGame(string filePath)
        {
            string jsonString = File.ReadAllText(filePath);
            var state = JsonSerializer.Deserialize<GameState>(jsonString);
            return state;
        }
    }
}