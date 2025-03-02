using AutoMapper;
using E_Commerce.Entities.Models;
using E_Commerce.Web.ViewModels.Products;

namespace E_Commerce.Web.Settings.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AddProductViewModel, Product>().ForMember(dest => dest.Image, option => option.Ignore());

            CreateMap<Product, AddProductViewModel>().ForMember(dest => dest.Categories, option => option.Ignore()).
                    ForMember(dest => dest.Image, option => option.Ignore());

            CreateMap<Product, EditProductViewModel>()
            .ForMember(dest => dest.oldImageName, opt => opt.MapFrom(src => src.Image)) // Ensure oldImageName gets the current image
            .ForMember(dest => dest.Image, opt => opt.Ignore()) // Ignore file upload field
            .ReverseMap()
            .ForMember(dest => dest.Image, opt => opt.Condition((src, dest, srcMember) => srcMember != null)); // Prevent overwriting with null

            //CreateMap<Product, EditProductViewModel>()
            //    .AfterMap((src, dest) =>
            //    {
            //        dest.oldImageName = src.Image;
            //    })
            //    .ForMember(dest => dest.Image, option => option.Ignore()).ReverseMap();
        }
    }
}
