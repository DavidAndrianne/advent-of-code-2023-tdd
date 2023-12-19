using System.Text;

namespace AdventOfCode2023.Tdd.Xunit.Week2.Day14.Domain;

public class RockLayout
{
    public const char Empty = '.';
    protected int minX = 0;
    protected int minY = 0;
    protected int maxX;
    protected int maxY;
    public List<Rock> Rocks { get; set; }
    public RockLayout(char[][] grid)
    {
        Rocks = new List<Rock>();
        maxX = grid.Length;
        maxY = grid.First().Length;
        for (var x = 0; x < maxX; x++)
            for(var y = 0; y < maxY; y++)
            {
                var character = grid[x][y];
                if (character != Empty) Rocks.Add(new Rock(character, x, y));
            }
    }

    public void Spin()
    {
        RollNorth();
        RollWest();
        RollSouth();
        RollEast();
    }

    public void RollNorth() 
        => RollDirection(
            predicate: r => r.X > minX,
            order: r => r.X,
            roll: r => r.RollNorth(),
            isAdjecentCheck: (r1, r2) => r1.IsNorthTo(r2));

    public void RollEast()
        => RollDirection(
            predicate: r => r.Y < maxY - 1,
            order: r => r.Y,
            roll: r => r.RollEast(),
            isAdjecentCheck: (r1, r2) => r1.IsEastTo(r2),
            isOrderingByDescending: true);

    public void RollSouth()
        => RollDirection(
            predicate: r => r.X < maxX - 1,
            order: r => r.X,
            roll: r => r.RollSouth(),
            isAdjecentCheck: (r1, r2) => r1.IsSouthTo(r2),
            isOrderingByDescending: true);

    public void RollWest()
        => RollDirection(
            predicate: r => r.Y > minY,
            order: r => r.Y,
            roll: r => r.RollWest(),
            isAdjecentCheck: (r1, r2) => r1.IsWestTo(r2));

    public void RollDirection(
        Func<Rock, bool> predicate, 
        Func<Rock, int> order, 
        Action<Rock> roll, 
        Func<Rock, Rock, bool> isAdjecentCheck, 
        bool isOrderingByDescending = false
        )
    {
        var roundRocksQuery = Rocks.Where(rock => rock.IsRound && predicate(rock));

        var roundRocks = isOrderingByDescending 
            ? roundRocksQuery.OrderByDescending(order).ToList()
            : roundRocksQuery.OrderBy(order).ToList();
        foreach (var rock in roundRocks)
        {
            var adjecentRock = Rocks.SingleOrDefault(other => isAdjecentCheck(other, rock));
            while (adjecentRock == null && predicate(rock))
            {
                roll(rock);
                if (Rocks.Any(r1 => r1 != rock && r1.X == rock.X && r1.Y == rock.Y)) throw new ApplicationException($"Collision detected at {rock.X},{rock.Y}");
                adjecentRock = Rocks.SingleOrDefault(other => isAdjecentCheck(other, rock));
            }
        }
    }

    public int CalculateTotalNorthBeamLoad() 
        => Rocks.Where(x => x.IsRound)
            .Select(x => maxX - x.X)
            .Sum();

    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.AppendLine();
        for(var x = 0; x < maxX; x++) {
            for (var y = 0; y < maxY; y++)
                sb.Append(Rocks.FirstOrDefault(rock => rock.X == x && rock.Y == y)?.ToString() ?? $"{Empty}");
            sb.AppendLine();
        }
        return sb.ToString();
    }
}
