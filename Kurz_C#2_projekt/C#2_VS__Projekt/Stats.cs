namespace C_2_VS__Projekt
{
    /// <summary>
    /// Základní statistiky pro bytosti ve hře.
    /// </summary>
    public class Stats
    {
        public int Health { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }

        // Bezparametrický konstruktor pro deserializaci
        public Stats() { }

        public Stats(int health, int attack, int defense)
        {
            Health = health;
            Attack = attack;
            Defense = defense;
        }

        public Stats Clone() => new Stats(Health, Attack, Defense);

        public void ApplyBonus(int attackBonus, int defenseBonus)
        {
            Attack += attackBonus;
            Defense += defenseBonus;
        }
    }
}