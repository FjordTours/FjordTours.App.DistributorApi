using FjordTours.App.DistributorApi.Infrastructure.Constants;
using FjordTours.App.DistributorApi.Infrastructure.Services.Bootstrapping;
using Microsoft.AspNetCore.Components;

namespace FjordTours.App.DistributorApi.Pages
{
    public partial class HomePage
    {

        [Inject]
        IBootstrappingService BootstrappingService { get; set; } = default!;

        [Inject]
        NavigationManager NavigationManager { get; set; } = default!;
        
        protected override async Task OnInitializedAsync()
        {
            if (HasValidConfig())
            {
                await BootstrappingService.BootstrapAsync();
                NavigationManager.NavigateTo(PagePathConstants.Dashboard);
            }
        }

    }
}