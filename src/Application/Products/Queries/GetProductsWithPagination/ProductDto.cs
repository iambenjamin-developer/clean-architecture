using Application.Mappings;
using AutoMapper;
using Domain.Entities;

namespace Application.Products.Queries.GetProductsWithPagination
{
    public class ProductDto : IMapFrom<Product>
    {
        public long Id { get; set; }
        public string SKU { get; set; }
        public string Name { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Product, ProductDto>();
        }
    }
}
