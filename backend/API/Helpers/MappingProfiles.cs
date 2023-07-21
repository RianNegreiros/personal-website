using AutoMapper;
using backend.API.DTOs;
using backend.Core.Models;

namespace backend.API.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<RegisterDto, User>();
        }
    }
}

