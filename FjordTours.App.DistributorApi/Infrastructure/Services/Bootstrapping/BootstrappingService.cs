using FjordTours.App.DistributorApi.Infrastructure.State;
using FjordTours.DistributorApi.Proprietary.Contracts;

namespace FjordTours.App.DistributorApi.Infrastructure.Services.Bootstrapping
{
    public class BootstrappingService : IBootstrappingService
    {

        private readonly IApiContract _apiClient;
        private readonly AppState _appState;

        public BootstrappingService(
            IApiContract apiClient,
            AppState appState)
        {
            _apiClient = apiClient;
            _appState = appState;
        }

        public async Task BootstrapAsync()
        {
            // Distributor Data
            var getApiDetailsTask = Task.Run(async () => { 
                var result = await _apiClient.PingAsync();
                _appState.PingApiDetails = result;
            });

            // Master Data
            var getCountriesTask = Task.Run(async () => {
                var result = await _apiClient.GetCountriesMasterDataAsync();
                _appState.Countries = result?.Value;
            });
            var getCurrenciesTask = Task.Run(async () => {
                var result = await _apiClient.GetCurrenciesMasterDataAsync();
                _appState.Currencies = result?.Value;
            });
            var getLocationsTask = Task.Run(async () => {
                var result = await _apiClient.GetLocationsMasterDataAsync();
                _appState.Locations = result?.Value;
            });
            var getSegmentsTask = Task.Run(async () => {
                var result = await _apiClient.GetSegmentsMasterDataAsync();
                _appState.Segments = result?.Value;
            });
            var getTicketTypesTask = Task.Run(async () => {
                var result = await _apiClient.GetTicketTypesMasterDataAsync();
                _appState.TicketTypes = result?.Value;
            });
            var getTransportUnitTypesTask = Task.Run(async () => {
                var result = await _apiClient.GetTransportUnitTypesMasterDataAsync();
                _appState.TransportUnitTypes = result?.Value;
            });
            var getTravelClassesTask = Task.Run(async () => {
                var result = await _apiClient.GetTravelClassesMasterDataAsync();
                _appState.TravelClasses = result?.Value;
            });

            // Products
            var getAccommodationProductsTask = Task.Run(async () => {
                try
                {
                    var result = await _apiClient.GetAccommodationProductsAsync();
                    _appState.AccommodationProducts = result?.Value;
                }
                catch (Exception) { 
                    // just continue
                }
            });
            var getActivityProductsTask = Task.Run(async () => {
                try
                {
                    var result = await _apiClient.GetActivityProductsAsync();
                    _appState.ActivityProducts = result?.Value;
                }
                catch (Exception)
                {
                    // just continue
                }
            });
            var getBundleProductsTask = Task.Run(async () => {
                try
                {
                    var result = await _apiClient.GetBundleProductsAsync();
                    _appState.BundleProducts = result?.Value;
                }
                catch (Exception)
                {
                    // just continue
                }
            });
            var getTransportProductsTask = Task.Run(async () => {
                try
                {
                    var result = await _apiClient.GetTransportProductsAsync();
                    _appState.TransportProducts = result?.Value;
                }
                catch (Exception)
                {
                    // just continue
                }
            });

            // Do the hard work
            await Task.WhenAll(
                // Distributor Data
                getApiDetailsTask,
                // Master Data
                getCountriesTask,
                getCurrenciesTask,
                getLocationsTask,
                getSegmentsTask,
                getTicketTypesTask,
                getTransportUnitTypesTask,
                getTravelClassesTask,
                // Products
                getAccommodationProductsTask,
                getActivityProductsTask,
                getBundleProductsTask,
                getTransportProductsTask);
        }

    }
}