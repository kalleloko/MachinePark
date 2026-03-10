using MachinePark.Shared.Models;

namespace MachinePark.State;

public class MachineState
{
    public int CountTotal { get; private set; }
    public int CountOnline { get; private set; }
    public int CountOffline { get; private set; }

    public event Action? OnChange;

    public void SetCount(IReadOnlyList<Machine> machines)
    {
        CountTotal = machines.Count;
        CountOnline = machines.Count(m => m.IsOnline);
        CountOffline = CountTotal - CountOnline;
        NotifyStateChanged();
    }

    public void NotifyStateChanged() => OnChange?.Invoke();
}