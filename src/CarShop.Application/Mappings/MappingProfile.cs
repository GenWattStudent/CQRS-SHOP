using AutoMapper;
using CarShop.Application.Commands.CarCommands;
using CarShop.Domain.Entities;

namespace CarShop.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Car, UpdateCarCommand>().ReverseMap();
    }
}
