using Blazored.LocalStorage;
using FjordTours.App.DistributorApi.Infrastructure.Constants;
using FjordTours.App.DistributorApi.Infrastructure.Models;
using FjordTours.App.DistributorApi.Infrastructure.Services.Bootstrapping;
using FjordTours.App.DistributorApi.Infrastructure.State;
using FjordTours.DistributorApi.Auth.Config;
using FjordTours.DistributorApi.Proprietary.Contracts;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace FjordTours.App.DistributorApi.Components
{
    public partial class AuthDetails
    {

        [Inject]
        IApiContract ApiClient { get; set; } = default!;

        [Inject]
        IBootstrappingService BootstrappingService { get; set; } = default!;

        [Inject]
        AppState AppState { get; set; } = default!;

        [Inject]
        FjordToursDistributorApiConfig DistributorApiAppConfig { get; set; } = default!;

        [Inject]
        ILocalStorageService LocalStorageService { get; set; } = default!;

        [Inject]
        NavigationManager _navigationManager { get; set; } = default!;

        MudForm form = default!;
        bool success = true;
        bool loading = false;

        protected override async Task OnInitializedAsync()
        {
            var storedConfig = await LocalStorageService.GetItemAsync<DistributorConfigStorageModel>(
                AppConstants.DistributorConfigStorageKey);
            if (storedConfig is not null)
            {
                DistributorApiAppConfig.OcpApimSubscriptionKey = storedConfig.SubscriptionKey;
                DistributorApiAppConfig.DistributorApiClientId = storedConfig.ClientId;
                DistributorApiAppConfig.DistributorApiClientSecret = storedConfig.ClientSecret;
                AppState.OllamaBaseAddress = storedConfig.OllamaBaseAddress;
                AppState.Model = storedConfig.Model;
            }
        }

        async Task Authenticate()
        {
            var missingData = string.IsNullOrEmpty(DistributorApiAppConfig.OcpApimSubscriptionKey) ||
                              string.IsNullOrEmpty(DistributorApiAppConfig.DistributorApiClientId) ||
                              string.IsNullOrEmpty(DistributorApiAppConfig.DistributorApiClientSecret);
            if (!missingData)
            {
                loading = true;
                var distributorProfile = await ApiClient.GetDistributorProfileAsync();
                if (distributorProfile is not null)
                {
                    // Store the config
                    await LocalStorageService.SetItemAsync<DistributorConfigStorageModel>(
                        AppConstants.DistributorConfigStorageKey,
                        new DistributorConfigStorageModel() { 
                            SubscriptionKey = DistributorApiAppConfig.OcpApimSubscriptionKey!,
                            ClientId = DistributorApiAppConfig.DistributorApiClientId!,
                            ClientSecret = DistributorApiAppConfig.DistributorApiClientSecret!,
                            OllamaBaseAddress = AppState.OllamaBaseAddress,
                            Model = AppState.Model
                        });
                    // Bootstrapping
                    AppState.DistributorProfile = distributorProfile.Value;
                    await BootstrappingService.BootstrapAsync();
                    StateHasChanged();
                    _navigationManager.NavigateTo(PagePathConstants.Dashboard);
                }
                loading = false;
                StateHasChanged();
            }
        }

        async Task Clear()
        {
            await LocalStorageService.RemoveItemAsync(AppConstants.DistributorConfigStorageKey);
            DistributorApiAppConfig.OcpApimSubscriptionKey = null;
            DistributorApiAppConfig.DistributorApiClientId = null;
            DistributorApiAppConfig.DistributorApiClientSecret = null;
            StateHasChanged();
        }

    }
}