using Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.OrderDtos;

public class OrderDto : IdEntity
{
    public int GuestId { get; set; }
    public string UserId { get; set; } = string.Empty!;
    public int StatusId { get; set; }
    [Required(ErrorMessage = "This Required Example 2020-01-05 Realy easy"), StringLength(16)]
    public string StartDate { get; set; } = string.Empty!;
    [Required(ErrorMessage = "This Required Example 2020-01-05 Realy easy"), StringLength(16)]
    public string EndDate { get; set; } = null!;
}
