using System.ComponentModel.DataAnnotations;

namespace MachinePark.Shared.Models;

public class UploadMachineData
{
    public Guid Id { get; set; }
    [MinLength(1, ErrorMessage = "Data must be entered")]
    [MaxLength(15, ErrorMessage = "Data can’t be longer than 15 characters.")]
    public string Data { get; set; } = string.Empty;
}
