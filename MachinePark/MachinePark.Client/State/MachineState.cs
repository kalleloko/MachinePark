namespace MachinePark.Client.State;

public class MachineState
{
    public int Count { get; private set; }

    public event Action? Onchange;

    public void SetCount(int count)
    {
        Count = count;
        Onchange?.Invoke();
    }
}
