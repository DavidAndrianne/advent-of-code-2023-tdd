using System.Collections.Frozen;

namespace AdventOfCode2023.Tdd.Xunit.Week2.Day9.Domain;

public class EcoMeasurement
{
    public double[] Values { get; set; }
    public List<double[]> ValueHistories { get; set; } 

    public EcoMeasurement(double[] values)
    {
        Values = values;
        ValueHistories = CalculateHistories();
    }

    private List<double[]> CalculateHistories()
    {
        var histories = new List<double[]> { };
        var nextValues = Values;
        var history = new double[nextValues.Length - 1];

        while(histories.Count() == 0 || !history.All(x => x == 0)) {
            history = new double[nextValues.Length - 1];
            for (var i = 0; i < nextValues.Length-1; i++)
            {
                history[i] = nextValues[i+1] - nextValues[i];
            }
            histories.Add(history);
            nextValues = history;
        }

        return histories;
    }

    public double PredictNextValue()
    {
        var result = 0D;
        ValueHistories.Reverse();
        foreach (var history in ValueHistories)
        {
            result += history.Last();
        };
        ValueHistories.Reverse(); // revert back to initial state
        return result + Values.Last();
    }

    public double PredictPreviousValue()
    {
        var result = 0D;
        ValueHistories.Reverse();
        foreach (var history in ValueHistories)
        {
            result = history.First() - result;
        };
        ValueHistories.Reverse(); // revert back to initial state
        return Values.First() - result;
    }
}
