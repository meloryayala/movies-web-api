using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using movies_api.Data;
using movies_api.Data.Dtos;
using movies_api.Models;

namespace movies_api.Controllers;

[ApiController]
[Route("[controller]")]
public class AdressController : ControllerBase
{
    private MovieContext _context;
    private IMapper _mapper;

    public AdressController(MovieContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpPost]
    public IActionResult AddAdress([FromBody] CreateAdressDto adressDto)
    {
        Adress adress = _mapper.Map<Adress>(adressDto);
        _context.Adresses.Add(adress);
        _context.SaveChanges();
        return CreatedAtAction(nameof(ReadAdressById), new { Id = adress.Id }, adress);
    }

    [HttpGet]
    public IEnumerable<ReadAdressDto> ReadAdresses([FromQuery] int skip = 0, [FromQuery] int take = 50)
    {
        var adressList = _context.Adresses.Skip(skip).Take(take).ToList();
        return _mapper.Map<List<ReadAdressDto>>(adressList);
    }

    [HttpGet("{id}")]
    public IActionResult ReadAdressById(int id)
    {
        var adress = _context.Adresses.FirstOrDefault(adress => adress.Id == id);
        if (adress == null) return NotFound();
        ReadAdressDto adressDto = _mapper.Map<ReadAdressDto>(adress);
        return Ok(adressDto);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateAdress(int id,[FromBody] UpdateAdressDto adressDto)
    {
        var adress = _context.Adresses.FirstOrDefault(adress => adress.Id == id);
        if (adress == null) return NotFound();
        _mapper.Map(adressDto, adress);
        _context.SaveChanges();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteAdress(int id)
    {
        var adress = _context.Adresses.FirstOrDefault(adress => adress.Id == id);
        if (adress == null) return NotFound();
        _context.Adresses.Remove(adress);
        _context.SaveChanges();
        return NoContent();
    }
}