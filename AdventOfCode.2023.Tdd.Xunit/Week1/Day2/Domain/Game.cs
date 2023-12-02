namespace AdventOfCode2023.Tdd.Xunit.Week1.Day2.Domain;

public class Game
{
    public int Id { get; set; }
    public int[] RedCubeCounts { get; set; }
    public int[] GreenCubeCounts { get; set; }
    public int[] BlueCubeCounts { get; set; }

    public Game(int id, int[] red, int[] green, int[] blue)
    {
        Id = id;
        RedCubeCounts = red;
        GreenCubeCounts = green;
        BlueCubeCounts = blue;
    }

    public static GameValidator Validator = new GameValidator();
    public bool IsValid() 
        => Validator.Validate(this).IsValid;

    public int CalculateMinCubePower() 
        => RedCubeCounts.Max() 
        * GreenCubeCounts.Max() 
        * BlueCubeCounts.Max();
}
