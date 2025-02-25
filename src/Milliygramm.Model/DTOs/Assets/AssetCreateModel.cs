using Microsoft.AspNetCore.Http;
using Milliygramm.Domain.Enums;

namespace Milliygramm.Model.DTOs.Assets;

public sealed class AssetCreateModel
{
    public IFormFile File { get; set; }
    public FileType FileType { get; set; }
}