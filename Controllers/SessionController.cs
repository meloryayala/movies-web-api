using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using movies_api.Data;
using movies_api.Data.Dtos;
using movies_api.Models;

namespace movies_api.Controllers;

[ApiController]
[Route("[controller]")]
public class SessionController : ControllerBase
{
    private MovieContext _context;
    private IMapper _mapper;

    public SessionController(MovieContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpPost]
    public IActionResult AddSession([FromBody] CreateSessionDto sessionDto)
    {
        Session session = _mapper.Map<Session>(sessionDto);
        _context.Sessions.Add(session);
        _context.SaveChanges();
        return CreatedAtAction(nameof(ReadSessionById), new { Id = session.Id }, session);
    }

    [HttpGet]
    public IEnumerable<ReadSessionDto> ReadSessions([FromQuery] int skip = 0, [FromQuery] int take = 50)
    {
        var sessionList = _context.Sessions.Skip(skip).Take(take).ToList();
        return _mapper.Map<List<ReadSessionDto>>(sessionList);
    }

    [HttpGet("{id}")]
    public IActionResult ReadSessionById(int id)
    {
        var session = _context.Sessions.FirstOrDefault(session => session.Id == id);
        if (session == null) return NotFound();
        ReadSessionDto sessionDto =  _mapper.Map<ReadSessionDto>(session);
        return Ok(sessionDto);
    }
}