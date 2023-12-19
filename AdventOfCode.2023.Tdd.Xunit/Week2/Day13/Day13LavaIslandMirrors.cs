using AdventOfCode2023.Tdd.Xunit.Util;
using AdventOfCode2023.Tdd.Xunit.Week2.Day13.Domain;
using AdventOfCode2023.Tdd.Xunit.Week2.Day13.InputParsers;
using FluentAssertions;

namespace AdventOfCode2023.Tdd.Xunit.Week2.Day13;

public class Day13LavaIslandMirrors
{
    public const string Example1 = @"#.##..##.
..#.##.#.
##......#
##......#
..#.##.#.
..##..##.
#.#.##.#.";

    public const string Example2 = 
@"#...##..#
#....#..#
..##..###
#####.##.
#####.##.
..##..###
#....#..#";

    [Fact]
    public void LavaIslandObservationParseExtensions_ParsesCorrectly()
    {
        // arrange
        var input = Example1.ToCharGrid()
            .Concat("".ToCharGrid())
            .Concat(Example2.ToCharGrid())
            .ToArray();

        // Act
        var observations = input.ToLavaIslandObservations();

        // Assert
        observations.Count().Should().Be(2);
        observations.First().Observation.Count().Should().Be(7);
        observations.Skip(1).First().Observation.Count().Should().Be(7);
    }

    [Fact]
    public void LavaIslandObservation_WithExampleInput_ReturnsExpectedLines()
    {
        // Arrange
        var observation = Example1.ToCharGrid()
            .ToLavaIslandObservations()
            .First();
        var expectedHorizontal = new string[]
        {
            "#.##..##.",
            "..#.##.#.",
            "##......#",
            "##......#",
            "..#.##.#.",
            "..##..##.",
            "#.#.##.#."
        };
        var expectedVertical = new string[]
        {
            "#.##..#",
            "..##...",
            "##..###",
            "#....#.",
            ".#..#.#",
            ".#..#.#",
            "#....#.",
            "##..###",
            "..##..."
        };

        // Act
        var horizontal = observation.GetHorizontalLines();
        var vertical = observation.GetVerticalLines();

        // Assert
        horizontal.Should().BeEquivalentTo(expectedHorizontal);
        vertical.Should().BeEquivalentTo(expectedVertical);
    }

    [Fact]
    public void LavaIslandObservation_DeduceMirrorLocation_ReturnsExpectedLocations()
    {
        // Arrange
        var observation1 = Example1.ToCharGrid()
            .ToLavaIslandObservations()
            .First();
        var observation2 = Example2.ToCharGrid()
            .ToLavaIslandObservations()
            .First();

        // Act
        var horizontalMirrorLines1 = observation1.DeduceHorizontalMirrorLocation();
        var verticalMirrorLines1 = observation1.DeduceVerticalMirrorLocation();

        var horizontalMirrorLines2 = observation2.DeduceHorizontalMirrorLocation();
        var verticalMirrorLines2 = observation2.DeduceVerticalMirrorLocation();

        // Assert
        horizontalMirrorLines1.Should().BeNull("there is no horizontal mirror");
        verticalMirrorLines1.Should().Be(4, "the vertical mirror is in between lines 5 & 6");

        horizontalMirrorLines2.Should().Be(3, "the horizontal mirror is between lines 4 & 5");
        verticalMirrorLines2.Should().BeNull("there is no vertical mirror");
    }

    [Fact]
    public void LavaIslandObservation_DeduceMirrorLocation_Example()
    {
        // Arrange
        var observations = Example1.ToCharGrid().Concat("".ToCharGrid()).Concat(Example2.ToCharGrid()).ToArray().ToLavaIslandObservations();

        // Act
        var horVert = observations.Select((x, i) => new { index = i, hor = x.HorizontalMirrorsCount(), vert = x.VerticalMirrorsCount() });
        var invalids = horVert.Where(x => x.hor == 0 && x.vert == 0).Select(x => x.index).ToList();
        var total = horVert.Sum(x => x.vert + x.hor * 100);

        // Assert
        total.Should().Be(405);
    }

