using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.Tdd.Xunit.Week1.Day7.Domain;

public class Hand : IComparable<Hand>
{
    public HandType Type { get; set; }
    public string Cards { get; set; }
    public double Bid { get; set; }

    public bool IsApplyingJoker { get; }

    public Hand(string cards, double bid, bool isApplyingJoker = false)
    {
        Cards = cards;
        Bid = bid;
        Type = isApplyingJoker
            ? HandType.ParseJoker(cards)
            : HandType.Parse(cards);
        IsApplyingJoker = isApplyingJoker;
    }

    public int CompareTo(Hand? other)
    {
        // HandType priority
        if (Type.Priority != other.Type.Priority) return other.Type.Priority - Type.Priority;
        
        // Equal
        if (Cards == other.Cards) return 0;

        // Highest card by order
        for(var i = 0; i < 5; i++)
        {
            var cardPriorityA = IsApplyingJoker ? CardPriority.GetJoker(Cards[i]) : CardPriority.Get(Cards[i]);
            var cardPriorityB = IsApplyingJoker ? CardPriority.GetJoker(other.Cards[i]) : CardPriority.Get(other.Cards[i]);
            if (cardPriorityA != cardPriorityB) return cardPriorityB - cardPriorityA;
        }

        throw new ArgumentException($"Couldn't determine hand {other.Cards} is bigger or smaller than {Cards}");
    }

    public override string ToString() 
        => $"{Cards}({Type.Name})";
}
