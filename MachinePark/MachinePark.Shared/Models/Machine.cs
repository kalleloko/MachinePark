using MachinePark.Shared.Extensions;

namespace MachinePark.Shared.Models;

public class Machine
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = string.Empty;

    public MachineType Type { get; set; }
    public bool IsOnline { get; set; }
    public double LastData { get; set; }
    public DateTime LastUpdated { get; set; }
    public string Unit => Type.GetUnit();
    public string DisplayValue => LastData.ToString("F1") + " " + Unit;

    public void UpdateValue(Random random)
    {
        double change = Type switch
        {
            MachineType.Thermometer => (random.NextDouble() - 0.5) * 1.0,   // ±0.5 °C
            MachineType.Hygrometer => (random.NextDouble() - 0.5) * 2.0,    // ±1 %
            MachineType.Barometer => (random.NextDouble() - 0.5) * 1.0,     // ±0.5 hPa
            MachineType.Speedometer => (random.NextDouble() - 0.5) * 50.0,  // ±25 rpm
            MachineType.PressureSensor => (random.NextDouble() - 0.5) * 0.2,// ±0.1 bar
            _ => 0
        };

        LastData += change;
        LastUpdated = DateTime.Now;
    }
}
