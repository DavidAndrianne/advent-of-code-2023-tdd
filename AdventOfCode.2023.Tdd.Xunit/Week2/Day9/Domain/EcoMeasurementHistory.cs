using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        var history = new double[Values.Length - 1];
        var nextValues = Values;

        while(!history.All(x => x == 0)) { 
            for(var i = 0; i < nextValues.Length-1; i++)
            {
                history[i] = nextValues[i+1] - nextValues[i];
            }
            histories.Add(history);
            nextValues = history;
        }

        return histories;
    }
}
