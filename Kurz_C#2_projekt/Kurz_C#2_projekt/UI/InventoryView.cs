using System;
using RPGGame.Models;

namespace RPGGame.UI
{
    /// <summary>
    /// Statická třída pro zobrazení a správu inventáře hráče v konzoli.
    /// Umožňuje použití předmětů nebo návrat zpět.
    /// </summary>
    public static class InventoryView
    {
        /// <summary>
        /// Vykreslí inventář hráče, umožní použít předmět nebo se vrátit zpět.
        /// </summary>
        /// <param name="player">Instance hráče, jehož inventář se má zobrazit.</param>
        public static void Show(Player player)
        {
            Console.Clear();
            Console.WriteLine("=== Inventář ===");
            if (player.Inventory.Count == 0)
            {
                Console.WriteLine("Inventář je prázdný.");
            }
            else
            {
                for (int i = 0; i < player.Inventory.Count; i++)
                {
                    var item = player.Inventory[i];
                    Console.WriteLine($"{i + 1}) {item.Name}: {item.Description}");
                }
                Console.WriteLine("0) Zpět");
                Console.Write("Vyber položku k použití: ");
                if (int.TryParse(Console.ReadLine(), out int choice) && choice > 0 && choice <= player.Inventory.Count)
                {
                    var selected = player.Inventory[choice - 1];
                    player.UseItem(selected);
                    Console.WriteLine($"Použil jsi {selected.Name}.");
                }
                else
                {
                    Console.WriteLine("Návrat zpět.");
                }
            }
            Console.WriteLine("Stiskni libovolnou klávesu pro pokračování...");
            Console.ReadKey();
        }
    }
}
