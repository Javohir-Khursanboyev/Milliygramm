using Microsoft.AspNetCore.Components;

namespace Milliygramm.Web.Components.Pages.Modals;

public partial class AppModal
{
    [Parameter]
    public RenderFragment Title { get; set; }
    [Parameter]
    public RenderFragment Body { get; set; }
    [Parameter]
    public RenderFragment Footer { get; set; }
    private bool showModal = false;

    public void Open()
    {
        showModal = true;
    }

    public void Close()
    {
        showModal = false;
    }
}
