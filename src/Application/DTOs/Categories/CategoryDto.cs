using AutoMapper;
using Domain.Entities;

namespace Application.DTOs.Categories
{
    public class CategoryDto
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Category, CategoryDto>().ReverseMap();
        }
    }
}
