namespace AdventOfCode2023.Tdd.Xunit.Week2.Day10.Domain;

public class MetalIslandPipeSymbol
{
    public static MetalIslandPipeSymbol Pipe => new('|', isConnectingNorth: true, isConnectingSouth: true);
    public static MetalIslandPipeSymbol Minus => new('-', isConnectingEast: true, isConnectingWest: true);
    public static MetalIslandPipeSymbol L => new('L', isConnectingNorth: true, isConnectingEast: true);
    public static MetalIslandPipeSymbol J => new('J', isConnectingNorth: true, isConnectingWest: true);
    public static MetalIslandPipeSymbol Seven => new('7', isConnectingSouth: true, isConnectingWest: true);
    public static MetalIslandPipeSymbol F => new('F', isConnectingSouth: true, isConnectingEast: true);
    public static MetalIslandPipeSymbol Ground => new('.');
    public static MetalIslandPipeSymbol Start => new('S', true, true, true, true);

    public static MetalIslandPipeSymbol[] All => [Pipe, Minus, L, J, Seven, F, Ground, Start];
    public static MetalIslandPipeSymbol Parse(char symbol) => All.First(x => x.Symbol == symbol);

    public char Symbol { get; set; }
    public bool IsConnectingNorth { get; set; }
    public bool IsConnectingEast { get; set; }
    public bool IsConnectingSouth { get; set; }
    public bool IsConnectingWest { get; set; }

    public MetalIslandPipeSymbol(char symbol, bool isConnectingNorth = false, bool isConnectingEast = false, bool isConnectingSouth = false, bool isConnectingWest = false)
    {
        Symbol = symbol;
        IsConnectingNorth = isConnectingNorth;
        IsConnectingEast = isConnectingEast;
        IsConnectingSouth = isConnectingSouth;
        IsConnectingWest = isConnectingWest;
    }

    public override string ToString()
        => $"{Symbol}";
}