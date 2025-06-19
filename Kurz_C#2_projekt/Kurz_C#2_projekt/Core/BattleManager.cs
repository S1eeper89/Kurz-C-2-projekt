using RPGGame.Models;
using RPGGame.UI;

namespace RPGGame.Core
{
    public static class BattleManager
    {
        // Vrací bool, zda hráč přežil
        public static bool RunBattle(Player player, Monster monster)
        {
            while (player.IsAlive && monster.IsAlive)
            {
                Console.Clear();
                Console.WriteLine($"Souboj: {player.Name} vs {monster.Name}");
                Console.WriteLine($"HP hráče: {player.Health}   HP monstra: {monster.Health}");
                Console.WriteLine("A) Útok  B) Ústup (trest)  C) Inventář");
                var input = Console.ReadKey(true).Key;

                switch (input)
                {
                    case ConsoleKey.A:
                        monster.ReceiveDamage(player.Attack);
                        Console.WriteLine($"Útočíš na {monster.Name} ({monster.Health} HP zbývá)");
                        if (!monster.IsAlive) break;
                        player.ReceiveDamage(monster.Attack);
                        Console.WriteLine($"{monster.Name} tě zasáhl ({player.Health} HP zbývá)");
                        break;

                    case ConsoleKey.B:
                        // Ústup – hráč obdrží zásah a ukončí souboj
                        player.ReceiveDamage(monster.Attack * 2); // Trestný zásah (např. dvojnásobek útoku)
                        Console.WriteLine($"Utekl jsi a utrpěl trestný zásah! ({player.Health} HP zbývá)");
                        return false; // Hráč utekl

                    case ConsoleKey.C:
                        // Otevři inventář a po návratu pokračuj v kole
                        InventoryView.Show(player);
                        break;

                    default:
                        Console.WriteLine("Neplatná volba.");
                        break;
                }
                Console.WriteLine("Pokračuj klávesou...");
                Console.ReadKey(true);
            }
            return player.IsAlive;
        }
    }
}
