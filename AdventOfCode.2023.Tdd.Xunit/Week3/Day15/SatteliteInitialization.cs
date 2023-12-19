using AdventOfCode2023.Tdd.Xunit.Util;
using AdventOfCode2023.Tdd.Xunit.Week3.Day15.Domain;
using AdventOfCode2023.Tdd.Xunit.Week3.Day15.InputParsers;
using FluentAssertions;

namespace AdventOfCode2023.Tdd.Xunit.Week3.Day15;

public class SatteliteInitializationTests
{
    public static object[][] HashTestData = [
        ["H", 200],
        ["HA", 153],
        ["HAS", 172],
        ["HASH", 52]
    ];
    [Theory]
    [MemberData(nameof(HashTestData))]
    public void HashExtensions_HashCorrectly(string input, int expected) 
        => input.ToInitializationAsciiHash()
            .Should().Be(expected);

    public static string Example = "rn=1,cm-,qp=3,cm=2,qp-,pc=4,ot=9,ab=5,pc-,pc=6,ot=7";
    [Fact]
    public void HashExtension_Example() 
        => Example
            .Split(",")
            .Select(x => x.ToInitializationAsciiHash())
            .Sum()
            .Should().Be(1320);

    [Fact]
    public void HashExtension_Part1()
    {
        // Arrange
        var input = InputReader.ReadLinesForDay(15)
            .First()
            .Split(",");

        // Act
        var total = input.Select(x => x.ToInitializationAsciiHash())
            .Sum();

        // Assert
        total.Should().BeGreaterThan(0);
    }

    public static object[][] ParseBoxesData = [
        [
            "rn=1",
            "Box 0: [rn 1]"
        ],
        [
            "rn=1,cm-1",
            "Box 0: [rn 1]"
        ],
        [
            "rn=1,cm-1,qp=3",
            @"Box 0: [rn 1]
Box 1: [qp 3]"
        ],
        [
            "rn=1,cm-1,qp=3,cm=2",
            @"Box 0: [rn 1] [cm 2]
Box 1: [qp 3]"
        ],
        [
            "rn=1,cm-1,qp=3,cm=2,cm=5",
            @"Box 0: [rn 1] [cm 5]
Box 1: [qp 3]"
        ],
    ];
    [Theory]
    [MemberData(nameof(ParseBoxesData))]
    public void ParseBoxes_AppliesOperationsCorrectly(string input, string expected)
    {
        string.Join(Environment.NewLine, 
            input.ToSatteliteBoxes()
            .Where(x => x.Lenses.Any())
            .Select(x => x.ToString())
            .ToArray()
            )
            .Should().Be(expected);
    }

    [Fact]
    public void Boxes_CalculateFocusingPower_Example()
    {
        var boxes = Example.ToSatteliteBoxes();
        boxes.Where(x => x.Lenses.Any())
            .Sum(x => x.CalculateFocusingPower())
            .Should().Be(145);
    }

    [Fact]
    public void Boxes_CalculateFocusingPower_Part2()
    {
        var boxes = InputReader.ReadLinesForDay(15).First().ToSatteliteBoxes();
        
        var total = boxes.Where(x => x.Lenses.Any())
            .Sum(x => x.CalculateFocusingPower());

        total.Should().BeGreaterThan(145);
    }
}
