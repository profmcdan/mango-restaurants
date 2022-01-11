using AutoMapper;
using Mango.Services.ProductApi.Dtos;
using Mango.Services.ProductApi.Models;

namespace Mango.Services.ProductApi.Config;

public class MappingConfig : Profile
{
    public static MapperConfiguration RegisterMaps()
    {
        var mappingConfig = new MapperConfiguration(config =>
        {
            config.CreateMap<ProductDto, Product>().ReverseMap();
            config.CreateMap<CreateProductDto, Product>();
        });

        return mappingConfig;
    }
}