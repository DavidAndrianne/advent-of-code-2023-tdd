using AdventOfCode2023.Tdd.Xunit.Week1.Day3.Carthesians;

namespace AdventOfCode2023.Tdd.Xunit.Week1.Day3.Domain;

public class PartNumberSymbol : IHasCarthesianCoordinates
{
    public int Line { get; set; }
    public int Column { get; set; }
    public string Symbol { get; set; }

    public int X => Column;
    public int Y => Line;

    public PartNumberSymbol(int line, int column, string symbol)
    {
        Line = line;
        Column = column;
        Symbol = symbol;
    }

    public object[] Dump() => [Line, Column, Symbol];

    public override string ToString()
    => $"{{{Line},{Column}}}: {Symbol}";

    public int SumGearScore(IEnumerable<PartNumber> partNumbers)
    {
        if (Symbol != "*") return 0;

        var adjecentNumbers = partNumbers.Where(x => this.IsAdjecentToRange(x))
            .ToArray();

        return adjecentNumbers.Count() == 2
            ? adjecentNumbers[0].CodeInt * adjecentNumbers[1].CodeInt
            : 0;
    }
}
