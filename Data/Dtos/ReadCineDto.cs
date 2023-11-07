using System.ComponentModel.DataAnnotations;
namespace movies_api.Data.Dtos;

public class ReadCineDto
{
    [Key]
    [Required]
    public int Id { get; set; }
    
    [Required(ErrorMessage = "The field name is required")]
    public string Name { get; set; }
    
    public ReadAdressDto Adress { get; set; }
    
    public ICollection<ReadSessionDto> Sessions { get; set; }
}