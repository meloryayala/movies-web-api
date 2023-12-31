using System.ComponentModel.DataAnnotations;
namespace movies_api.Models;

public class Movie
{
    [Key]
    [Required]
    public int Id { get; set; }
    
    [Required(ErrorMessage = "The movie title is required")]
    public string Title { get; set; }
    
    [Required(ErrorMessage = "The movie genre is required")]
    [MaxLength(50, ErrorMessage = "The genre can not have more than 50 characters")]
    public string Genre { get; set; }

    [Required(ErrorMessage = "The movie duration is required")]
    [Range(70, 600, ErrorMessage = "The movie duration must have between 70 and 600 minutes")]
    public int Duration { get; set; }
    
    public virtual ICollection<Session> Sessions { get; set; }
}