    [Fact]
    public void LavaIslandObservation_DeduceMirrorLocation_Part1()
    {
        // Arrange
        var observations = InputReader.ReadCharGrid(13).ToLavaIslandObservations();

        // Act
        var horVert = observations.Select((x, i) => new { x = x, index = i, hor = x.HorizontalMirrorsCount(), vert = x.VerticalMirrorsCount() });
        var total = horVert.Sum(x => x.vert + x.hor*100);

        // Assert
        horVert.Count(x => x.hor == 0 && x.vert == 0 || (x.hor > 0 && x.vert > 0)).Should().Be(0);
        total.Should().BeGreaterThan(34340);
        total.Should().BeLessThan(36784);
    }

    public static object[][] CleanData = [
        ["#...#", 1, "##..#"],
        ["#...#", 0, "....#"],
        ["#...#", 4, "#...."],
        ];
    [Theory]
    [MemberData(nameof(CleanData))]
    public void LavaIslandObservation_CleanSmudge_CleansCorrectly(string line, int index, string expectedClean)
    {
        LavaIslandObservation.InvertCharAtIndex(line, index)
            .Should().Be(expectedClean);
    }

    [Fact]
    public void LavaIslandObservation_DeduceSmudgedMirrorLocation_Example()
    {
        // Arrange
        var observation = Example1.ToCharGrid().ToLavaIslandObservations().First();
        var observation2 = Example2.ToCharGrid().ToLavaIslandObservations().First();

        // Act
        var smudgeHorizontalIndex = observation.DeduceHorizontalMirrorLocation(true);
        var smudgeHorizontalIndex2 = observation2.DeduceHorizontalMirrorLocation(true);
        //var smudgeVerticalIndex = observation.DeduceVerticalMirrorLocation(true);

        // Assert
        smudgeHorizontalIndex.Should().Be(2, "mirror between rows 3 and 4");
        //smudgeVerticalIndex.Should().BeNull("no vertical mirror exists");
        smudgeHorizontalIndex2.Should().Be(0, "mirror between rows 1 and 2");
    }

    [Fact]
    public void LavaIslandObservation_DeduceSmudgedMirrorLocation_Source()
    {
        var observation = 
@"###..##.######.##
..#..#.#......#.#
#.##.#..#.##.#..#
#.#.##.#..##..#.#
#..#...###..###..
#..#...###..###..
#.#.##.#..##..#.#
#.##.#..#.##.#..#
..#..#.#......#.#
###..##.######.##
...#.#..######..#
..###....#..#..#.
#.###....#..#....
##.#####.#..#.###
...#.....####....
..#...#.#.##.#.#.
...###.#......#.#".ToCharGrid().ToLavaIslandObservations().First();

        var result = observation.DeduceHorizontalMirrorLocation(true)
            ?? observation.DeduceVerticalMirrorLocation(true);

        result.Should().NotBeNull();
        result.Should().Be(10);
    }

    [Fact]
    public void LavaIslandObservation_DeduceSmudgedMirrorLocation_Part2()
    {
        // Arrange
        var observations = InputReader.ReadCharGrid(13).ToLavaIslandObservations();

        // Act
        var horVert = observations.Select(x => new { hor = x.DeduceHorizontalMirrorLocation(true), vert = x.DeduceVerticalMirrorLocation(true) });
        var total = horVert.Sum(x => (x.hor.HasValue ? x.hor * 100 : 0) + (x.vert ?? 0));

        // Assert
        var firstInvalid = horVert.FirstOrDefault(x => x.hor == null && x.vert == null)?.ToString();
        var invalidCount = horVert.Count(x => x.hor == null && x.vert == null);
        var hasBoth = horVert.Where(x => x.hor.HasValue && x.vert.HasValue).ToArray();
        horVert.Any(x => x.hor == null && x.vert == null).Should().BeFalse("\n"+firstInvalid);
        total.Should().BeGreaterThan(30200);
        total.Should().BeLessThan(36784);
    }
}
