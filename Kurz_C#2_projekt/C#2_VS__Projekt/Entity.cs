using System;

namespace C_2_VS__Projekt
{
    /// <summary>
    /// Abstraktní základ pro všechny "bytosti" ve hře.
    /// </summary>
    public abstract class Entity
    {
        public Stats BaseStats { get; protected set; }
        public int Level { get; set; } = 1;
        public bool IsAlive => BaseStats.Health > 0;

        protected Entity(int health, int attack, int defense)
        {
            BaseStats = new Stats(health, attack, defense);
        }

        public virtual void ReceiveDamage(int damage)
        {
            var newHealth = Math.Max(0, BaseStats.Health - damage);
            BaseStats = new Stats(newHealth, BaseStats.Attack, BaseStats.Defense);
            if (!IsAlive) OnDeath();
        }

        public virtual int CalculateDamage(Entity target)
            => Math.Max(0, BaseStats.Attack - target.BaseStats.Defense);

        protected virtual void OnDeath() { }
    }
}