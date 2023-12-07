namespace AdventOfCode2023.Tdd.Xunit.Week1.Day5.Domain;

public class Seed
{
    public double Id { get; set; }

    public Seed(double id) => Id = id;

    public override string ToString() => Id.ToString();
}
