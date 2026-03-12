using System.ComponentModel.DataAnnotations;

namespace MachinePark.Shared.Models;

public class UploadMachineData
{
    public Guid Id { get; set; }
    [Required(ErrorMessage = "Enter a value.")]
    [Range(-1000, 1000, ErrorMessage = "Value is out of range.")]
    public double Data { get; set; }
}
