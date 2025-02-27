using AutoMapper;
using Milliygramm.Data.UnitOfWork;
using Milliygramm.Service.Helpers;
using Milliygramm.Domain.Entities;
using Milliygramm.Model.DTOs.Assets;
using Milliygramm.Service.Exceptions;
using Milliygramm.Service.Validators.Assets;
using Milliygramm.Service.Extensions;

namespace Milliygramm.Service.Services.Assets;

public sealed class AssetService(IMapper mapper, IUnitOfWork unitOfWork, AssetCreateModelValidator createModelValidator) : IAssetService
{
    public async Task<AssetViewModel> UploadAsync(AssetCreateModel model)
    {
        await createModelValidator.EnsureValidatedAsync(model);

        var assetData = await FileHelper.CreateFileAsync(model);
        var asset = new Asset()
        {
            Name = assetData.Name,
            Path = assetData.Path,
            FileType = model.FileType
        };

        var createdAsset = await unitOfWork.Assets.InsertAsync(asset);
        await unitOfWork.SaveAsync();

        return mapper.Map<AssetViewModel>(asset);
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var existAsset = await unitOfWork.Assets.SelectAsync(asset => asset.Id == id)
            ?? throw new NotFoundException("Asset is not found");

        await unitOfWork.Assets.DropAsync(existAsset);
        return await unitOfWork.SaveAsync();
    }

    public async Task<AssetViewModel> GetByIdAsync(long id)
    {
        var existAsset = await unitOfWork.Assets.SelectAsync(asset => asset.Id == id)
           ?? throw new NotFoundException("Asset is not found");

        return mapper.Map<AssetViewModel>(existAsset);
    }

}