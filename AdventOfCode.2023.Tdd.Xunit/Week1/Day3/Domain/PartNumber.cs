using AdventOfCode2023.Tdd.Xunit.Week1.Day3.Carthesians;

namespace AdventOfCode2023.Tdd.Xunit.Week1.Day3.Domain;

public class PartNumber : IHasCarthesianRange
{
    public int Line { get; set; }
    public int Column { get; set; }
    public string Code { get; set; }
    public int CodeInt { get; set; }

    public int X => Column;
    public int Y => Line;
    public int Length => Code.Length;

    public PartNumber(int line, int column, string partNumberCode)
    {
        Line = line;
        Column = column;
        Code = partNumberCode;
        CodeInt = int.Parse(partNumberCode);
    }

    public int[] Dump() => [Line, Column, CodeInt];

    public bool IsNextToSymbol(IEnumerable<PartNumberSymbol> symbols) 
        => symbols.Any(symbol => symbol.IsAdjacentTo(this));

    public override string ToString()
        => $"{{{Line},{Column}}}: {Code}";
}
