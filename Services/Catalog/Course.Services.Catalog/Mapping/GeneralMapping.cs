using AutoMapper;
using Course.Services.Catalog.Dtos;
using Course.Services.Catalog.Models;

namespace Course.Services.Catalog.Mapping
{
    public class GeneralMapping : Profile
    {
        public GeneralMapping()
        {
            CreateMap<Courses, CourseDto>().ReverseMap();
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Feature, FeatureDto>().ReverseMap();

            CreateMap<Courses, CourseCreateDto>().ReverseMap();
            CreateMap<Courses, CourseUpdateDto>().ReverseMap();
        }
    }
}
