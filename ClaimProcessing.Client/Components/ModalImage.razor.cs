using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace ClaimProcessing.Client.Components
{
    public partial class ModalImage
    {
        [Parameter]
        public bool Show { get; set; }

        [Parameter]
        public string FilePath { get; set; }

        [Parameter]
        public EventCallback<MouseEventArgs> OnCancel { get; set; }

    }
}
