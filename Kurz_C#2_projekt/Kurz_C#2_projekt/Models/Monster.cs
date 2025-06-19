namespace RPGGame.Models
{
    /// <summary>
    /// Reprezentuje nepřátele se základními atributy a logikou pro přijímání poškození.
    /// </summary>
    public class Monster : Entity
    {
        public Monster(string name, int maxHealth, int attack, int defense)
            : base(name, maxHealth, attack, defense)
        {
        }

        public override void ReceiveDamage(int damage)
        {
            base.ReceiveDamage(damage);
            // Zde lze do budoucna přidat např. loot nebo animace smrti
        }
    }
}
