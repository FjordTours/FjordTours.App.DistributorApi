using Blazored.LocalStorage;
using FjordTours.App.DistributorApi.Infrastructure.Services.Bootstrapping;
using FjordTours.App.DistributorApi.Infrastructure.State;
using FjordTours.Channel.Distributor2.Client.DistributorApi.Auth;
using FjordTours.Channel.Distributor2.Client.DistributorApi.Auth.Config;
using FjordTours.Channel.Distributor2.Client.DistributorApi.Auth.Contracts;
using FjordTours.Channel.Distributor2.Client.DistributorApi.Auth.Infrastructure;
using FjordTours.Channel.Distributor2.Client.DistributorApi.Auth.Models;
using FjordTours.Channel.Distributor2.Client.DistributorApi.Octo;
using FjordTours.Channel.Distributor2.Client.DistributorApi.Octo.Contracts;
using FjordTours.Channel.Distributor2.Client.DistributorApi.Osdm;
using FjordTours.Channel.Distributor2.Client.DistributorApi.Osdm.Contracts;
using FjordTours.Channel.Distributor2.Client.DistributorApi.Proprietary;
using FjordTours.Channel.Distributor2.Client.DistributorApi.Proprietary.Contracts;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

namespace FjordTours.App.DistributorApi
{
    public class Program
    {

        public static async Task Main(string[] args)
        {

            // BASICS
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            var apiBaseAddress = builder.Configuration["FjordToursDistributorApiConfig:BaseAddress"]!;

            // BLAZOR
            builder.Services.AddSingleton<AppState>();
            builder.Services.AddMudServices();
            builder.Services.AddBlazoredLocalStorage();

            // AUTH
            var apiConfig = new FjordToursDistributorApiConfig() { 
                BaseAddress = apiBaseAddress
            };
            builder.Services.AddSingleton(apiConfig);
            builder.Services.AddSingleton<DistributorApiToken>();
            builder.Services.AddTransient<OcpmDelegatingHandler>();
            builder.Services.AddTransient<FjordToursApimTokenDelegatingHandler>();

            builder.Services
                .AddHttpClient<IAuthContract, AuthClient>(client => {
                    client.BaseAddress = new Uri(apiBaseAddress);
                })
                .AddHttpMessageHandler<OcpmDelegatingHandler>();

            // PROPRIETARY API
            builder.Services
                .AddHttpClient<IApiContract, ApiClient>(client => {
                    client.BaseAddress = new Uri(apiBaseAddress);
                })
                .AddHttpMessageHandler<OcpmDelegatingHandler>()
                .AddHttpMessageHandler<FjordToursApimTokenDelegatingHandler>();

            // OCTO API
            builder.Services
                .AddHttpClient<IOctoContract, OctoClient>(client => {
                    client.BaseAddress = new Uri(apiBaseAddress);
                })
                .AddHttpMessageHandler<OcpmDelegatingHandler>()
                .AddHttpMessageHandler<FjordToursApimTokenDelegatingHandler>();

            // OSDM API
            builder.Services
                .AddHttpClient<IOsdmContract, OsdmClient>(client => {
                    client.BaseAddress = new Uri(apiBaseAddress);
                })
                .AddHttpMessageHandler<OcpmDelegatingHandler>()
                .AddHttpMessageHandler<FjordToursApimTokenDelegatingHandler>();

            // BOOTSTRAPPING
            builder.Services.AddTransient<IBootstrappingService, BootstrappingService>();

            // LET'S GO.
            await builder.Build().RunAsync();
        }

    }
}