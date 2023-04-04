using AutoMapper;
using GasStation.Application.Commands.Fuel.Create;
using GasStation.Application.Commands.Fuel.Update;
using GasStation.Application.Common.Interfaces.Services;
using GasStation.Application.Queries.Fuel.GetAll;
using GasStation.Domain.Entities;

namespace GasStation.Application.Mappings;

public class FuelProfile : Profile
{
    private readonly IDateTimeService _dateTimeService;

    public FuelProfile(IDateTimeService dateTimeService)
    {
        _dateTimeService = dateTimeService;
        
        //Convert CreateFuelRequest to Fuel
        CreateMap<CreateFuelRequest, Fuel>();
        
        //Convert Fuel to GetAllFuelsResponse
        CreateMap<Fuel, GetAllFuelsResponse>();
        
        //Convert UpdateFuelRequest to Fuel
        CreateMap<UpdateFuelRequest, Fuel>()
            .ForMember(f => f.Title, opt => opt.Ignore())
            .ForMember(f => f.Price, opt => opt.MapFrom(src => src.NewPrice))
            .ForMember(f => f.PriceChangeDate, opt => opt.MapFrom(src => _dateTimeService.Now));
    }
}