using AutoMapper;
using movies_api.Data.Dtos;
using movies_api.Models;
namespace movies_api.Profiles;

public class CineProfile : Profile
{
    public CineProfile()
    {
        CreateMap<CreateCineDto, Cine>();
        CreateMap<UpdateCineDto, Cine>();
        CreateMap<Cine, ReadCineDto>();
    }
}