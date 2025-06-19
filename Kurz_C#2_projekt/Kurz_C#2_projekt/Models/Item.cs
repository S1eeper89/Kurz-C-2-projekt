namespace RPGGame.Models
{
    /// <summary>Základní abstraktní třída pro předměty ve hře.</summary>
    public abstract class Item
    {
        public string Name { get; protected set; }
        public string Description { get; protected set; }

        protected Item(string name, string description)
        {
            Name = name;
            Description = description;
        }

        /// <summary>Efekt předmětu, který se aplikuje při jeho použití hráčem.</summary>
        public abstract void ApplyEffect(Player player);
    }

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
            player.EquipWeapon(this);
        }
    }
}
