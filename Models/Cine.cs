using System.ComponentModel.DataAnnotations;

namespace movies_api.Models;

public class Cine
{
    [Key]
    [Required]
    public int Id { get; set; }
    
    [Required(ErrorMessage = "The field name is required")]
    public string Name { get; set; }
    
    public int AdressId { get; set; }
    
    public virtual Adress Adress { get; set; }
    
    public virtual ICollection<Session> Sessions { get; set; }
}