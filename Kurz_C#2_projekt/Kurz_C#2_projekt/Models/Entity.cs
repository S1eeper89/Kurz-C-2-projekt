namespace RPGGame.Models
{
    /// <summary>
    /// Abstraktní základ pro všechny entity ve hře (hráč, monstrum, ...).
    /// Obsahuje atributy: jméno, zdraví, útok, obranu, logiku pro příjem poškození a bool pro určení trvání.
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

        /// <summary>
        /// Sníží zdraví o zadané poškození (damage). Hodnota nesmí klesnout pod nulu.
        /// </summary>
        public virtual void ReceiveDamage(int damage)
        {
            int damageTaken = damage - Defense;
            if (damageTaken < 0) damageTaken = 0;

            Health -= damageTaken;
            if (Health < 0) Health = 0;
        }

        /// <summary>
        /// Vrací true, pokud entita žije (HP > 0).
        /// </summary>
        public bool IsAlive => Health > 0;
    }
}
