using AutoMapper;
using RepositoryArchitecture.DataLayer.Models;
using RepositoryArchitecture.Model.Models;

namespace RepositoryArchitecture.MappingProfile
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserModel, Users>().ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.FirstName}{src.LastName}"));
            CreateMap<Users, UserModel>();
        }
    }
}
