using AdventOfCode2023.Tdd.Xunit.Week1.Day1.Extensions;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode2023.Tdd.Xunit.Week2.Day12.Domain;

public class SpringRecord
{
    public const char Damaged = '#';
    public const char Working = '.';
    public const char Unknown = '?';

    public static Regex UnknownRegex = new Regex($@"\{Unknown}");

    public string SpringLedger { get; set; }
    public int[] DamagedSpringGroupSizes { get; set; }

    private int expectedDamagedSprings;
    private int damagedSprings;

    public SpringRecord(string ledger, int[] damagedSpringGroupSizes)
    {
        DamagedSpringGroupSizes = damagedSpringGroupSizes;
        expectedDamagedSprings = damagedSpringGroupSizes.Sum();
        SpringLedger = DeduceLedger(ledger, damagedSpringGroupSizes);
        damagedSprings = SpringLedger.Count(x => x == Damaged);
    }

    private static string DeduceLedger(string ledger, int[] damagedSpringGroupSizes)
    {
        if (!damagedSpringGroupSizes.Any()) return ledger; // No deductions left

        var damagePattern = GeneratePattern(damagedSpringGroupSizes);
        if (ledger.Length == damagePattern.Length) return damagePattern;

        (Func<int[], Regex> getPattern, Func<string, int[], string> deduce)[] deductions = [
            // I.E. first damageGroup is 2 '##'
            (
                // start is '??.'
                (damagedSpringGroupSizes) => new Regex(@$"^[#\?]{{{damagedSpringGroupSizes.First()}}}\."),

                // convert to ##.
                (ledger, damagedSpringGroupSizes) =>
                {
                    var damageGroupSymbols = DamagedString(damagedSpringGroupSizes.First());
                    var remainingDamagedGroupSizes = damagedSpringGroupSizes.Skip(1).ToArray();
                    return ContinueSolvingLedgerFromStart($"{damageGroupSymbols}.", ledger, remainingDamagedGroupSizes);
                }
            ),
            (
                // start is '?##?'
                (damagedSpringGroupSizes) => new Regex(@$"^\?[#]{{{damagedSpringGroupSizes.First()}}}"),

                // convert to .##.
                (ledger, damagedSpringGroupSizes) =>
                {
                    var damageGroupSymbols = DamagedString(damagedSpringGroupSizes.First());
                    var remainingDamagedGroupSizes = damagedSpringGroupSizes.Skip(1).ToArray();
                    return ContinueSolvingLedgerFromStart($".{damageGroupSymbols}.", ledger, remainingDamagedGroupSizes);
                }
            ),
            (
                // start is '##?'
                (damagedSpringGroupSizes) => new Regex(@$"^[#]{{{damagedSpringGroupSizes.First()}}}\?"),

                // convert to ##.
                (ledger, damagedSpringGroupSizes) =>
                {
                    var damageGroupSymbols = DamagedString(damagedSpringGroupSizes.First());
                    var remainingDamagedGroupSizes = damagedSpringGroupSizes.Skip(1).ToArray();
                    return ContinueSolvingLedgerFromStart($"{damageGroupSymbols}.", ledger, remainingDamagedGroupSizes);
                }
            ),
            (
                // start is '???#'
                (damagedSpringGroupSizes) => new Regex(@$"^[#\?]{{{damagedSpringGroupSizes.First()}}}\?\#"),

                // convert to ##.#
                (ledger, damagedSpringGroupSizes) =>
                {
                    var damageGroupSymbols = DamagedString(damagedSpringGroupSizes.First());
                    var remainingDamagedGroupSizes = damagedSpringGroupSizes.Skip(1).ToArray();
                    return ContinueSolvingLedgerFromStart($"{damageGroupSymbols}.", ledger, remainingDamagedGroupSizes);
                }
            ),
            (
                // end is '.??'
                (damagedSpringGroupSizes) => new Regex(@$"\.[#\?]{{{damagedSpringGroupSizes.First()}}}\?$"),

                // convert to .##
                (ledger, damagedSpringGroupSizes) =>
                {
                    var damageGroupSymbols = DamagedString(damagedSpringGroupSizes.Last());
                    var remainingDamagedGroupSizes = damagedSpringGroupSizes.Take(damagedSpringGroupSizes.Length - 1).ToArray();
                    return ContinueSolvingLedgerFromEnd($".{damageGroupSymbols}", ledger, remainingDamagedGroupSizes);
                }
            ),
            (
                // end is '??.'
                (damagedSpringGroupSizes) => new Regex(@$"[#\?]{{{damagedSpringGroupSizes.Last()}}}\.$"),

                // convert to .##.
                (ledger, damagedSpringGroupSizes) =>
                {
                    var damageGroupSymbols = DamagedString(damagedSpringGroupSizes.Last());
                    var remainingDamagedGroupSizes = damagedSpringGroupSizes.Take(damagedSpringGroupSizes.Length - 1).ToArray();
                    return ContinueSolvingLedgerFromEnd($".{damageGroupSymbols}.", ledger, remainingDamagedGroupSizes);
                }
            ),
            (
                // end is '?##'
                (damagedSpringGroupSizes) => new Regex(@$"\?[#]{{{damagedSpringGroupSizes.Last()}}}$"),

                // convert to .##
                (ledger, damagedSpringGroupSizes) =>
                {
                    var damageGroupSymbols = DamagedString(damagedSpringGroupSizes.Last());
                    var remainingDamagedGroupSizes = damagedSpringGroupSizes.Take(damagedSpringGroupSizes.Length - 1).ToArray();
                    return ContinueSolvingLedgerFromEnd($".{damageGroupSymbols}", ledger, remainingDamagedGroupSizes);
                }
            ),
            (
                // end is '##?'
                (damagedSpringGroupSizes) => new Regex(@$"[#]{{{damagedSpringGroupSizes.Last()}}}\?$"),

                // convert to .##.
                (ledger, damagedSpringGroupSizes) =>
                {
                    var damageGroupSymbols = DamagedString(damagedSpringGroupSizes.Last());
                    var remainingDamagedGroupSizes = damagedSpringGroupSizes.Take(damagedSpringGroupSizes.Length - 1).ToArray();
                    return ContinueSolvingLedgerFromEnd($".{damageGroupSymbols}.", ledger, remainingDamagedGroupSizes);
                }
            ),
            (
                // end is '#???'
                (damagedSpringGroupSizes) => new Regex(@$"#\?[#\?]{{{damagedSpringGroupSizes.Last()}}}$"),

                // convert to #.##
                (ledger, damagedSpringGroupSizes) =>
                {
                    var damageGroupSymbols = DamagedString(damagedSpringGroupSizes.Last());
                    var remainingDamagedGroupSizes = damagedSpringGroupSizes.Take(damagedSpringGroupSizes.Length - 1).ToArray();
                    return ContinueSolvingLedgerFromEnd($".{damageGroupSymbols}", ledger, remainingDamagedGroupSizes);
                }
            )
        ];

        var applicableDeductions = deductions.Where(d => d.getPattern(damagedSpringGroupSizes).IsMatch(ledger));
        if (applicableDeductions.Any()) return applicableDeductions.First().deduce(ledger, damagedSpringGroupSizes);

        // if a damage count is unique
        var uniqueCounts = damagedSpringGroupSizes.Where(count => damagedSpringGroupSizes.Count(x => x == count) == 1)
            .ToArray();
        foreach(var c in uniqueCounts)
        {
            // and matches once
            var matches = new Regex(@$".+\.[#\?]{{{c}}}\..+").Matches(ledger);
            if (matches.Count() > 1) continue;

            // then replace the ?? with damage symbols and consider this group solved
            var solved = $".{DamagedString(c)}.";
            ledger = ledger.Substring(matches.First().Index-1) + solved + ledger.Substring(matches.First().Index + solved.Length-1);
            return DeduceLedger(ledger, damagedSpringGroupSizes.Where(x => x != c).ToArray());
        }

        return ledger;
    }

