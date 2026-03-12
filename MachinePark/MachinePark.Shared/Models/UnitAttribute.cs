namespace MachinePark.Shared.Models;

[AttributeUsage(AttributeTargets.Field)]
public class UnitAttribute : Attribute
{
    public string Unit { get; }

    public UnitAttribute(string unit)
    {
        Unit = unit;
    }
}