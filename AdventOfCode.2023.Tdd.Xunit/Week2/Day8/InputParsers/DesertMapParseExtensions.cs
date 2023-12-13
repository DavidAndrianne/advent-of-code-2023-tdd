using AdventOfCode2023.Tdd.Xunit.Week2.Day8.Domain;
using System.Text.RegularExpressions;

namespace AdventOfCode2023.Tdd.Xunit.Week2.Day8.InputParsers;

public static class DesertMapParseExtensions
{
    private static Regex DesertNodePattern = new Regex(@"(\w{3}) = \((\w{3}), (\w{3})\)");
    public static DesertMap ParseDesertMap(this string[] input)
    {
        var nodes = new Dictionary<string, DesertMapNode>();
        var sequence = input.First();

        foreach(var line in input.Skip(2))
        {
            var groups = DesertNodePattern.Match(line).Groups.Values.Skip(1);
            var node = nodes.AddNodeIfMissing(groups.First().Value);
            node.LeftNode = nodes.AddNodeIfMissing(groups.Skip(1).First().Value);
            node.RightNode = nodes.AddNodeIfMissing(groups.Skip(2).First().Value);
        }
        return new DesertMap(sequence, nodes.Values);
    }

    private static DesertMapNode AddNodeIfMissing(this Dictionary<string, DesertMapNode> nodes, string name)
    {
        if (!nodes.ContainsKey(name)) nodes.Add(name, new DesertMapNode(name));
        return nodes[name];
    }

    public static DesertMap[] ParseDesertMaps(this string[] input)
    {
        var defaultMap = input.ParseDesertMap();

        var startNodes = defaultMap.Nodes.Where(x => x.Name.EndsWith("A")).ToArray();
        return startNodes.Select(x => new DesertMap(defaultMap.Sequence, defaultMap.Nodes, x.Name))
            .ToArray();
    }
}
