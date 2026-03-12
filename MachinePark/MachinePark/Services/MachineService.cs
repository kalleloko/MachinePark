using MachinePark.Data;
using MachinePark.Hubs;
using MachinePark.Shared.Models;
using Microsoft.AspNetCore.JsonPatch.SystemTextJson;
using Microsoft.AspNetCore.SignalR;

namespace MachinePark.Services;

public class MachineService
{
    private readonly MachineRepository _repository;
    private readonly IHubContext<MachineHub> _hubContext;
    private static readonly Random _random = new();

    public MachineService(
        MachineRepository repository,
        IHubContext<MachineHub> hubContext)
    {
        _repository = repository;
        _hubContext = hubContext;
    }

    public List<Machine> GetMachines()
    {
        return _repository.GetMachines();
    }

    public async Task AddMachine(Machine machine)
    {
        _repository.AddMachine(machine);
        machine = InitMachineData(machine);
        await NotifyMachinesChanged();
    }

    public async Task RemoveMachine(Guid id)
    {
        _repository.RemoveMachine(id);
        await NotifyMachinesChanged();
    }

    public async Task StartMachine(Guid id)
    {
        _repository.StartMachine(id);
        await NotifyMachinesChanged();
    }

    public async Task StopMachine(Guid id)
    {
        _repository.StopMachine(id);
        await NotifyMachinesChanged();
    }

    public async Task<Machine?> UpdateMachine(Guid id, JsonPatchDocument<Machine> patch)
    {
        var machine = _repository.UpdateMachine(id, patch);
        await NotifyMachinesChanged();
        return machine;
    }

    private Task NotifyMachinesChanged()
    {
        return _hubContext.Clients.All.SendAsync("MachinesChanged");
    }

    private Machine InitMachineData(Machine machine)
    {
        machine.LastData = machine.Type switch
        {
            MachineType.Thermometer => 20 + _random.NextDouble() * 10,   // 20–30 °C
            MachineType.Hygrometer => 40 + _random.NextDouble() * 20,    // 40–60 %
            MachineType.Barometer => 990 + _random.NextDouble() * 30,    // 990–1020 hPa
            MachineType.Speedometer => _random.NextDouble() * 1500,      // 0–1500 rpm
            MachineType.PressureSensor => 2 + _random.NextDouble() * 3,  // 2–5 bar
            _ => 0
        };

        machine.LastData = Math.Round(machine.LastData, 1);
        machine.LastUpdated = DateTime.Now;
        return machine;
    }


}