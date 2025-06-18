using System;

namespace RPGGame.Models
{
    /// <summary>
    /// Základní abstraktní třída pro všechny typy předmětů ve hře.
    /// </summary>
    public abstract class Item
    {
        public string Name { get; protected set; }
        public string Description { get; protected set; }

        protected Item(string name, string description)
        {
            Name = name;
            Description = description;
        }

        /// <summary>
        /// Efekt předmětu, který se aplikuje při jeho použití hráčem.
        /// </summary>
        public abstract void ApplyEffect(Player player);
    }

    /// <summary>
    /// Lektvar – použití obnoví zdraví hráče.
    /// </summary>
    public class Potion : Item
    {
        public int HealAmount { get; private set; }

        public Potion(string name, string description, int healAmount)
            : base(name, description)
        {
            HealAmount = healAmount;
        }

        public override void ApplyEffect(Player player)
        {
            player.Heal(HealAmount);
        }

    }

    /// <summary>
    /// Zbraň – při použití zvýší útok hráče (vybaví zbraň).
    /// </summary>
    public class Weapon : Item
    {
        public int AttackBonus { get; private set; }

        public Weapon(string name, string description, int attackBonus)
            : base(name, description)
        {
            AttackBonus = attackBonus;
        }

        public override void ApplyEffect(Player player)
        {
            player.EquipWeapon(this); // Předá zbraň hráči, aby ji vybavil správně
        }
    }
}
