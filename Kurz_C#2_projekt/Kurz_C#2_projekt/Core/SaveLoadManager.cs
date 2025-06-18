using System.IO;
using System.Text.Json;
using RPGGame.Models;
using RPGGame.Core;

namespace RPGGame.Core
{
    public static class SaveLoadManager
    {
        public static void SaveGame(string filePath, Player player, MapManager mapManager)
        {
            var gameState = new
            {
                Player = player,
                PlayerPosition = mapManager.PlayerPosition
            };
            string jsonString = JsonSerializer.Serialize(gameState);
            File.WriteAllText(filePath, jsonString);
        }

        public static (Player, (int X, int Y)) LoadGame(string filePath)
        {
            string jsonString = File.ReadAllText(filePath);
            var gameState = JsonSerializer.Deserialize<dynamic>(jsonString);
            var player = JsonSerializer.Deserialize<Player>(gameState.GetProperty("Player").ToString());
            var position = gameState.GetProperty("PlayerPosition").Deserialize<(int X, int Y)>();
            return (player, position);
        }
    }
}