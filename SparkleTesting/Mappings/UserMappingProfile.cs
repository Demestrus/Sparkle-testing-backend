using AutoMapper;
using SparkleTesting.API.Models.Dto;
using SparkleTesting.Application.Models;

namespace SparkleTesting.API.Mappings
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<UserProfileDto, UserProfile>();
        }
    }
}
