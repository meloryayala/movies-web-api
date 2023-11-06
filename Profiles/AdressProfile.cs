using AutoMapper;
using movies_api.Data.Dtos;
using movies_api.Models;

namespace movies_api.Profiles;

public class AdressProfile : Profile
{
    public AdressProfile()
    {
        CreateMap<Adress, ReadAdressDto>();
        CreateMap<CreateAdressDto, Adress>();
        CreateMap<UpdateAdressDto, Adress>();
    }
}