namespace RPGGame.Models
{
    public class Player : Entity
    {
        public List<Item> Inventory { get; private set; }
        public Weapon EquippedWeapon { get; private set; }

        public Player(string name, int maxHealth, int attack, int defense)
            : base(name, maxHealth, attack, defense)
        {
            Inventory = new List<Item>();
        }
        public void Heal(int amount)
        {
            Health = Math.Min(Health + amount, MaxHealth);
        }

        public void AddItemToInventory(Item item) => Inventory.Add(item);

        public void UseItem(Item item)
        {
            item.ApplyEffect(this);
            Inventory.Remove(item);
        }

        // Přidáno – vybavení zbraně
        public void EquipWeapon(Weapon weapon)
        {
            if (EquippedWeapon != null)
                Attack -= EquippedWeapon.AttackBonus;  // Odebrat bonus předchozí zbraně

            EquippedWeapon = weapon;
            Attack += weapon.AttackBonus;             // Přidat bonus nové zbraně
        }

        // Přidáno – odebrání vybavené zbraně
        public void UnequipWeapon()
        {
            if (EquippedWeapon != null)
            {
                Attack -= EquippedWeapon.AttackBonus;
                EquippedWeapon = null;
            }
        }
    }
}
