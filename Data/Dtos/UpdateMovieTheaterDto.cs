using System.ComponentModel.DataAnnotations;
namespace movies_api.Data.Dtos;

public class UpdateMovieTheaterDto
{
    [Required(ErrorMessage = "The field name is required")]
    public string Name { get; set; }
}