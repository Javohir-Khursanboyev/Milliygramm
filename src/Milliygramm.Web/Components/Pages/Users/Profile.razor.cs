using System.Net.Http.Headers;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Milliygramm.Domain.Enums;
using Milliygramm.Model.DTOs.Users;
using Milliygramm.Web.Authorization;
using Milliygramm.Web.Services.Users;

namespace Milliygramm.Web.Components.Pages.Users;

public partial class Profile
{
    [Inject]
    private AuthenticationStateProvider AuthStateProvider { get; set; } = default!;

    [Inject]
    private IUserApiService userApiService { get; set; } = default!;
    private UserViewModel? userModel {  get; set; }
    private IBrowserFile? selectedFile;

    protected override async Task OnInitializedAsync()
    {
        userModel = await ((CustomAuthStateProvider)AuthStateProvider).GetCurrentUser();
    }

    private void HandleImageUpload (InputFileChangeEventArgs e)
    {
        selectedFile = e.File;
    }

    private async Task UploadImage()
    {
        if (selectedFile == null || userModel == null) return;
        try
        {
            using var content = new MultipartFormDataContent();

            var fileContent = new StreamContent(selectedFile.OpenReadStream(5 * 1024 * 1024));
            fileContent.Headers.ContentType = new MediaTypeHeaderValue(selectedFile.ContentType);
            content.Add(fileContent, "file", selectedFile.Name);
            content.Add(new StringContent(FileType.Images.ToString()), "fileType");

            userModel = await userApiService.UploadPictureAsync(userModel.Id, content);

            StateHasChanged();
        }
        catch (Exception ex)
        {
        }
    }
}
