using Microsoft.AspNetCore.Components;

namespace Milliygramm.Web.Components.Pages.Modals;

public partial class AppModal
{
    [Parameter]
    public RenderFragment Title { get; set; } = default!;
    [Parameter]
    public RenderFragment Body { get; set; } = default!;
    [Parameter]
    public RenderFragment Footer { get; set; } = default!;
    private bool showModal = false;

    [Parameter]
    public string Style { get; set; } = "";

    public void Open()
    {
        showModal = true;
    }

    public void Close()
    {
        showModal = false;
    }
}
