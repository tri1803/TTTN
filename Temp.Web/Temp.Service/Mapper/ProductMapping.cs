using AutoMapper;
using Temp.DataAccess.Data;
using Temp.Service.DTO;

namespace Temp.Service.Mapper
{
    public class ProductMapping : Profile
    {
        public ProductMapping()
        {
            CreateMap<Product, ProductDto>();

            CreateMap<CreateProductDto, Product>();
            
            CreateMap<Product, CreateProductDto>();

            CreateMap<Product, ProductViewModel>();
        }
    }
}