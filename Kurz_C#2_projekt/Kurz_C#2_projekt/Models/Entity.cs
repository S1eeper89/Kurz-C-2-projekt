namespace RPGGame.Models
{
    /// <summary>
    /// Základ pro všechny herní entity (hráč, monstrum apod.).
    /// </summary>
    public abstract class Entity
    {
        public string Name { get; protected set; }
        public int Health { get; protected set; }
        public int MaxHealth { get; protected set; }
        public int Attack { get; protected set; }
        public int Defense { get; protected set; }

        protected Entity(string name, int maxHealth, int attack, int defense)
        {
            Name = name;
            MaxHealth = maxHealth;
            Health = maxHealth;
            Attack = attack;
            Defense = defense;
        }

        /// <summary>Aplikuje poškození, sníží životy (HP nesmí klesnout pod 0).</summary>
        public virtual void ReceiveDamage(int damage)
        {
            int damageTaken = damage - Defense;
            if (damageTaken < 0)
                damageTaken = 0;

            Health -= damageTaken;
            if (Health < 0)
                Health = 0;
        }

        public bool IsAlive => Health > 0;
    }
}