    public string[] GeneratePossibleValues()
    {
        int unknownCount = SpringLedger.Count(c => c == Unknown);
        if (unknownCount == 0) return [SpringLedger];

        double combinations = Math.Pow(2D, unknownCount);
        var result = new List<string>();
        var regex = new Regex(GeneratePattern(DamagedSpringGroupSizes));
        for (var i = 0; i < combinations; i++)
        {
            var maybeScenario = maybeGenerateScenario(i, unknownCount);
            if(maybeScenario != null && regex.IsMatch(maybeScenario)) result.Add(maybeScenario);
        }
        return result.ToArray();
    }

    private string? maybeGenerateScenario(double seed, int padding)
    {
        var mask = toBinaryMask((long)seed, padding);
        if ((mask.Count(x => x == '0') + damagedSprings) != expectedDamagedSprings) return null;

        var result = SpringLedger;
        foreach (var c in mask)
        {
            var unknownIndex = result.IndexOf(Unknown);
            result = new string(
                result.Take(unknownIndex)
                .Concat([c == '1' ? Working : Damaged])
                .Concat(result.Skip(unknownIndex+1).Take(result.Length - unknownIndex))
                .ToArray()
                );
        }
        return result;
    }

    private static string toBinaryMask(long seed, int padding)
        => Convert.ToString(seed, 2).PadLeft(padding, '0');

    private static string ContinueSolvingLedgerFromStart(string solved, string ledger, int[] damagedSpringGroupSizes)
    {
        var remainingPartToSolve = ledger.Substring(solved.Length);
        var result = solved + DeduceLedger(remainingPartToSolve, damagedSpringGroupSizes);
        return result;
    }

    private static string ContinueSolvingLedgerFromEnd(string solved, string ledger, int[] damagedSpringGroupSizes)
    {
        var remainingPartToSolve = ledger.Substring(0, ledger.Length - solved.Length);
        var result = DeduceLedger(remainingPartToSolve, damagedSpringGroupSizes) + solved;
        return result;
    }

    public static string GeneratePattern(int[] damagedSpringGroupSizes)
    {
        var sb = new StringBuilder(@"^\.*");
        foreach (var i in damagedSpringGroupSizes)
        {
            var damageGroup = DamagedString(i);
            sb.Append(@$"{damageGroup}\.+");
        }
        return sb.ToString().ReplaceLastOccurance("+", "*$");
    }

    private static string DamagedString(int i) => new string(new char[] { }).PadLeft(i, Damaged);
}
