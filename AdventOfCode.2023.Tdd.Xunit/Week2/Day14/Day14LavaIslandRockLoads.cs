using AdventOfCode2023.Tdd.Xunit.Util;
using AdventOfCode2023.Tdd.Xunit.Week1.Day5.Domain;
using AdventOfCode2023.Tdd.Xunit.Week2.Day13.InputParsers;
using AdventOfCode2023.Tdd.Xunit.Week2.Day14.Domain;
using AdventOfCode2023.Tdd.Xunit.Week2.Day14.InputParsers;
using FluentAssertions;

namespace AdventOfCode2023.Tdd.Xunit.Week2.Day14;

public class Day14LavaIslandRockLoads
{
    public const string Example = 
@"
O....#....
O.OO#....#
.....##...
OO.#O....O
.O.....O#.
O.#..O.#.#
..O..#O..O
.......O..
#....###..
#OO..#....";

    [Fact]
    public void LavaIslandRockLayoutParseExtensions_ParsesCorrectly()
    {
        // arrange
        var input = Example.ToCharGrid()
            .Concat("".ToCharGrid())
            .Concat(Example.ToCharGrid())
            .ToArray();

        // Act
        var rockLayouts = input.ParseRockLayouts();

        // Assert
        rockLayouts.Count().Should().Be(2);
        rockLayouts.First().Rocks.Should().NotBeNull();
        rockLayouts.First().Rocks.Where(x => x.IsRound).Count().Should().Be(18);
        rockLayouts.First().Rocks.Where(x => !x.IsRound).Count().Should().Be(17);
        rockLayouts.First().ToString().Trim().Should().Be(Example.Trim());
    }

    public static string RollNorthExample = 
@"OOOO.#.O..
OO..#....#
OO..O##..O
O..#.OO...
........#.
..#....#.#
..O..#.O.O
..O.......
#....###..
#....#....";

    [Fact]
    public void RockLayout_RollNorth_RollsRoundRocks()
    {
        // Arrange
        var layout = Example.ToCharGrid().ParseRockLayouts().First();

        // Act
        layout.RollNorth();
        var updated = layout.ToString();

        // Assert
        updated.Trim().Should().Be(RollNorthExample.Trim());
    }

    [Fact]
    public void RockLayout_CalculateTotalNorthBeamLoad_Example()
    {
        // Arrange
        var layout = Example.ToCharGrid().ParseRockLayouts().First();

        // Act
        layout.RollNorth();
        var total = layout.CalculateTotalNorthBeamLoad();

        // Assert
        total.Should().Be(136);
    }

    [Fact]
    public void RockLayout_CalculateTotalNorthBeamLoad_Part1()
    {
        // Arrange
        var layout = InputReader.ReadCharGrid(14).ParseRockLayouts().First();

        // Act
        layout.RollNorth();
        var total = layout.CalculateTotalNorthBeamLoad();

        // Assert
        total.Should().BeGreaterThan(136);
    }

    public static Rock DummyRock = new Rock('O', 1, 1);
    public static Func<Rock, bool>[][] IsAdjacentToExtensionTests = [
        [DummyRock.IsNorthTo],
        [DummyRock.IsWestTo],
        [DummyRock.IsSouthTo],
        [DummyRock.IsEastTo],
    ];
    [Theory]
#pragma warning disable xUnit1019 // MemberData must reference a member providing a valid data type
    [MemberData(nameof(IsAdjacentToExtensionTests))]
#pragma warning restore xUnit1019 // MemberData must reference a member providing a valid data type
    public void RockLayoutExtensions_IsAdjacentTo_ReturnsTrue(Func<Rock, bool> condition)
    {
        // Arrange
        Rock[] rocks = [
            new Rock('#', 2, DummyRock.Y), // 1,1 is north to 2,1
            new Rock('#', DummyRock.X, 2), // 1,1 is west to 1,2
            new Rock('#', 0, DummyRock.Y), // 1,1 is south to 0,1
            new Rock('#', DummyRock.X, 0), // 1,1 is east to 1,0
        ];
        var index = IsAdjacentToExtensionTests.Select((x, i) => x.First() == condition ? (int?)i : null).First(x => x.HasValue).Value;

        // Act
        var result = condition(rocks[index]);

        // Assert
        result.Should().BeTrue("♥");
    }

