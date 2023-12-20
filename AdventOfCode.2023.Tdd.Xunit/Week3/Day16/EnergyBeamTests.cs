using AdventOfCode2023.Tdd.Xunit.Util;
using AdventOfCode2023.Tdd.Xunit.Week2.Day13.InputParsers;
using AdventOfCode2023.Tdd.Xunit.Week3.Day16.Domain;
using AdventOfCode2023.Tdd.Xunit.Week3.Day16.InputParsers;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.Tdd.Xunit.Week3.Day16;

public class EnergyBeamTests
{
    public const string Example =@"
.|...\....
|.-.\.....
.....|-...
........|.
..........
.........\
..../.\\..
.-.-/..|..
.|....-|.\
..//.|....";

    [Fact]
    public void ParseEnergyGrid_ReturnsGrid()
    {
        // Arrange
        var input = Example.Trim().ToCharGrid();

        // Act
        var grid = input.ToEnergyGrid();

        // Assert
        grid.MaxX.Should().Be(10);
        grid.MaxY.Should().Be(10);
        grid.Tiles[8][9].Object.Icon.Should().Be('\\');
        grid.Tiles.Sum(row => row.Count(t => t.Object?.Icon == '|'))
            .Should().Be(8);
        grid.Tiles.Sum(row => row.Count(t => t.Object?.Icon == '-'))
            .Should().Be(5);
        grid.Tiles.Sum(row => row.Count(t => t.Object?.Icon == '\\'))
            .Should().Be(6);
        grid.Tiles.Sum(row => row.Count(t => t.Object?.Icon == '/'))
            .Should().Be(4);
    }

    [Fact]
    public void EnergyGrid_Visit_TriggersExpectedTiles()
    {
        // Arrange
        var grid = Example.Trim().ToCharGrid().ToEnergyGrid();
        var beam = new EnergyBeam(0,0);
        var expectedEnergizedTiles = @"######....
.#...#....
.#...#####
.#...##...
.#...##...
.#...##...
.#..####..
########..
.#######..
.#...#.#..";

        // Act
        var total = grid.VisitAndCountEnergized(beam);
        var energizedTiles = grid.EnergizedString();

        // Assert
        total.Should().Be(46);
        energizedTiles.Trim().Should().Be(expectedEnergizedTiles.Trim());
    }

    [Fact]
    public void EnergyGrid_Visit_Part1()
    {
        // Arrange
        var grid = InputReader.ReadCharGrid(16).ToEnergyGrid();
        var beam = new EnergyBeam(0, 0);

        // Act
        var total = grid.VisitAndCountEnergized(beam);

        // Assert
        total.Should().BeGreaterThanOrEqualTo(156);
    }

    [Fact]
    public void EnergyGrid_MaxVisits_Example()
    {
        // Arrange
        var input = Example.Trim().ToCharGrid();

        // Act
        var bestBeamAndTotal = SimulateGridBeams(input);

        // Assert
        bestBeamAndTotal.total.Should().Be(51);
    }

    [Fact]
    public void EnergyGrid_MaxVisits_Example_Part2()
    {
        // Arrange
        var input = InputReader.ReadCharGrid(16);

        // Act
        (EnergyBeam beam, int total) bestBeamAndTotal = SimulateGridBeams(input);

        // Assert
        bestBeamAndTotal.total.Should().BeGreaterThan(51);
    }

    private static (EnergyBeam beam, int total) SimulateGridBeams(char[][] input)
    {
        var rows = input.Count();
        var columns = input.First().Count();
        (EnergyBeam beam, int total) bestBeamAndTotal = (new EnergyBeam(0, 0), 0);

        // Simulate top beams
        for (var y = 0; y < columns; y++)
        {
            var grid = input.ToEnergyGrid();
            var beam = new EnergyBeam(0, y, CardinalDirection.South);
            var result = grid.VisitAndCountEnergized(beam);
            if (result > bestBeamAndTotal.total) bestBeamAndTotal = (beam, result);
        }

        // Simulate left beams
        for (var x = 0; x < rows; x++)
        {
            var grid = input.ToEnergyGrid();
            var beam = new EnergyBeam(x, 0, CardinalDirection.East);
            var result = grid.VisitAndCountEnergized(beam);
            if (result > bestBeamAndTotal.total) bestBeamAndTotal = (beam, result);
        }

        // Simulate bottom beams
        for (var y = 0; y < columns; y++)
        {
            var grid = input.ToEnergyGrid();
            var beam = new EnergyBeam(rows - 1, y, CardinalDirection.North);
            var result = grid.VisitAndCountEnergized(beam);
            if (result > bestBeamAndTotal.total) bestBeamAndTotal = (beam, result);
        }

        // Simulate right beams
        for (var x = 0; x < rows; x++)
        {
            var grid = input.ToEnergyGrid();
            var beam = new EnergyBeam(x, columns - 1, CardinalDirection.West);
            var result = grid.VisitAndCountEnergized(beam);
            if (result > bestBeamAndTotal.total) bestBeamAndTotal = (beam, result);
        }

        return bestBeamAndTotal;
    }
}
