namespace MachinePark.Shared.Models;

public enum MachineType
{
    [Unit("°C")]
    Thermometer,

    [Unit("%")]
    Hygrometer,

    [Unit(" hPa")]
    Barometer,

    [Unit(" rpm")]
    Speedometer,

    [Unit(" bar")]
    PressureSensor
}