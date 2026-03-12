using MachinePark.Shared.Models;
using System.Reflection;

namespace MachinePark.Shared.Extensions;

public static class MachineTypeExtensions
{
    public static string GetUnit(this MachineType type)
    {
        var member = type.GetType().GetMember(type.ToString()).First();
        var attribute = member.GetCustomAttribute<UnitAttribute>();

        return attribute?.Unit ?? "";
    }
}