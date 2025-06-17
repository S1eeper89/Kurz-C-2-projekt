namespace RPGGame.Models
{
    /// <summary>
    /// Reprezentuje nepřátele se základními atributy.
    /// </summary>
    public class Monster : Entity
    {
        /// <summary>
        /// Inicializuje monstrum se zadaným maximálním zdravím, útokem a obranou.
        /// </summary>
        public Monster(string name, int maxHealth, int attack, int defense)
            : base(name, maxHealth, attack, defense)
        {
        }

        public override void ReceiveDamage(int damage)
        {
            base.ReceiveDamage(damage);
            if (!IsAlive)
            {
                // Zde lze přidat logiku po smrti (loot, odměna apod.)
            }
        }
    }
}