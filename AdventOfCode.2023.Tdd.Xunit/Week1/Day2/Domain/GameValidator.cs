using FluentValidation;

namespace AdventOfCode2023.Tdd.Xunit.Week1.Day2.Domain;

public class GameValidator : AbstractValidator<Game>
{
    public GameValidator()
    {
        RuleForEach(x => x.RedCubeCounts)
            .LessThanOrEqualTo(12);
        RuleForEach(x => x.GreenCubeCounts)
            .LessThanOrEqualTo(13);
        RuleForEach(x => x.BlueCubeCounts)
            .LessThanOrEqualTo(14);
    }
}
