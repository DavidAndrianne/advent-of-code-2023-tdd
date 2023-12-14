using System.Text;

namespace AdventOfCode2023.Tdd.Xunit.Week2.Day10.Domain;

public class MetalIslandMap
{
    public MetalIslandPipe[][] PipeGrid { get; set; }
    public List<MetalIslandPipe> Loop { get; set; }
    public MetalIslandPipe Start { get; protected set; }

    public MetalIslandMap(char[][] map)
    {
        InitGrid(map.Count(), map.First().Count());
        SetGrid(map);
        IdentifyLoop();
    }

    private void InitGrid(int width, int length)
    {
        PipeGrid = new MetalIslandPipe[length][];
        for (var i = 0; i < length; i++)
        {
            PipeGrid[i] = new MetalIslandPipe[width];
        }
    }

    private void SetGrid(char[][] map)
    {
        for (var i = 0; i < map.Length; i++)
        {
            for (var j = 0; j < map[i].Length; j++)
            {
                PipeGrid[j][i] = new MetalIslandPipe(map[i][j], j, i);
                if (PipeGrid[j][i].Symbol.Symbol == MetalIslandPipeSymbol.Start.Symbol) Start = PipeGrid[j][i];
            }
        }
        if (Start == null) throw new ArgumentException($"No '{MetalIslandPipeSymbol.Start.Symbol}' identified in {this}");
    }

    private void IdentifyLoop()
    {
        var pipe = Start;
        Loop = new List<MetalIslandPipe> { Start };
        while (pipe != null)
        {
            pipe = MaybeLink(pipe);
        }
        if (!Loop.Last().GetNeighbours(PipeGrid).Contains(Start)) throw new Exception($"Loop did not loop fully {this}");
        IdentifySubLoops();
    }
    private List<MetalIslandPipe[]> SubLoops { get; set; } = new List<MetalIslandPipe[]>();
    private char[] ExpectedSubloopSymbols = [
        MetalIslandPipeSymbol.Seven.Symbol, 
        MetalIslandPipeSymbol.F.Symbol, 
        MetalIslandPipeSymbol.J.Symbol,
        MetalIslandPipeSymbol.L.Symbol];
    private void IdentifySubLoops()
    {
        var discovered = new List<MetalIslandPipe>();
        foreach (var node in Loop)
        {
            discovered.Add(node);
            var closingSubloopNodes = node.GetNeighbours(PipeGrid, isConnected: false)
                .Where(x => x != node.NextPipe && x != node.PreviousPipe && discovered.Contains(x!) && SubLoops.All(sub => !sub.Contains(x)));
            if (!closingSubloopNodes.Any()) continue;

            var loopStartIndex = discovered.IndexOf(closingSubloopNodes.First()!);
            var subLoop = discovered.Skip(loopStartIndex).ToArray();
            if (IsInvalidSubloop(subLoop)) continue;

            var debug = subLoop.Stringify();
            SubLoops.Add(subLoop);
        }
    }

    private bool IsInvalidSubloop(MetalIslandPipe[] subLoop)
    {
        var distinctSymbols = subLoop.Select(x => x.Symbol.Symbol).ToArray();
        return subLoop.Count() < 8
            || distinctSymbols.Count(s => ExpectedSubloopSymbols.Contains(s)) < 3
            || (subLoop.Max(x => x.X) - subLoop.Min(x => x.X)) <= 2
            || (subLoop.Max(x => x.Y) - subLoop.Min(x => x.Y)) <= 2
            || !subLoop.Stringify().RemoveOuterCharacters().Contains('.');
    }

    private MetalIslandPipe? MaybeLink(MetalIslandPipe currentPipe)
    {
        var nextPipe = currentPipe.GetNeighbours(PipeGrid)
            .FirstOrDefault(x => x != null && ((Loop.Count() > 2 && x.Symbol.Symbol == MetalIslandPipeSymbol.Start.Symbol) || !Loop.Contains(x)));

        if (nextPipe == null) throw new ArgumentException($"Pipe '{currentPipe.Symbol}' ({currentPipe.X},{currentPipe.Y}) has no connected pipe to it in: {this}");

        currentPipe.Link(nextPipe);

        if (nextPipe == Start) return null; // if returned to start, return null to break the loop

        Loop.Add(nextPipe);
        return nextPipe;
    }

    public int CountJunkEnclosedInLoop()
    {
        var minX = Loop.Select(x => x.X).Min()+1;
        var maxX = Loop.Select(x => x.X).Max()-1;
        var minY = Loop.Select(x => x.Y).Min()+1;
        var maxY = Loop.Select(x => x.Y).Max()-1;

        var junk = PipeGrid.SelectMany(x => x)
            .Where(pipe => minX <= pipe.X && pipe.X <= maxX
                && minY <= pipe.Y && pipe.Y <= maxY)
            .Where(pipe => !Loop.Contains(pipe))
            .ToArray();
        var enclosedJunk = junk.Where(x => SubLoops.Any(loop => IsEnclosedByLoop(loop, x))).ToArray();

        return enclosedJunk.Length;
    }

    private bool IsEnclosedByLoop(MetalIslandPipe[] loop, MetalIslandPipe pipe)
    {
        var countNorthInLoop = loop.Count(x => x.X == pipe.X && pipe.Y > x.Y);
        var countSouthInLoop = loop.Count(x => x.X == pipe.X && pipe.Y < x.Y);
        var countEastInLoop = loop.Count(x => x.Y == pipe.Y && pipe.X < x.X);
        var countWestInLoop = loop.Count(x => x.Y == pipe.Y && pipe.X > x.X);

        var isEnclosed = countNorthInLoop > 0 && countSouthInLoop > 0 && countEastInLoop > 0 && countWestInLoop > 0;
        var isDoubledIn = countNorthInLoop % 2 == 0
            && countEastInLoop % 2 == 0
            && countSouthInLoop % 2 == 0
            && countWestInLoop % 2 == 0;

        if (isEnclosed && !isDoubledIn)
        {
            var debug = loop.Stringify(pipe.X, pipe.Y);
        }

        return isEnclosed && !isDoubledIn;
    }

    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.AppendLine();
        for(var x = 0; x < PipeGrid.First().Length; x++)
        { 
            for(var y = 0; y < PipeGrid.Length; y++)
            {
                sb.Append($"{PipeGrid[x][y]}");
            }
            sb.AppendLine();
        }
        return sb.ToString();
    }
}