namespace AdventOfCode2023.Tdd.Xunit.Week1.Day4.Domain;

public class ScratchCardWinner
{
    public Dictionary<int, int> ScratchCardCounts { get; set; }

    public int TotalScratchCards => ScratchCardCounts.Values.Sum();

    public ScratchCardWinner(IEnumerable<ScratchCard> scratchCards)
    {
        ScratchCardCounts = scratchCards.ToDictionary(x => x.Id-1, y => 1);
        scoreCards(scratchCards.ToArray());
    }

    private void scoreCards(ScratchCard[] scratchCards)
    {
        for(var i = 0; i < scratchCards.Count(); i++)
        {
            var card = scratchCards[i];
            for(var j = 0; j < card.WinningEntriesCount; j++) ScratchCardCounts[card.Id+j] += ScratchCardCounts[i];
        }
    }
}
