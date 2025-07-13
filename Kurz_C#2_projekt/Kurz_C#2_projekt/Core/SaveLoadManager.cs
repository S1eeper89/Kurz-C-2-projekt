using RPGGame.Models;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Xml.Serialization;

namespace RPGGame.Core
{
    /// <summary>
    /// Zajišťuje ukládání a načítání stavu hry ze souboru.
    /// </summary>
    public static class SaveLoadManager
    {
        /// <summary>
        /// Uloží aktuální stav hry do zadaného souboru.
        /// </summary>
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
                    Position = new Position(m.Position.X, m.Position.Y),
                }).ToList(),
                Items = mapManager.GetItems().Select(i => new ItemState
                {
                    Type = i.Item is Potion ? "Potion" : "Weapon",
                    Name = i.Item.Name,
                    Description = i.Item.Description,
                    Position = new Position(i.Position.X, i.Position.Y),
                    HealAmount = (i.Item as Potion)?.HealAmount,
                    AttackBonus = (i.Item as Weapon)?.AttackBonus
                }).ToList(),
                MapWidth = mapManager.Width,
                MapHeight = mapManager.Height
            };

            try
            {
                var serializer = new XmlSerializer(typeof(GameState));
                using var writer = new StreamWriter(filePath);
                serializer.Serialize(writer, state);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Chyba při ukládání: {ex.Message}");
            }
        }

        /// <summary>
        /// Načte stav hry ze zadaného souboru a vrátí jej ve formě objektu GameState.
        /// </summary>
        public static GameState LoadGame(string filePath)
        {
            try //původně jsem zvažoval exception a možná by byl na místě ale pokud je prázdný mělo by to snad stačit
            {
                var serializer = new XmlSerializer(typeof(GameState));
                using (var reader = new StreamReader(filePath))
                { 
                    return (GameState)serializer.Deserialize(reader);
                }
            }
            //catch (FileNotFoundException)
            //{
            //    Console.WriteLine("Soubor s ulo6enou hrou nebzl naleyen.");
            //    return null;
            //}
            catch (Exception ex)
            {
                {
                    Console.Clear();
                    Console.WriteLine($"\nChyba při načítání uložené hry: {ex.Message}");
                    return null;
                }
            }
        }
    }
}
