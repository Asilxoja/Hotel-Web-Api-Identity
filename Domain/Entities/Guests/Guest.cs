using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.Guests;


public class Guest : BaseEntitiy
{
    [StringLength(15)]
    public string PhoneNumber { get; set; } = string.Empty;

    [Required]
    [StringLength(60, ErrorMessage = "Passport length must be less than 60")]
    public string Passport { get; set; } = string.Empty;

    public string BirthDate { get; set; } = string.Empty!;
    public string DateOfIssue { get; set; } = string.Empty!;

    [StringLength(100)]
    public string Address { get; set; } = string.Empty;

    [StringLength(50)]
    public string OrganizationName { get; set; } = string.Empty;

    [StringLength(50)]
    public string Givenbywhom { get; set; } = string.Empty;

    [StringLength(50)]
    public string Citizenship { get; set; } = string.Empty;

    [StringLength(50)]
    public string Description { get; set; } = string.Empty;

    [StringLength(50)]
    public string Organization { get; set; } = string.Empty;

    [StringLength(5)]
    public string Gender { get; set; } = string.Empty;

    [Required]
    public string UserId { get; set; } = string.Empty!;
}