using FluentValidation;
using Milliygramm.Model.DTOs.Assets;

namespace Milliygramm.Service.Validators.Assets;

public sealed class AssetCreateModelValidator : AbstractValidator<AssetCreateModel>
{
    private static readonly string[] AllowedFileExtensions = { ".jpg", ".jpeg", ".png", ".gif", ".mp4", ".avi", ".mov" };
    private const long MaxFileSize = 10 * 1024 * 1024; // 10 MB

    public AssetCreateModelValidator()
    {
        RuleFor(asset => asset.File)
            .NotNull().WithMessage("File is required.")
            .Must(file => file.Length > 0).WithMessage("File cannot be empty.")
            .Must(file => file.Length <= MaxFileSize).WithMessage("File size must not exceed 10 MB.")
            .Must(file => AllowedFileExtensions.Contains(Path.GetExtension(file.FileName).ToLower()))
            .WithMessage($"Only {string.Join(", ", AllowedFileExtensions)} file types are allowed.");

        RuleFor(asset => asset.FileType)
            .IsInEnum().WithMessage("Invalid file type.");
    }
}