using AdventOfCode2023.Tdd.Xunit.Util;
using AdventOfCode2023.Tdd.Xunit.Week2.Day12.Domain;
using AdventOfCode2023.Tdd.Xunit.Week2.Day12.InputParsers;
using FluentAssertions;

namespace AdventOfCode2023.Tdd.Xunit.Week2.Day12;

public class Day12Tests
{
    public const string ExampleRecordInput = "?###???????? 3,2,1";
    [Fact]
    public void SpringRecordParseExtensions_ToSpringRecord_ParsesCorrectly()
    {
        // Arrange
        int[] expectedBrokenSpringGroupSizes = [3,2,1];

        // Act
        var record = ExampleRecordInput.ToSpringRecord();

        // Assert
        record.SpringLedger.Should().Be("?###????????");
        record.DamagedSpringGroupSizes.Should().BeEquivalentTo(expectedBrokenSpringGroupSizes);
    }

    [Fact]
    public void SpringRecord_GenerateRegex_ReturnsExpected()
    {
        // Arrange
        var expectedPattern = @"^\.*###\.+##\.+#\.*$";

        // Act
        var pattern = SpringRecord.GeneratePattern([3,2,1]);

        // Assert
        pattern.Should().Be(expectedPattern);
    }

    [Fact]
    public void SpringRecord_GeneratePossibleValues_ReturnsExpectedValues()
    {
        // Arrange
        var record = "?###???????? 3,2,1".ToSpringRecord();
        string[] expectedPossibilities = [
            ".###.##..#..",
            ".###.##...#.",
            ".###.##.#...",
            ".###.##....#",
            ".###..##.#..",
            ".###..##..#.",
            ".###..##...#",
            ".###...##.#.",
            ".###...##..#",
            ".###....##.#"
        ];

        // Act
        var possibilities = record.GeneratePossibleValues();

        // Assert
        possibilities.Count().Should().Be(expectedPossibilities.Count());
        possibilities.Should().BeEquivalentTo(expectedPossibilities); 
    }



    public static object[][] GeneratePossibities = [
        ["???.### 1,1,3", 1],
        [".??..??...?##. 1,1,3", 4],
        ["?#?#?#?#?#?#?#? 1,3,1,6", 1],
        ["????.#...#... 4,1,1", 1],
        ["????.######..#####. 1,6,5", 4],
        ["?###???????? 3,2,1", 10],
    ];

    [Theory]
    [MemberData(nameof(GeneratePossibities))]
    public void SpringRecord_GeneratePossibleValues_ReturnsExpectedCounts(string recordLine, int expectedPossibilities)
    {
        // Arrange
        var record = recordLine.ToSpringRecord();

        // Act
        var possibilities = record.GeneratePossibleValues();

        // Assert
        possibilities.Count().Should().Be(expectedPossibilities);
    }

    [Fact]
    public void SpringRecord_SumPossibleValues_Part1()
    {
        // Arrange
        var records = InputReader.ReadLinesForDay(12)
            .Select(x => x.ToSpringRecord());

        // Act
        var total = records.Select(x => x.GeneratePossibleValues().Length)
            .Sum();

        // Assert
        total.Should().BeGreaterThan(1);
    }

    [Fact]
    public void SpringRecordParseExtensions_UnfoldToSpringRecord_ParsesCorrectly()
    {
        // Arrange
        int[] expectedBrokenSpringGroupSizes = [3, 2, 1];

        // Act
        var recordLine = "???.### 1,1,3".UnfoldRecordLine();

        // Assert
        recordLine.Should().Be("???.###????.###????.###????.###????.### 1,1,3,1,1,3,1,1,3,1,1,3,1,1,3");
    }

    public static object[][] GenerateUnfoldedPossibities = [
        ["???.### 1,1,3", 1],
        [".??..??...?##. 1,1,3", 16384],
        ["?#?#?#?#?#?#?#? 1,3,1,6", 1],
        ["????.#...#... 4,1,1", 16],
        ["????.######..#####. 1,6,5", 2500],
        ["?###???????? 3,2,1", 506250],
    ];
    [Theory]
    [MemberData(nameof(GenerateUnfoldedPossibities))]
    public void SpringRecord_GenerateUnfoldedPossibleValues_ReturnsExpectedCounts(string recordLine, int expectedPossibilities)
    {
        // Arrange
        var record = recordLine.UnfoldRecordLine()
            .ToSpringRecord();

        // Act
        var possibilities = record.GeneratePossibleValues();

        // Assert
        possibilities.Count().Should().Be(expectedPossibilities);
    }

        //[Fact]
        //public void UniverseReading_CountShortestDistanceMultitude_Part2()
        //{
        //    // Arrange
        //    var oneMillion = 1000000;
        //    var reading = InputReader.ReadCharGrid(11).ToUniverseMultitudeReading(oneMillion);

        //    // Act
        //    var galaxyPairs = reading.GalaxyPairDistances();
        //    var total = galaxyPairs.Select(x => x.Value).Sum();

        //    // Assert
        //    total.Should().BeGreaterThan(1);
        //}
    }