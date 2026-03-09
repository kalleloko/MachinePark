using System.ComponentModel.DataAnnotations;

namespace MachinePark.Client.Models;

public class AddToDoItem
{
    [Required(ErrorMessage = "Please enter a name.")]
    [MinLength(2, ErrorMessage = "Name must be at least 2 characters.")]
    [MaxLength(15, ErrorMessage = "Name can’t be longer than 15 characters.")]
    public string Name { get; set; } = string.Empty;
    public bool IsOnline { get; set; } = false;

    [MaxLength(15, ErrorMessage = "Data can’t be longer than 15 characters.")]
    public string LastData { get; set; } = string.Empty;
    public DateTime LastUpdated { get; set; } = DateTime.Now;
}
