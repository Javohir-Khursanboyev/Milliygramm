using AutoMapper;
using Milliygramm.Domain.Entities;
using Milliygramm.Model.DTOs.Assets;
using Milliygramm.Model.DTOs.Users;

namespace Milliygramm.Service.Mappers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Asset, AssetViewModel>().ReverseMap();

        CreateMap<UserCreateModel, User>().ReverseMap();
        CreateMap<UserUpdateModel, User>().ReverseMap();
        CreateMap<User, UserViewModel>().ReverseMap();
    }
}