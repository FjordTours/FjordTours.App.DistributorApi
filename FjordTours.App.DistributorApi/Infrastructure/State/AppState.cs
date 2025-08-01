using FjordTours.DistributorApi.Common.Management.Profile;
using FjordTours.DistributorApi.Common.MasterData;
using FjordTours.DistributorApi.Common.Product;
using FjordTours.DistributorApi.Common.Utility;

namespace FjordTours.App.DistributorApi.Infrastructure.State
{
    public class AppState
    {

        #region Parameters

        public string? OllamaBaseAddress { get; set; } = null;

        public string? Model { get; set; } = null;

        #endregion


        #region Management

        internal DistributorProfileDto? DistributorProfile { get; set; } = null;

        #endregion


        #region Configuration

        internal PingDto? PingApiDetails { get; set; } = null;

        #endregion


        #region Master Data

        internal IReadOnlyCollection<CountryDto>? Countries { get; set; } = null;

        internal IReadOnlyCollection<CurrencyDto>? Currencies { get; set; } = null;

        internal IReadOnlyCollection<LocationDto>? Locations { get; set; } = null;

        internal IReadOnlyCollection<SegmentDto>? Segments { get; set; } = null;

        internal IReadOnlyCollection<TicketTypeDto>? TicketTypes { get; set; } = null;

        internal IReadOnlyCollection<TransportUnitTypeDto>? TransportUnitTypes { get; set; } = null;

        internal IReadOnlyCollection<TravelClassDto>? TravelClasses { get; set; } = null;

        #endregion


        #region Products

        public IReadOnlyCollection<ProductDtoBase> Products =>
            new List<ProductDtoBase>()
                .Concat(AccommodationProducts?.ToList() ?? new List<AccommodationProductDto>()).ToList()
                .Concat(ActivityProducts?.ToList() ?? new List<ActivityProductDto>()).ToList()
                .Concat(BundleProducts?.ToList() ?? new List<BundleProductDto>()).ToList()
                .Concat(TransportProducts?.ToList() ?? new List<TransportProductDto>()).ToList();

        public IReadOnlyCollection<AccommodationProductDto>? AccommodationProducts { get; set; } = null;

        public IReadOnlyCollection<ActivityProductDto>? ActivityProducts { get; set; } = null;

        public IReadOnlyCollection<BundleProductDto>? BundleProducts { get; set; } = null;

        public IReadOnlyCollection<TransportProductDto>? TransportProducts { get; set; } = null;

        #endregion

    }
}