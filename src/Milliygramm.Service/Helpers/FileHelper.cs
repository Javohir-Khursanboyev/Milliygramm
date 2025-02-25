using System.Net.Http;
using Milliygramm.Model.DTOs.Assets;

namespace Milliygramm.Service.Helpers;

public static class FileHelper
{
    public static async Task<(string Path, string Name)> CreateFileAsync(AssetCreateModel asset)
    {
        var directoryPath = Path.Combine(EnvironmentHelper.WebRootPath, "Assets", asset.FileType.ToString());
        if (!Directory.Exists(directoryPath))
            Directory.CreateDirectory(directoryPath);

        var fileName = MakeFileName(asset.File.FileName);
        var fullPath = Path.Combine(directoryPath, fileName);

        var stream = File.Create(fullPath);
        await asset.File.CopyToAsync(stream);
        stream.Close();

        return ($"/{asset.FileType}/{fileName}", fileName);
    }

    private static string MakeFileName(string fileName)
    {
        string fileExtension = Path.GetExtension(fileName);
        string guid = Guid.NewGuid().ToString();
        return $"{guid}{fileExtension}";
    }
}