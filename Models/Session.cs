using System.ComponentModel.DataAnnotations;

namespace movies_api.Models;

public class Session
{
    [Key]
    [Required]
    public int Id { get; set; }
}