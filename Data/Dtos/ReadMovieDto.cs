namespace movies_api.Data.Dtos;

public class ReadMovieDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Genre { get; set; }
    public int Duration { get; set; }
    public DateTime TimeConsult { get; set; } = DateTime.Now;
    public ICollection<ReadSessionDto> Sessions { get; set; }
}