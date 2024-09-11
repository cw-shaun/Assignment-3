using AutoMapper;
using StockApi.Dto;
public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        // CreateMap<StockSearchRequestDTO, Filters>()
        //     .ForMember(dest => dest.FuelTypes, opt => opt.MapFrom(src => src.FuelTypes.Select(f => (FuelType)f).ToList()));
        CreateMap<StockSearchRequestDTO, Filters>()
            .ForMember(dest => dest.FuelTypes, opt => opt.Ignore()); 
        CreateMap<Stock, StockDTO>()
            .ForMember(dest => dest.FormattedPrice, opt => opt.MapFrom(src => $"Rs. {src.CarPrice / 100000:N1} Lakh"))
            .ForMember(dest => dest.CarName, opt => opt.MapFrom(src => $"{src.MakeYear} {src.CarName}"))
            .ForMember(dest => dest.Fuel, opt => opt.MapFrom(src => src.Fuel.ToString()))  // Convert FuelType enum to string
            .ForMember(dest => dest.IsValueForMoney, opt => opt.MapFrom(src => src.Km < 10000 && src.CarPrice < 200000));
    }
}
