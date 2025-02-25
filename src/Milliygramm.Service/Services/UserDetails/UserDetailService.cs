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
        var existUser = await unitOfWork.Users.SelectAsync(u => u.Id == createModel.UserId)
           ?? throw new NotFoundException($"User not found with ID {createModel.UserId}");

        var mappedUserDetail = mapper.Map<UserDetail>(createModel);

        mappedUserDetail.PictureId = Asset.DefaultPictureId;
        var createdUserDetail = await unitOfWork.UserDetails.InsertAsync(mappedUserDetail);
        await unitOfWork.SaveAsync();

        return mapper.Map<UserDetailViewModel>(createdUserDetail);
    }
}