namespace AdventOfCode2023.Tdd.Xunit.Week2.Day10.Domain;

public class MetalIslandPipe
{
    public MetalIslandPipeSymbol Symbol { get; set; }
    public MetalIslandPipe? NextPipe { get; set; }
    public MetalIslandPipe? PreviousPipe { get; set; }

    public int X { get; protected set; }
    public int Y { get; protected set; }
    public MetalIslandPipe(char symbol, int x, int y)
    {
        Symbol = MetalIslandPipeSymbol.Parse(symbol);
        X = x;
        Y = y;
    }

    public MetalIslandPipe Link(MetalIslandPipe other)
    {
        NextPipe = other;
        other.PreviousPipe = this;
        return other;
    }
    public MetalIslandPipe?[] GetNeighbours(MetalIslandPipe[][] grid, bool isConnected = true)
        => [GetNorth(grid, isConnected), GetEast(grid, isConnected), GetSouth(grid, isConnected), GetWest(grid, isConnected)];

    public MetalIslandPipe? GetNorth(MetalIslandPipe[][] grid, bool isConnected = true) 
        => (!isConnected || Symbol.IsConnectingNorth) && Y > 0 
            ? ReturnIf(grid[X][Y - 1], nextSymbol => !isConnected || nextSymbol.IsConnectingSouth) 
            : null;
    public MetalIslandPipe? GetSouth(MetalIslandPipe[][] grid, bool isConnected = true) 
        => (!isConnected || Symbol.IsConnectingSouth) && grid[0].Length > Y + 1 
            ? ReturnIf(grid[X][Y + 1], nextSymbol => !isConnected || nextSymbol.IsConnectingNorth) 
            : null; 
    public MetalIslandPipe? GetEast(MetalIslandPipe[][] grid, bool isConnected = true) 
        => (!isConnected || Symbol.IsConnectingEast) && grid.Length > X + 1 
            ? ReturnIf(grid[X + 1][Y], nextSymbol => !isConnected || nextSymbol.IsConnectingWest)
            : null;
    public MetalIslandPipe? GetWest(MetalIslandPipe[][] grid, bool isConnected = true) 
        => (!isConnected || Symbol.IsConnectingWest) && X > 0 
            ? ReturnIf(grid[X - 1][Y], nextSymbol => !isConnected || nextSymbol.IsConnectingEast)
            : null;

    private MetalIslandPipe? ReturnIf(MetalIslandPipe pipe, Func<MetalIslandPipeSymbol, bool> condition) 
        => condition(pipe.Symbol) ? pipe : null;

    public override string ToString()
     => $"{Symbol}";
}