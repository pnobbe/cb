using AutoMapper;
using Insurance.Api.DataTransferObjects;
using Insurance.Core.Entities;

namespace Insurance.Api
{
    // TODO: Document
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<InsuredOrderEntry, InsuredOrderEntryDto>().ReverseMap();
            CreateMap<InsuredOrder, InsuredOrderDto>()
                .ConstructUsing(s => new InsuredOrderDto(s.InsuranceValue));
            CreateMap<InsuredProduct, InsuredProductDto>();
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<ProductType, ProductTypeDto>().ReverseMap(); ;
            CreateMap<OrderEntry, OrderEntryDto>().ReverseMap();
        }
    }
}
