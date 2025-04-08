using System.Net.Http.Headers;
using Blazored.Toast.Services;
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

    [Inject]
    private NavigationManager navigationManager { get; set; } = default!;

    [Inject]
    private IToastService toastService { get; set; } = default!;
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
            await ((CustomAuthStateProvider)AuthStateProvider).SetCurrentUser(userModel, true);
            navigationManager.NavigateTo(navigationManager.Uri, forceLoad: true);
        }
        catch (Exception ex)
        {
            toastService.ShowError($"Failed to upload image: {ex.Message}");
        }
    }

    private async Task HandleSave()
    {
        if(userModel == null) return;
        try
        {
            var userUpdateModel = new UserUpdateModel()
            {
                FirstName = userModel.FirstName,
                LastName = userModel.LastName,
                UserName = userModel.UserName,
                UserDeatil = new UserDetailUpdateModel
                {
                    Bio = userModel.UserDetail.Bio,
                    Location = userModel.UserDetail.Location,
                    DataOfBirth = userModel.UserDetail.DataOfBirth
                }
            };
            userModel = await userApiService.UpdateAsync(userModel.Id, userUpdateModel);
            await ((CustomAuthStateProvider)AuthStateProvider).SetCurrentUser(userModel, true);
            StateHasChanged();
            toastService.ShowSuccess("Profile updated successfully!");
        }
        catch (Exception ex)
        {
            toastService.ShowError($"Failed to update profile: {ex.Message}");
        }
    }

    private async Task CancelChanges()
    {
        try
        {
            userModel = await ((CustomAuthStateProvider)AuthStateProvider).GetCurrentUser();
            StateHasChanged();
            toastService.ShowInfo("Changes discarded");
        }
        catch(Exception ex)
        {
            toastService.ShowError($"Failed to cancel changes: {ex.Message}");
        }
    }
}
