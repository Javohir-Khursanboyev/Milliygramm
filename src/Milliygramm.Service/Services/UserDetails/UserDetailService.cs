using AutoMapper;
using Milliygramm.Data.UnitOfWork;
using Milliygramm.Domain.Entities;
using Milliygramm.Model.DTOs.Users;
using Milliygramm.Service.Exceptions;

namespace Milliygramm.Service.Services.UserDetails;

public sealed class UserDetailService(IMapper mapper, IUnitOfWork unitOfWork) : IUserDetailService
{
    public async Task<UserDetailViewModel> CreateAsync(UserDetailCreateModel createModel)
    {
        var existPicture = await unitOfWork.Assets.SelectAsync(a => a.Id == Asset.DefaultPictureId)
            ?? throw new NotFoundException($"Default Picture not found");

        var mappedUserDetail = mapper.Map<UserDetail>(createModel);

        mappedUserDetail.Picture = existPicture;
        var createdUserDetail = await unitOfWork.UserDetails.InsertAsync(mappedUserDetail);
        await unitOfWork.SaveAsync();

        return mapper.Map<UserDetailViewModel>(createdUserDetail);
    }
}