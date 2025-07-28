using Application.DTOs.Categories;
using Application.Mappings;
using AutoMapper;
using Domain.Entities;

namespace Application.DTOs.Products
{
    public class ProductDto : IMapFrom<Product>
    {
        public long Id { get; set; }
        public string SKU { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public double Rating { get; set; }
        public string? ImageUrl { get; set; }

        //public CategoryDto Category { get; set; }
        

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Product, ProductDto>();
        }
    }
}
