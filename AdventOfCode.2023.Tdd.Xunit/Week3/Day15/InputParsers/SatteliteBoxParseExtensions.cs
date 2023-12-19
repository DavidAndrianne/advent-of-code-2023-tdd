using AdventOfCode2023.Tdd.Xunit.Week3.Day15.Domain;

namespace AdventOfCode2023.Tdd.Xunit.Week3.Day15.InputParsers;

public static class SatteliteBoxParseExtensions
{
    public static SatteliteBox[] ToSatteliteBoxes(this string input, int boxCount = 256)
    {
        var boxes = new SatteliteBox[boxCount];
        for (var i = 0; i < boxes.Length; i++) boxes[i] = new SatteliteBox(i);

        input.Split(",")
            .ToList()
            .ForEach(step =>
            {
                ParseLensAndOperation(step, out var lens, out var operation);
                var relevantBoxNumber = lens.Label.ToInitializationAsciiHash();
                boxes[relevantBoxNumber].Update(operation, lens);
            });

        return boxes;
    }

    private static void ParseLensAndOperation(string input, out SatteliteLens lens, out SatteliteBoxOperation operation)
    {
        if (input.Contains(SatteliteBoxOperation.PutIntoBoxSign))
        {
            var parts = input.Split(SatteliteBoxOperation.PutIntoBoxSign);
            lens = new SatteliteLens(parts[0], int.Parse(parts[1].Trim()));
            operation = SatteliteBoxOperation.Put;
        }
        else if (input.Contains(SatteliteBoxOperation.RemoveFromBoxSign))
        {
            var parts = input.Split(SatteliteBoxOperation.RemoveFromBoxSign);
            lens = new SatteliteLens(parts[0].Trim());
            operation = SatteliteBoxOperation.Remove;
        }
        else throw new ApplicationException($"Unknown sign: {input}");
    }
}
