using System.ComponentModel.DataAnnotations;
namespace movies_api.Data.Dtos;

public class UpdateCineDto
{
    [Required(ErrorMessage = "The field name is required")]
    public string Name { get; set; }
}