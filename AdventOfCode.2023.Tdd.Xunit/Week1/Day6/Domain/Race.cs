using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.Tdd.Xunit.Week1.Day6.Domain;

public class Race
{
    public double TimeInMilliseconds { get; set; }
    public double RecordDistanceInMillimeter { get; set; }

    public Race(double time, double distance)
    {
        TimeInMilliseconds = time;
        RecordDistanceInMillimeter = distance;
    }

    public double HoldDownButtonForMs(double timeToHoldDown)
    {
        if (timeToHoldDown >= TimeInMilliseconds) throw new ArgumentException($"can't hold the button longer or for entire race ({timeToHoldDown} >= {TimeInMilliseconds})");
        
        var timeRemaining = TimeInMilliseconds - timeToHoldDown;
        var speed = timeToHoldDown;
        return timeRemaining * speed;
    }

    public int CalculateTotalWaysToBeatRecord()
    {
        var total = 0;
        for(var i = 1; i < TimeInMilliseconds-1; i++)
        {
            var distance = HoldDownButtonForMs(i);
            var isBreakingRecord = distance > RecordDistanceInMillimeter;
            if (isBreakingRecord) total++;
            if (total < 0 && !isBreakingRecord) break; // further attempts won't break record anymore
        }
        return total;
    }

    public double CalculateTotalWaysToBeatRecord2()
    {
        var minTime = -1D;
        var maxTime = -1D;
        for (var i = 1; i < TimeInMilliseconds - 1; i++)
        {
            var distance = HoldDownButtonForMs(i);
            var isBreakingRecord = distance > RecordDistanceInMillimeter;
            if (isBreakingRecord)
            {
                minTime = i;
                break;
            }
        }
        for (var i = TimeInMilliseconds-1; i > 1; i--)
        {
            var distance = HoldDownButtonForMs(i);
            var isBreakingRecord = distance > RecordDistanceInMillimeter;
            if (isBreakingRecord)
            {
                maxTime = i;
                break;
            }
        }
        return maxTime-minTime+1;
    }

    public override string ToString() 
        => $"Race record {TimeInMilliseconds}ms: - {RecordDistanceInMillimeter}mm";
}
