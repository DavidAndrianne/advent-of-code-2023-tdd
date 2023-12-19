namespace AdventOfCode2023.Tdd.Xunit.Week3.Day15.Domain;

public class SatteliteBox
{
    public int Number { get; set; }
    public List<SatteliteLens> Lenses { get; set; } = new List<SatteliteLens> ();
    public SatteliteBox(int number) => Number = number;

    public void Update(SatteliteBoxOperation operation, SatteliteLens lens)
    {
        var existingLens = Lenses.SingleOrDefault(x => x.Label == lens.Label);
        if (operation == SatteliteBoxOperation.Remove) {
            if (existingLens == null) return; // ignore: nothing to remove
            Lenses.Remove(existingLens);
            return;
        }
        if (operation == SatteliteBoxOperation.Put)
        {
            if(lens.FocalLength == null) throw new ApplicationException($"Operation '{operation.Sign}' requires a lens' focal length");
            if (existingLens != null)
            {
                ReplaceLens(existingLens, lens);
                return;
            }
            Lenses.Add(lens);
            return;
        }
        throw new ApplicationException($"Operation '{operation.Sign}' unrecognized");
    }

    private void ReplaceLens(SatteliteLens existingLens, SatteliteLens newLens)
    {
        var indexOfLens = Lenses.IndexOf(existingLens);
        var newLenses = Lenses.Take(indexOfLens).ToList();
        newLenses.Add(newLens);
        Lenses = newLenses.Concat(Lenses.Skip(indexOfLens + 1)).ToList();
    }

    public int CalculateFocusingPower() 
        => Lenses.Select((lens, index) 
            => lens.FocalLength!.Value 
                * (Number + 1) 
                * (index + 1)
            ).Sum();

    public override string ToString()
    {
        var lenses = Lenses.Select(x => x.ToString()).ToArray();
        return $"Box {Number}: {string.Join(" ", lenses)}";
    }
}
