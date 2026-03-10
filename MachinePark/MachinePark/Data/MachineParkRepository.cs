using MachinePark.Shared.Models;

namespace MachinePark.Data;

public class MachineRepository
{
    private readonly List<Machine> _machines =
    [
        new() { Name = "Thermometer A", LastData = "7°", IsOnline = true, LastUpdated = DateTime.Now.Add(new System.TimeSpan(-2, 0, 0, 0)) },
        new() { Name = "Humidity", LastData = "45%", IsOnline = false, LastUpdated = DateTime.Now.Add(new System.TimeSpan(-1, 2, -14, 22)) },
        new() { Name = "Thermometer B", LastData = "22°", IsOnline = false, LastUpdated = DateTime.Now.Add(new System.TimeSpan(-2, -4, 0, 42)) }
    ];

    public List<Machine> GetMachines()
    {
        return _machines;
    }

    public void AddMachine(Machine machine)
    {

        machine.LastUpdated = DateTime.Now;
        _machines.Add(machine);
    }

    public void RemoveMachine(Guid id)
    {
        var machine = _machines.FirstOrDefault(m => m.Id == id);
        if (machine is not null)
            _machines.Remove(machine);
    }

    public void StartMachine(Guid id)
    {
        var machine = _machines.FirstOrDefault(m => m.Id == id);
        if (machine is null)
        {
            return;
        }
        machine.IsOnline = true;
        machine.LastData = "Started";
        machine.LastUpdated = DateTime.Now;
    }

    public void StopMachine(Guid id)
    {
        var machine = _machines.FirstOrDefault(m => m.Id == id);
        if (machine is null)
        {
            return;
        }
        machine.IsOnline = false;
        machine.LastData = "Stopped";
        machine.LastUpdated = DateTime.Now;
    }

    public void UpdateMachineData(Guid id, string data)
    {
        var machine = _machines.FirstOrDefault(m => m.Id == id);
        if (machine is null)
        {
            return;
        }
        machine.LastData = data;
        machine.LastUpdated = DateTime.Now;
    }
}