using RPGGame.Models;
using RPGGame.UI;

namespace RPGGame.Core
{
    /// <summary>
    /// Statická třída zajišťující logiku soubojů mezi hráčem a monstrem.
    /// Umožňuje útok, ústup a použití inventáře během boje.
    /// </summary>
    public static class BattleManager
    {
        /// <summary>
        /// Spustí souboj mezi hráčem a konkrétním monstrem.
        /// Vrací true, pokud hráč přežije, jinak false.
        /// </summary>
        /// <param name="player">Instance hráče, který bojuje.</param>
        /// <param name="monster">Instance monstra, se kterým se bojuje.</param>
        /// <returns>True pokud hráč přežije, false pokud zemře nebo zemře při útěku.</returns>
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
                        // Hráč útočí na monstrum, pokud přežije, útočí i monstrum na hráče
                        monster.ReceiveDamage(player.Attack);
                        Console.WriteLine($"Útočíš na {monster.Name} ({monster.Health} HP zbývá)");
                        if (!monster.IsAlive) break;
                        player.ReceiveDamage(monster.Attack);
                        Console.WriteLine($"{monster.Name} tě zasáhl ({player.Health} HP zbývá)");
                        break;

                    case ConsoleKey.B:
                        // Hráč se pokusí o útěk, obdrží trestný zásah, pokud přežije, souboj končí
                        player.ReceiveDamage(monster.Attack); // Trestný zásah (může být např. dvojnásobek útoku)
                        Console.WriteLine($"Utekl jsi a utrpěl trestný zásah! ({player.Health} HP zbývá)");
                        Console.ReadKey(true);
                        if (player.Health <= 0)
                            return false;
                        else
                            return true; // Hráč utekl, ale přežil

                    case ConsoleKey.C:
                        // Otevře inventář hráče, po návratu pokračuje v souboji
                        InventoryView.Show(player);
                        break;

                    default:
                        Console.WriteLine("Neplatná volba.");
                        break;
                }
                Console.WriteLine("Pokračuj klávesou...");
                Console.ReadKey(true);
            }
            // Vrací true, pokud hráč souboj přežije
            return player.IsAlive;
        }
    }
}