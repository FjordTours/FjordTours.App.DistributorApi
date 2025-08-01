namespace FjordTours.App.DistributorApi.Infrastructure.Models
{
    public class DistributorConfigStorageModel
    {

        public required string SubscriptionKey { get; set; }

        public required string ClientId { get; set; }

        public required string ClientSecret { get; set; }

        public string? OllamaBaseAddress { get; set; }

        public string? Model { get; set; }

    }
}