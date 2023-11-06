using System.ComponentModel.DataAnnotations;

namespace movies_api.Models;

public class Adress
{
    [Key]
    [Required]
    public int Id { get; set; }
    
    [Required(ErrorMessage = "The street adress is required")]
    public string Street { get; set; }
    
    [Required(ErrorMessage = "The number adress is required")]
    public int Number { get; set; }
    
    public virtual Cine Cine { get; set; }
}