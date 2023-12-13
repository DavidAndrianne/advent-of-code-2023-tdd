namespace AdventOfCode2023.Tdd.Xunit.Week2.Day8.Domain;

public class DesertMapNode
{
    public string Name { get; set; }
    public DesertMapNode? LeftNode { get; set; }
    public DesertMapNode? RightNode { get; set; }
    public bool IsDeadEnd => LeftNode == this && RightNode == this;

    public DesertMapNode(string name)
    {
        Name = name;
        LeftNode = this;
        RightNode = this;
    }

    public void SetLeftRight(DesertMapNode left, DesertMapNode right)
    {
        LeftNode = left;
        RightNode = right;
    }

    public override string ToString()
        => $"{Name}[Left:{LeftNode.Name}, Right:{RightNode.Name}]";
}