using System.Collections.Generic;

namespace RPGGame.Models
{
    /// <summary>
    /// Hráč – dědí od Entity, má inventář a možnost vybavit zbraň.
    /// </summary>
    public class Player : Entity
    {
        /// <summary>
        /// Inventář hráče.
        /// </summary>
        public List<Item> Inventory { get; private set; }

        /// <summary>
        /// Aktuálně vybavená zbraň (může být null).
        /// </summary>
        public Weapon EquippedWeapon { get; private set; }

        public Player(string name, int maxHealth, int attack, int defense)
            : base(name, maxHealth, attack, defense)
        {
            Inventory = new List<Item>();
        }

        /// <summary>
        /// Uzdraví hráče o zadanou hodnotu (maximálně do MaxHealth).
        /// </summary>
        public void Heal(int amount)
        {
            Health = Math.Min(Health + amount, MaxHealth);
        }

        /// <summary>
        /// Přidá předmět do inventáře.
        /// </summary>
        public void AddItemToInventory(Item item) => Inventory.Add(item);

        /// <summary>
        /// Použije vybraný předmět z inventáře.
        /// </summary>
        public void UseItem(Item item)
        {
            item.ApplyEffect(this);
            Inventory.Remove(item);
        }

        /// <summary>
        /// Vybaví hráče novou zbraní, přičte bonus k útoku.
        /// </summary>
        public void EquipWeapon(Weapon weapon)
        {
            if (EquippedWeapon != null)
                Attack -= EquippedWeapon.AttackBonus;

            EquippedWeapon = weapon;
            Attack += weapon.AttackBonus;
        }

        /// <summary>
        /// Odstraní aktuální zbraň a odebere její bonus.
        /// </summary>
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
