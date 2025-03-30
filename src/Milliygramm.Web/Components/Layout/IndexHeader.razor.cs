using Milliygramm.Model.DTOs.Users;
using Microsoft.AspNetCore.Components;
using Milliygramm.Web.Components.Pages.Modals;

namespace Milliygramm.Web.Components.Layout;

public partial class IndexHeader
{
    [Parameter]
    public UserViewModel? user { get; set; }
    public AppModal? Modal { get; set; }
}
