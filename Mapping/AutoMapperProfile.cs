
using AutoMapper;
using DotNetCoeEFWebApi.Dtos;

namespace DotNetCoreEFWebApi.Mapping;
public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Product, ProductDto>().ReverseMap();
    }
}