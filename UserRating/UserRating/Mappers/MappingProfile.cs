using AutoMapper;
using UserRating.Models;
using UserRating.ViewModels;

namespace UserRating.Infrastructure.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ProfileViewModel, User>().ReverseMap();
        }
    }
}
