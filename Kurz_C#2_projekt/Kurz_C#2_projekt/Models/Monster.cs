namespace RPGGame.Models
{
    /// <summary>Reprezentuje nepřátele se základními atributy.</summary>
    public class Monster : Entity
    {
        public Monster(string name, int maxHealth, int attack, int defense)
            : base(name, maxHealth, attack, defense)
        {
        }

        public override void ReceiveDamage(int damage)
        {
            base.ReceiveDamage(damage);
            // Po smrti monstra lze přidat logiku (např. loot, odměnu)
        }
    }
}
