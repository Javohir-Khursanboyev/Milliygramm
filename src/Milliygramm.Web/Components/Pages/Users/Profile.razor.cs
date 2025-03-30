using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using Milliygramm.Model.DTOs.Users;
using Milliygramm.Web.Authorization;
using Milliygramm.Web.Services.Users;
using Microsoft.AspNetCore.Components.Forms;
using Milliygramm.Model.DTOs.Assets;

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
        try
        {
            var asset = new AssetCreateModel()
            {
                FileType = Domain.Enums.FileType.Images
            };

            await userApiService.UploadPictureAsync(Convert.ToInt64(userModel?.Id), asset);
        }
        catch (Exception ex)
        {
        }
    }
}
