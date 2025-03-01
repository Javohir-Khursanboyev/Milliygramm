using AutoMapper;
using Milliygramm.Domain.Entities;
using Milliygramm.Model.DTOs.Assets;
using Milliygramm.Model.DTOs.Chats;
using Milliygramm.Model.DTOs.Groups;
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

        CreateMap<UserDetailCreateModel, UserDetail>().ReverseMap();
        CreateMap<UserDetailUpdateModel, UserDetail>().ReverseMap();
        CreateMap<UserDetail, UserDetailViewModel>().ReverseMap();

        CreateMap<ChatCreateModel, Chat>().ReverseMap();
        CreateMap<Chat, ChatVievModel>().ReverseMap();

        CreateMap<Group, GroupCreatModel>().ReverseMap();
        CreateMap<Group, GroupViewModel>().ReverseMap();
        CreateMap<Group, GroupUpdateModel>().ReverseMap();
    }
}