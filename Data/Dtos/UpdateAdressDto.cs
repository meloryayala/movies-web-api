using System.ComponentModel.DataAnnotations;
namespace movies_api.Data.Dtos;

public class UpdateAdressDto
{
    [Required(ErrorMessage = "The street adress is required")]
    public string Street { get; set; }
    
    [Required(ErrorMessage = "The number adress is required")]
    public int Number { get; set; }
}