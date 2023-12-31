﻿namespace AdventOfCode2023.Tdd.Xunit.Week1.Day5.Domain;

public class AlmanacMapReference
{
    public AlmanacEntry Entry { get; set; }
    public double SourceIndex { get; set; }
    public double TargetIndex { get; set; }
    public List<AlmanacMapReference> PriorReferences { get; set; }

    public AlmanacMapReference(AlmanacEntry entry, double sourceIndex, double targetIndex)
    {
        Entry = entry;
        SourceIndex = sourceIndex;
        TargetIndex = targetIndex;
    }

    public override string ToString()
        => $"{Entry.Source.Name}({SourceIndex}) => {Entry.Target.Name}({TargetIndex})";
}