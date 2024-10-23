using FjordTours.App.DistributorApi.Infrastructure.State;
using FjordTours.DistributorApi.Auth.Config;
using Microsoft.AspNetCore.Components;

namespace FjordTours.App.DistributorApi.Components
{
    public partial class AppComponent
    {

        [Inject]
        public FjordToursDistributorApiConfig DistributorApiAppConfig { get; set; } = default!;

        [Inject]
        internal AppState AppState { get; set; } = default!;

        public bool HasValidConfig()
        {
            var x = AppState.DistributorProfile;
            if (string.IsNullOrEmpty(DistributorApiAppConfig.BaseAddress) ||
                string.IsNullOrEmpty(DistributorApiAppConfig.OcpApimSubscriptionKey) ||
                string.IsNullOrEmpty(DistributorApiAppConfig.DistributorApiClientId) ||
                string.IsNullOrEmpty(DistributorApiAppConfig.DistributorApiClientSecret))
                return false;

            return true;
        }

    }
}