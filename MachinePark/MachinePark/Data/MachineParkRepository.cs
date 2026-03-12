using MachinePark.Shared.Models;
using Microsoft.AspNetCore.JsonPatch.SystemTextJson;

namespace MachinePark.Data;

public class MachineRepository
{
    private readonly List<Machine> _machines =
    [
        new() { Name = "Thermometer A", LastData = 7, Type = MachineType.Thermometer, IsOnline = true, LastUpdated = DateTime.Now.Add(new System.TimeSpan(-2, 0, 0, 0)) },
        new() { Name = "Humidity", LastData = 45, Type = MachineType.Hygrometer, IsOnline = false, LastUpdated = DateTime.Now.Add(new System.TimeSpan(-1, 2, -14, 22)) },
        new() { Name = "Thermometer B", LastData = 22, Type = MachineType.Thermometer, IsOnline = false, LastUpdated = DateTime.Now.Add(new System.TimeSpan(-2, -4, 0, 42)) }
    ];

    public List<Machine> GetMachines()
    {
        return _machines;
    }

    public Machine? GetMachine(Guid id)
    {
        return _machines.FirstOrDefault(m => m.Id == id);
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
        machine.LastUpdated = DateTime.Now;
    }

    public Machine? UpdateMachine(Guid id, JsonPatchDocument<Machine> patch)
    {
        var machine = _machines.FirstOrDefault(m => m.Id == id);
        if (machine is null)
        {
            return null;
        }
        patch.ApplyTo(machine);
        machine.LastUpdated = DateTime.Now;
        return machine;
    }
}