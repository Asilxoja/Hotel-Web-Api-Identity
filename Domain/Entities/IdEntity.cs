using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class IdEntity
{
    [Key, Required]
    public int Id { get; set; }
}
