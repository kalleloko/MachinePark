namespace MachinePark.Client.Models;

public class Machine
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = string.Empty;
    public bool IsOnline { get; set; }
    public string LastData { get; set; } = string.Empty;
    public DateTime LastUpdated { get; set; }
}
