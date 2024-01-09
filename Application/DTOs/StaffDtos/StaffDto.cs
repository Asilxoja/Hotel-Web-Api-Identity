using Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.StaffDtos;

public class StaffDto : BaseEntitiy
{
    [StringLength(50, ErrorMessage = "Name length must be between 3 and 500 characters", MinimumLength = 3)]
    public string Username { get; set; } = string.Empty;

    [StringLength(50, ErrorMessage = "Email length must be between 3 and 500 characters", MinimumLength = 3)]
    public string Email { get; set; } = string.Empty;

    [StringLength(50, ErrorMessage = "Phone number length must be between 3 and 500 characters", MinimumLength = 3)]
    public string PhoneNumber { get; set; } = string.Empty;
    [StringLength(50, ErrorMessage = "Gander type length must be between 3 and 500 characters", MinimumLength = 3)]
    public string Gender { get; set; } = string.Empty;

    [Required]
    public DateTime BirthDate { get; set; }
    [Required, StringLength(50)]
    public string Address { get; set; } = string.Empty;
    [Required, StringLength(1500)]
    public string Description { get; set; } = string.Empty;

    public string Position { get; set; } = string.Empty;
}
