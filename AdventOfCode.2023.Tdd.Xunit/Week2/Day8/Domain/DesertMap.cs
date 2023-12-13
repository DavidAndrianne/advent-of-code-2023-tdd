namespace AdventOfCode2023.Tdd.Xunit.Week2.Day8.Domain;

public class DesertMap
{
    public string Sequence { get; set; }
    public IEnumerable<DesertMapNode> Nodes { get; set; }
    
    public DesertMapNode CurrentNode { get; set; }
    public double StepsTaken { get; set; }

    public DesertMap(string sequence, IEnumerable<DesertMapNode> nodes, string startNode = "AAA")
    {
        Sequence = sequence;
        Nodes = nodes;
        CurrentNode = nodes.FirstOrDefault(x => x.Name == startNode) ?? Nodes.First();
    }

    public double FollowSequenceTillDestinationSteps()
    {
        while(CurrentNode.Name != "ZZZ") NextStepTillEndsWithZ(StepsTaken);
        return StepsTaken;
    }

    public double NextStepTillEndsWithZ(double minStepsToTake)
    {
        while (!CurrentNode.Name.EndsWith("Z") || StepsTaken < minStepsToTake || StepsTaken % Sequence.Length != 0) { 
            var direction = Sequence[(int)(StepsTaken % Sequence.Length)];
            var priorNode = CurrentNode;
            CurrentNode = direction == 'L'
                ? CurrentNode!.LeftNode
                : CurrentNode!.RightNode;
            if (CurrentNode.IsDeadEnd) throw new ArgumentException($"Inf. loop detected! step {direction}{StepsTaken}, from {priorNode} to {CurrentNode.Name}");
            StepsTaken++;
        }
        return StepsTaken;
    }

    public override string ToString()
        => $"{Nodes.Count()} nodes: {Sequence}";
}