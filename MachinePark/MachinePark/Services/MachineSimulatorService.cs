using MachinePark.Data;
using MachinePark.Hubs;
using MachinePark.Shared.Models;
using Microsoft.AspNetCore.SignalR;

namespace MachinePark.Services;

public class MachineSimulatorService : BackgroundService
{
    private readonly MachineRepository _repository;
    private readonly Random _random = new();
    private readonly IHubContext<MachineHub> _hubContext;

    public MachineSimulatorService(MachineRepository machineRepository, IHubContext<MachineHub> hubContext)
    {
        _repository = machineRepository;
        _hubContext = hubContext;
    }

    protected override async Task ExecuteAsync(CancellationToken ct)
    {
        while (!ct.IsCancellationRequested)
        {
            await Task.Delay(3000, ct);

            var machines = _repository.GetMachines().ToList();
            if (!machines.Any())
                continue;

            foreach (var machine in machines)
            {
                UpdateMachineData(machine);
            }

            await _hubContext.Clients.All.SendAsync("MachinesChanged");
        }
    }

    public void UpdateMachineData(Machine machine)
    {
        if (!machine.IsOnline)
        {
            return;
        }
        double change = machine.Type switch
        {
            MachineType.Thermometer => (_random.NextDouble() - 0.5) * 1.0,   // ±0.5 °C
            MachineType.Hygrometer => (_random.NextDouble() - 0.5) * 2.0,    // ±1 %
            MachineType.Barometer => (_random.NextDouble() - 0.5) * 1.0,     // ±0.5 hPa
            MachineType.Speedometer => (_random.NextDouble() - 0.5) * 50.0,  // ±25 rpm
            MachineType.PressureSensor => (_random.NextDouble() - 0.5) * 0.2,// ±0.1 bar
            _ => 0
        };

        machine.LastData += Math.Round(change, 1);
        machine.LastUpdated = DateTime.Now;
    }
}