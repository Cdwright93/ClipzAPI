using AutoMapper;
using ClipzAPI.DataTransferObjects;
using ClipzAPI.Models;

namespace ClipzAPI.Managers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserForRegistrationDto, AspNetUsers>();
        }
    }
}
