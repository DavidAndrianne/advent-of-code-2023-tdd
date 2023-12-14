using AdventOfCode2023.Tdd.Xunit.Util;
using AdventOfCode2023.Tdd.Xunit.Week2.Day10.Domain;
using AdventOfCode2023.Tdd.Xunit.Week2.Day9.InputParsers;
using FluentAssertions;
using System.Diagnostics;

namespace AdventOfCode2023.Tdd.Xunit.Week2.Day10;

public class Day10Tests
{
    public static string ExampleParseInput = @".....
.S-7.
.|.|.
.L-J.
.....
.....";
    [Fact]
    public void ParsePipeMap_WithValidInput_ReturnsCorrectValues()
    {
        // Arrange
        var charArr = ExampleParseInput.Split("\r\n")
            .Select(x => x.ToArray())
            .ToArray();
        // act
        var map = new MetalIslandMap(charArr);

        //Assert
        map.PipeGrid.Count().Should().Be(5);
        map.PipeGrid.First().Count().Should().Be(6);
        map.PipeGrid.Skip(1).First().Skip(1).First().Symbol.Symbol.Should().Be('S');
        map.PipeGrid.Skip(1).First().Skip(3).First().Symbol.Symbol.Should().Be('L');
        map.PipeGrid.Skip(3).First().Skip(1).First().Symbol.Symbol.Should().Be('7');
    }

    [Fact]
    public void ParsePipeMap_WithValidInput_IdentifiesLoop()
    {
        // Arrange
        var charArr = ExampleParseInput.Split("\r\n")
            .Select(x => x.ToArray())
            .ToArray();

        // act
        var map = new MetalIslandMap(charArr);

        //Assert
        map.Loop.Should().NotBeNull();
        map.Loop.Count().Should().Be(8);
        map.Loop.Skip(1).First().Symbol.Symbol.Should().Be('-');
        map.Loop.Last().Symbol.Symbol.Should().Be('|');
    }

    public static string ExampleAlternateInput = @"-L|F7
7S-7|
L|7||
-L-J|
L|-JF";
    [Fact]
    public void ParsePipeMap_WithAlternateInput_IdentifiesLoop()
    {
        // Arrange
        var charArr = ExampleAlternateInput.Split("\r\n")
            .Select(x => x.ToArray())
            .ToArray();

        // act
        var map = new MetalIslandMap(charArr);

        //Assert
        map.Loop.Should().NotBeNull();
        map.Loop.Count().Should().Be(8);
        map.Loop.Skip(1).First().Symbol.Symbol.Should().Be('-');
        map.Loop.Last().Symbol.Symbol.Should().Be('|');
    }

    [Fact]
    public void MetalIslandMap_HalfLoop_Part1()
    {
        // Arrange
        var map = new MetalIslandMap(InputReader.ReadCharGrid(10));

        // Act
        var total = map.Loop.Count()/2;

        // Assert
        total.Should().BeGreaterThan(2);
    }

    public static object[][] CountJunkData = [
        [
@"...........
.S-------7.
.|F-----7|.
.||.....||.
.||.....||.
.|L-7.F-J|.
.|..|.|..|.
.L--J.L--J.
...........",
            4,
            "2 pockets of 2 are enclosed by loop"
        ],
        [
@"..........
.S------7.
.|F----7|.
.||....||.
.||....||.
.|L-7F-J|.
.|..||..|.
.L--JL--J.
..........",
            4,
            "2 pockets of 2 are enclosed by loop"
        ],
        [
@"S--------7
L-----7..|
.F----|..|
F-----J..|
|........|
L-------7|
.F-7..F-J|
.|.|..|..|
FJ.L--J..|
L--------J",
            20,
            "4, 2 & 14 are enclosed by 3 subloops"
],
        [
@"..........
.S------7.
.|F----7|.
.||....||.
.||....||.
.|L-7F-J|.
.|..||..|.
.L--JL--J.
..........",
            4,
            "2 pockets of 2 are enclosed by loop"
        ],
        [
@".F----7F7F7F7F-7....
.|F--7||||||||FJ....
.||.FJ||||||||L7....
FJL7L7LJLJ||LJ.L-7..
L--J.L7...LJS7F-7L7.
....F-J..F7FJ|L7L7L7
....L7.F7||L7|.L7L7|
.....|FJLJ|FJ|F7|.LJ
....FJL-7.||.||||...
....L---J.LJ.LJLJ...",
            8,
            "4 pockets of 1, 5, 1, 1 are enclosed by loop"
        ],
[
@"FF7FSF7F7F7F7F7F---7
L|LJ||||||||||||F--J
FL-7LJLJ||||||LJL-77
F--JF--7||LJLJ7F7FJ-
L---JF-JLJ.||-FJLJJ7
|F|F-JF---7F7-L7L|7|
|FFJF7L7F-JF7|JL---7
7-L-JL7||F7|L7F-7F7|
L.L7LFJ|||||FJL7||LJ
L7JLJL-JLJLJL--JLJ.L",
            10,
    "4 pockets of 1, 4, 3, 2 are enclosed by loop"
        ],
    ];
    [Theory]
    [MemberData(nameof(CountJunkData))]
    public void MetalIslandMap_CountJunk_ReturnsExpected(string input, int expectedEnclosedJunkCount, string because)
    {
        // Arrange
        var charArr = input.Split("\r\n")
            .Select(x => x.ToArray())
            .ToArray();
        var map = new MetalIslandMap(charArr);

        // act
        var junkCount = map.CountJunkEnclosedInLoop();

        //Assert
        junkCount.Should().Be(expectedEnclosedJunkCount, because);
    }

    [Fact]
    public void MetalIslandMap_CountJunk__Part2()
    {
        // Arrange
        var map = new MetalIslandMap(InputReader.ReadCharGrid(10));

        // Act
        var total = map.CountJunkEnclosedInLoop();

        // Assert
        total.Should().BeGreaterThan(2);
    }
}
