using AdventOfCode2023.Tdd.Xunit.Util;
using AdventOfCode2023.Tdd.Xunit.Week2.Day8.Domain;
using AdventOfCode2023.Tdd.Xunit.Week2.Day8.InputParsers;
using FluentAssertions;
using System.Diagnostics;

namespace AdventOfCode2023.Tdd.Xunit.Week2.Day8;

public class Day8Tests
{
    public static string ExampleInput = @"RL

AAA = (BBB, CCC)
BBB = (DDD, EEE)
CCC = (ZZZ, GGG)
DDD = (DDD, DDD)
EEE = (EEE, EEE)
GGG = (GGG, GGG)
ZZZ = (ZZZ, ZZZ)";

    public string ExpectedSequence = "RL";
    public DesertMapNode[] ExpectedNodes;

    public Day8Tests()
    {
        var a = new DesertMapNode("AAA");
        var b = new DesertMapNode("BBB");
        var c = new DesertMapNode("CCC");
        var d = new DesertMapNode("DDD");
        var e = new DesertMapNode("EEE");
        var g = new DesertMapNode("GGG");
        var z = new DesertMapNode("ZZZ");
        a.SetLeftRight(b, c);
        b.SetLeftRight(d, e);
        c.SetLeftRight(z, g); 
        // default left right is self reference so no need to set others
        ExpectedNodes = new[]
        {
            a,b,c,d,e,g,z
        };
    }
    [Fact]
    public void ParseDesertMap_WithValidInput_ReturnsCorrectValues()
    {
        // Arrange
        var input = ExampleInput.Split(Environment.NewLine);

        // Act
        var desertMap = input.ParseDesertMap();

        // Assert
        desertMap.Sequence.Should().Be("RL");
        desertMap.Nodes.Count().Should().Be(ExpectedNodes.Count());
        desertMap.Nodes.Select(x => x.ToString()).Should().BeEquivalentTo(ExpectedNodes.Select(x => x.ToString()));
    }

    [Fact]
    public void DesertMap_WithExampleSequence_Returns2()
    {
        // Arrange
        var expectedSteps = 2;
        var desertMap = new DesertMap(ExpectedSequence, ExpectedNodes);

        // Act
        var steps = desertMap.FollowSequenceTillDestinationSteps();

        // Assert
        steps.Should().Be(expectedSteps);
    }

    [Fact]
    public void DesertMap_WithAlternateSequence_Returns6()
    {
        // Arrange
        var altMap = @"LLR

AAA = (BBB, BBB)
BBB = (AAA, ZZZ)
ZZZ = (ZZZ, ZZZ)".Split(Environment.NewLine);
        var expectedSteps = 6;
        var desertMap = altMap.ParseDesertMap();

        // Act
        var steps = desertMap.FollowSequenceTillDestinationSteps();

        // Assert
        steps.Should().Be(expectedSteps);
    }

    [Fact]
    public void DesertMap_FollowsToDestination_Part1()
    {
        // Arrange
        var desertMap = InputReader.ReadLinesForDay(8).ParseDesertMap();

        // Act
        var steps = desertMap.FollowSequenceTillDestinationSteps();

        // Assert
        steps.Should().BeGreaterThan(0);
    }

    [Fact]
    public void DesertMap_FollowUntilEndsWithZ_Returns6()
    {
        // Arrange
        var altMap = @"LR

11A = (11B, XXX)
11B = (XXX, 11Z)
11Z = (11B, XXX)
22A = (22B, XXX)
22B = (22C, 22C)
22C = (22Z, 22Z)
22Z = (22B, 22B)
XXX = (XXX, XXX)".Split(Environment.NewLine);
        var expectedSteps = 6;
        var desertMaps = altMap.ParseDesertMaps();

        // Act
        var distinctZSteps = desertMaps.Select(x => x.NextStepTillEndsWithZ(0)).Distinct().OrderByDescending(x => x).ToArray();
        var seqLen = desertMaps.First().Sequence.Length;
        var distinctSequences = distinctZSteps.Select(x => x / seqLen).ToArray();
        var productSeq = distinctSequences.Aggregate((aggr, x) => (aggr % x) != 0 ? aggr*x : aggr);
        var productSteps = productSeq * seqLen;
        //var allDesertMapsAtZ = false;
        //while (!allDesertMapsAtZ) {
        //    var distinctZSteps = desertMaps.Select(x => x.NextStepTillEndsWithZ(stepsTaken)).ToArray().Distinct();
        //    allDesertMapsAtZ = distinctZSteps.Count() == 1;
        //    stepsTaken = distinctZSteps.Max();
        //}

        // Assert
        productSteps.Should().Be(expectedSteps);
    }

    [Fact]
    public void DesertMap_FollowsToDestinations_Part2()
    {
        // Arrange
        var desertMaps = InputReader.ReadLinesForDay(8).ParseDesertMaps();

        // Act
        var distinctZSteps = desertMaps.Select(x => x.NextStepTillEndsWithZ(0)).Distinct().OrderByDescending(x => x).ToArray();
        var seqLen = desertMaps.First().Sequence.Length;
        var distinctSequences = distinctZSteps.Select(x => x / seqLen).ToArray();
        var productSeq = distinctSequences.Aggregate((aggr, x) => (aggr % x) != 0 ? aggr * x : aggr);
        var productSteps = productSeq * seqLen;

        // Assert
        productSteps.Should().BeGreaterThan(0);
        productSteps.Should().BeLessThan(155388722128598);
    }
}
