using System.ComponentModel.DataAnnotations;

namespace movies_api.Models;

public class Session
{
    [Key]
    [Required]
    public int Id { get; set; }
    
    [Required]
    public int MovieId { get; set; }
    
    public virtual Movie Movie { get; set; }
    
    public int? CineId { get; set; }
    
    public virtual Cine Cine { get; set; }
}