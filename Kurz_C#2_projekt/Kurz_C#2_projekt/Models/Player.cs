// Player.cs
namespace RPGGame.Models
{
    /// <summary>
    /// Reprezentuje hráče s atributy a inventářem.
    /// </summary>
    public class Player : Entity
    {
        public List<Item> Inventory { get; private set; }

        /// <summary>
        /// Inicializuje hráče se zadaným maximálním zdravím, útokem a obranou.
        /// </summary>
        public Player(string name, int maxHealth, int attack, int defense)
            : base(name, maxHealth, attack, defense)
        {
            Inventory = new List<Item>();
        }

        /// <summary> Přidá předmět do inventáře. </summary>
        public void AddItemToInventory(Item item) => Inventory.Add(item);

        /// <summary> Použije předmět: aplikuje efekt a odstraní ho z inventáře. </summary>
        public void UseItem(Item item)
        {
            item.ApplyEffect(this);
            Inventory.Remove(item);
        }
    }
}
