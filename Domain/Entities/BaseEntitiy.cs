
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class BaseEntitiy : IdEntity
{
    [Required]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "FirstName length must be between 3 and 50")]
    public string FirstName { get; set; } = string.Empty!;

    [Required]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "LastName length must be between 3 and 50")]
    public string LastName { get; set; } = string.Empty!;

    [StringLength(50, MinimumLength = 3, ErrorMessage = "FatherName length must be between 3 and 50")]
    public string FatherName { get; set; } = string.Empty!;
    [Required]
    public string AddedTime { get; set; } = string.Empty!;
    [Required]
    public string EditedTime { get; set; } = string.Empty!;
}
