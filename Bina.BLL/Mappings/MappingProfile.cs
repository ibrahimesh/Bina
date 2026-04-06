using AutoMapper;
using Bina.DAL.Models;
using Bina.BLL.DTOs.Property;
using Bina.BLL.DTOs.User;
using System.Linq;

namespace Bina.BLL.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Property mappings
            CreateMap<CreatePropertyDto, Property>();
            CreateMap<UpdatePropertyDto, Property>();

            CreateMap<Property, PropertyListDto>()
                .ForMember(dest => dest.MainImageUrl, opt => opt.MapFrom(src => src.Images.FirstOrDefault(i => i.IsMain) != null ? src.Images.FirstOrDefault(i => i.IsMain).Url : null))
                .ForMember(dest => dest.CityName, opt => opt.MapFrom(src => src.City != null ? src.City.Name : null))
                .ForMember(dest => dest.DistrictName, opt => opt.MapFrom(src => src.District != null ? src.District.Name : null))
                .ForMember(dest => dest.IsFavorite, opt => opt.Ignore()); // dynamically evaluated in service

            CreateMap<Property, PropertyResponseDto>()
                .IncludeBase<Property, PropertyListDto>()
                .ForMember(dest => dest.MetroName, opt => opt.MapFrom(src => src.Metro != null ? src.Metro.Name : null))
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category != null ? src.Category.Name : null))
                .ForMember(dest => dest.ImageUrls, opt => opt.MapFrom(src => src.Images.Select(i => i.Url).ToList()));

            // User mappings
            CreateMap<User, UserProfileDto>()
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => (int)src.Role));
                
            CreateMap<UpdateProfileDto, User>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null)); 

            // Category mappings
            CreateMap<Bina.DAL.Models.Category, Bina.BLL.DTOs.Category.CategoryDto>()
                .ForMember(dest => dest.Children, opt => opt.MapFrom(src => src.Children));

            // Message mappings
            CreateMap<Bina.DAL.Models.Message, Bina.BLL.DTOs.Message.MessageDto>()
                .ForMember(dest => dest.SenderName, opt => opt.MapFrom(src => src.Sender != null ? src.Sender.FullName : null))
                .ForMember(dest => dest.SenderAvatar, opt => opt.MapFrom(src => src.Sender != null ? src.Sender.AvatarUrl : null))
                .ForMember(dest => dest.ReceiverName, opt => opt.MapFrom(src => src.Receiver != null ? src.Receiver.FullName : null));
        }
    }
}