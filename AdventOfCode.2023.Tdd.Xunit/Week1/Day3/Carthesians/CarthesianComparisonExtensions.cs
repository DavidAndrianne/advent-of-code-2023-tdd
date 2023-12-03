namespace AdventOfCode2023.Tdd.Xunit.Week1.Day3.Carthesians;

public static class CarthesianComparisonExtensions
{
    public static bool IsAdjacentTo(this IHasCarthesianCoordinates a, IHasCarthesianCoordinates b)
    {
        if(b is IHasCarthesianRange bRange) return a.IsAdjecentToRange(bRange);

        var isLeft = a.X == b.X - 1 && a.Y == b.Y;
        var isRight = a.X == b.X + 1 && a.Y == b.Y;
        var isAbove = a.X == b.X && a.Y == b.Y - 1;
        var isBelow = a.X == b.X && a.Y == b.Y + 1;

        bool[] conditions = [isLeft, isRight, isAbove, isBelow];
        return conditions.Any(x => x);
    }

    public static bool IsAdjecentToRange(this IHasCarthesianCoordinates a, IHasCarthesianRange b)
    {
        var isLeft = a.X == b.X - 1 && a.Y == b.Y;
        var isRight = a.X == b.EndX()+1 && a.Y == b.Y; 
        var isAbove = a.X.IsBetween(b) && a.Y == b.Y - 1;
        var isBelow = a.X.IsBetween(b) && a.Y == b.Y + 1;

        var isDiagonallyLeftAbove = a.X == b.X-1 && a.Y == b.Y - 1;
        var isDiagonallyRightAbove = a.X == b.EndX()+1 && a.Y == b.Y - 1;
        var isDiagonallyLeftBelow = a.X == b.X - 1 && a.Y == b.Y + 1;
        var isDiagonallyRightBelow = a.X == b.EndX() + 1 && a.Y == b.Y + 1;

        bool[] conditions = [
            isLeft, isRight, isAbove, isBelow, 
            isDiagonallyLeftAbove, isDiagonallyRightAbove, isDiagonallyLeftBelow, isDiagonallyRightBelow
            ];
        return conditions.Any(x => x);
    }

    public static int EndX(this IHasCarthesianRange c) => c.X + c.Length-1; // we assume the direction of length is horizontal

    public static bool IsBetween(this int x, IHasCarthesianRange b)
        => b.X <= x && x <= b.EndX();

    public static bool IsOverlapping(this IHasCarthesianRange a, IHasCarthesianRange b)
        => a.Y == b.Y // we assume the direction of length is horizontal
        || (a.X.IsBetween(b) || b.X.IsBetween(a));
}