    public static string RollWestExample =
@"
OOOO.#O...
OO..#....#
OOO..##O..
O..#OO....
........#.
..#....#.#
O....#OO..
O.........
#....###..
#....#....";

[Fact]
public void RockLayout_RollWest_RollsRoundRocks()
{
    // Arrange
    var layout = RollNorthExample.ToCharGrid().ParseRockLayouts().First();

    // Act
    layout.RollWest();
    var updated = layout.ToString();

    // Assert
    updated.Trim().Should().Be(RollWestExample.Trim());
}

    public static string RollSouthExample =
@"
.....#....
....#.O..#
O..O.##...
O.O#......
O.O....O#.
O.#..O.#.#
O....#....
OO....OO..
#O...###..
#O..O#....";

    [Fact]
    public void RockLayout_RollSouth_RollsRoundRocks()
    {
        // Arrange
        var layout = RollWestExample.ToCharGrid().ParseRockLayouts().First();

        // Act
        layout.RollSouth();
        var updated = layout.ToString();

        // Assert
        updated.Trim().Should().Be(RollSouthExample.Trim());
    }

    public static string RollEastExample =
@"
.....#....
....#...O#
...OO##...
.OO#......
.....OOO#.
.O#...O#.#
....O#....
......OOOO
#...O###..
#..OO#....";

    [Fact]
    public void RockLayout_RollEast_RollsRoundRocks()
    {
        // Arrange
        var layout = RollSouthExample.ToCharGrid().ParseRockLayouts().First();

        // Act
        layout.RollEast();
        var updated = layout.ToString();

        // Assert
        updated.Trim().Should().Be(RollEastExample.Trim());
    }

    public static object[][] SpinData = [
        [
@".....#....
....#...O#
...OO##...
.OO#......
.....OOO#.
.O#...O#.#
....O#....
......OOOO
#...O###..
#..OO#....",
            1,
            "it spun north, west, south & east 1 time"
        ],
        [
@".....#....
....#...O#
.....##...
..O#......
.....OOO#.
.O#...O#.#
....O#...O
.......OOO
#..OO###..
#.OOO#...O",
            2,
            "it spun north, west, south & east 2 times"
        ],
        [
@".....#....
....#...O#
.....##...
..O#......
.....OOO#.
.O#...O#.#
....O#...O
.......OOO
#...O###.O
#.OOO#...O",
            3,
            "it spun north, west, south & east 3 times"
        ]
    ];
    [Theory]
    [MemberData(nameof(SpinData))]
    public void RockLayout_Spin_Cycles(string expected, int spinCount, string because)
    {
        // Arrange
        var layout = Example.ToCharGrid().ParseRockLayouts().First();

        // Act
        for (var i = 0; i < spinCount; i++) layout.Spin();
        var result = layout.ToString().Trim();

        // Assert
        result.Should().Be(expected.Trim(), because);
    }

    [Fact]
    public void RockLayout_Spin_Part2()
    {
        // Arrange
        var cycles = 1000000000L;
        var layout = InputReader.ReadCharGrid(14).ParseRockLayouts().First();

        // Act
        var snapshots = new List<string> { layout.ToString() };
        for (var i = 0L; i < 60; i++)
        {
            layout.Spin();
            var newSnapshot = layout.ToString();
            if (snapshots.Contains(newSnapshot)) break;
            snapshots.Add(newSnapshot);
        }

        //var expectedSnapshotIndex = (int)(cycles % snapshots.Count());
        //var expectedSnapshot = snapshots[expectedSnapshotIndex];
        //layout = expectedSnapshot.ToCharGrid().ParseRockLayouts().First();

        var totals = snapshots.Skip(50).Select(x => x.ToCharGrid().ParseRockLayouts().First().CalculateTotalNorthBeamLoad()).ToArray();


        var total = layout.CalculateTotalNorthBeamLoad();

        // Assert
        total.Should().BeGreaterThan(96195);
    }
}
