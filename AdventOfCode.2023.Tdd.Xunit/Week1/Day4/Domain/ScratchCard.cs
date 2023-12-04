namespace AdventOfCode2023.Tdd.Xunit.Week1.Day4.Domain;

public class ScratchCard
{
    public int Id { get; set; }
    public int[] WinningNumbers { get; set; }
    public int[] Entries { get; set; }

    public int WinningEntriesCount => WinningNumbers.Where(x => Entries.Any(y => y == x)).Count();

    public int Score
    {
        get
        {
            var winCount = WinningEntriesCount;
            return winCount > 2 
                ? (int)Math.Pow(2, winCount - 1)
                : winCount;
        }
    }

    public ScratchCard(int id, int[] winningNumbers, int[] entries)
    {
        Id = id;
        WinningNumbers = winningNumbers;
        Entries = entries;
    }
}
