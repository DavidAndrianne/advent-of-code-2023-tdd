namespace AdventOfCode2023.Tdd.Xunit.Week1.Day3.Domain;

public class GondolaManual
{
    public IEnumerable<PartNumber> PartNumbers { get; set; }
    public IEnumerable<PartNumberSymbol> PartNumberSymbols { get; set; }
    public GondolaManual(IEnumerable<PartNumber> partNumbers, IEnumerable<PartNumberSymbol> symbols)
    {
        PartNumbers = partNumbers;
        PartNumberSymbols = symbols;
    }

    public int SumPartNumbersNextToSymbols() 
        => PartNumbers.Where(x => x.IsNextToSymbol(PartNumberSymbols))
            .Select(x => x.CodeInt)
            .Sum();

    public int SumGearsNextTo2Parts()
        => PartNumberSymbols.Sum(x => x.SumGearScore(PartNumbers));
}