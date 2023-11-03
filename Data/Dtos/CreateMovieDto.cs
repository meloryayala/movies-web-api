using System.ComponentModel.DataAnnotations;
namespace movies_api.Data.Dtos;

public class CreateMovieDto
{
    
    [Required(ErrorMessage = "The movie title is required")]
    public string Title { get; set; }
    
    [Required(ErrorMessage = "The movie genre is required")]
    [StringLength(50, ErrorMessage = "The genre can not have more than 50 characters")]
    public string Genre { get; set; }

    [Required(ErrorMessage = "The movie duration is required")]
    [Range(70, 600, ErrorMessage = "The movie duration must have between 70 and 600 minutes")]
    public int Duration { get; set; }
}