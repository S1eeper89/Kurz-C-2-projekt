using RPGGame.Core;
using RPGGame.Models;
using RPGGame.UI;

public class GameSimulation
{
    private Player _player;
    private MapManager _mapManager;
    private bool _isRunning;

    // Pro novou hru:
    public GameSimulation(Player player)
    {
        _player = player;
        _mapManager = new MapManager(_player);
    }

    // Volitelně: pro načtenou hru můžeš přidat tento konstruktor:
    public GameSimulation(Player player, GameState state)
    {
        _player = player;
        _mapManager = new MapManager(_player, state);
    }


    public void GameLoop()
    {
        _isRunning = true;
        while (_isRunning)
        {
            Console.Clear();
            _mapManager.RenderMap();
            Console.WriteLine($"HP: {_player.Health}/{_player.MaxHealth}  Útok: {_player.Attack}  Obrana: {_player.Defense}");
            Console.WriteLine("W/A/S/D = Pohyb | I = Inventář | T = Uložit | L = Načíst | M = Menu | Q = Konec");
            var key = Console.ReadKey(true).Key;

            switch (key)
            {
                case ConsoleKey.W:
                case ConsoleKey.A:
                case ConsoleKey.S:
                case ConsoleKey.D:
                    var dir = key switch
                    {
                        ConsoleKey.W => Direction.North,
                        ConsoleKey.S => Direction.South,
                        ConsoleKey.A => Direction.West,
                        ConsoleKey.D => Direction.East,
                        _ => throw new InvalidOperationException()
                    };
                    _mapManager.MovePlayer(dir);
                    break;

                case ConsoleKey.I:
                    InventoryView.Show(_player);
                    break;
                case ConsoleKey.T:
                    SaveLoadManager.SaveGame("save.json", _player, _mapManager);
                    Console.WriteLine("Hra byla uložena.");
                    Console.ReadKey();
                    break;
                case ConsoleKey.L:
                    var state = SaveLoadManager.LoadGame("save.json");
                    _player = state.Player;
                    _mapManager = new MapManager(_player, state);
                    Console.WriteLine("Hra byla načtena.");
                    Console.ReadKey();
                    break;

                case ConsoleKey.M:
                    _isRunning = false; // Ukončí smyčku, návrat do menu
                    break;

                case ConsoleKey.Q:
                    Environment.Exit(0);
                    break;
            }
        }
    }
}
