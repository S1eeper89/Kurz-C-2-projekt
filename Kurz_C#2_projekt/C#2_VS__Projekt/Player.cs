using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace C_2_VS__Projekt
{
    public class Player : Entity
    {
        private readonly List<Item> _equipment = new();
        public int Experience { get; private set; }
        public string Name { get; set; }

        public Player(int health, int attack, int defense)
            : base(health, attack, defense) { }

        public Player() : base(0, 0, 0) { }

        public void Equip(Item item) => _equipment.Add(item);

        public override int CalculateDamage(Entity target)
        {
            var damage = base.CalculateDamage(target);
            foreach (var item in _equipment) damage += item.AttackBonus;
            return damage;
        }

        public void Attack(Entity target)
        {
            var dmg = CalculateDamage(target);
            target.ReceiveDamage(dmg);
        }

        public void AddExperience(int xp)
        {
            Experience += xp;
            var xpNeeded = Level * 100;
            if (Experience >= xpNeeded)
            {
                Experience -= xpNeeded;
                Level++;
                Console.WriteLine($"Level up! New level: {Level}");
            }
        }

        public void Save(string path)
        {
            var opts = new JsonSerializerOptions { WriteIndented = true };
            var json = JsonSerializer.Serialize(this, opts);
            File.WriteAllText(path, json);
        }

        public static Player Load(string path)
        {
            if (!File.Exists(path)) throw new FileNotFoundException(path);
            var json = File.ReadAllText(path);
            return JsonSerializer.Deserialize<Player>(json);
        }
    }
}