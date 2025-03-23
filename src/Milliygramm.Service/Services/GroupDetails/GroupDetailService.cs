using AutoMapper;
using Milliygramm.Data.UnitOfWork;
using Milliygramm.Domain.Entities;
using Milliygramm.Domain.Enums;
using Milliygramm.Model.DTOs.GroupDetails;
using Milliygramm.Model.DTOs.Users;
using Milliygramm.Service.Exceptions;
using Milliygramm.Service.Extensions;
using Milliygramm.Service.Validators.GroupDetails;

namespace Milliygramm.Service.Services.GroupDetails;

public sealed class GroupDetailService(IMapper mapper,
    IUnitOfWork unitOfWork,
     GroupDetailCreateModelValidator createModelValidator,
    GroupDetailUpdateModelValidator updateModelValidato) : IGroupDetailService
{
    public async Task<GroupDetailVievModel> CreateAsync(GroupDetailCreateModel createModel)
    {
        await createModelValidator.EnsureValidatedAsync(createModel);
        var existPicture = await unitOfWork.Assets.SelectAsync(picture => picture.Id == Asset.DefaultPictureId)
            ?? throw new NotFoundException($"Default Picture not found");

        var mappedGroupDetail = mapper.Map<GroupDetail>(createModel);

        mappedGroupDetail.Privacy = Privacy.Private;
        mappedGroupDetail.Link = Guid.NewGuid().ToString("N");
        mappedGroupDetail.Asset = existPicture;
        var createdGroupDetail = await unitOfWork.GroupDetails.InsertAsync(mappedGroupDetail);
        await unitOfWork.SaveAsync();
        return mapper.Map<GroupDetailVievModel>(createdGroupDetail);
    }

    public async Task<GroupDetailVievModel> UpdateAsync(long id, GroupDetailUpdateModel updateModel)
    {
        await updateModelValidato.EnsureValidatedAsync(updateModel);
        var existGroupdetail = await unitOfWork.GroupDetails.SelectAsync(groupDetail => groupDetail.Id == id && !groupDetail.IsDeleted)
            ?? throw new NotFoundException($"Group detail is not found this ID = {id}");

        existGroupdetail.Description = updateModel.Description;
        existGroupdetail.Link = updateModel.Link;
        existGroupdetail.Privacy = Privacy.Private;
        await unitOfWork.GroupDetails.UpdateAsync(existGroupdetail);
        await unitOfWork.SaveAsync();

        return mapper.Map<GroupDetailVievModel>(existGroupdetail);
    }
}
