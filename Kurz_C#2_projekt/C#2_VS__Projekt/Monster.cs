namespace C_2_VS__Projekt
{
    public class Monster : Entity
    {
        public Monster(int health, int attack, int defense)
            : base(health, attack, defense) { }

        protected override void OnDeath()
        {
            Console.WriteLine("Monster defeated!");
        }

        public int RewardGold() => new Random().Next(5, 15);
        public int RewardXP() => Level * 10;
    }
}