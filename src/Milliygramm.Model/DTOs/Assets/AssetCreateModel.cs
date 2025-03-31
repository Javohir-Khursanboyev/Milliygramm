using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Milliygramm.Domain.Enums;

namespace Milliygramm.Model.DTOs.Assets;

public sealed class AssetCreateModel
{

    [JsonIgnore] // Bu property APIga yuborilmaydi
    public IBrowserFile BrowserFile { get; set; }
    public IFormFile File { get; set; }
    public FileType FileType { get; set; }
}