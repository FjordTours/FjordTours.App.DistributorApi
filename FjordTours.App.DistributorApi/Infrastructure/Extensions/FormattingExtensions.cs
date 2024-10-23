using FjordTours.DistributorApi.Common.Common;

namespace FjordTours.App.DistributorApi.Infrastructure.Extensions
{
    public static class FormattingExtensions
    {

        public static string ToHyphenIfNullOrEmpty(
            this string? value)
            => string.IsNullOrEmpty(value)
                ? "-"
                : value;

        public static string ToDateTimeString(
            this DateTimeOffset value)
            => value.ToString("dd MMM yyyy HH:mm z");

        public static string ToDateString(
            this DateTimeOffset value)
            => value.ToString("dd MMM yyyy");

        public static string ToDateString(
            this DateOnly value)
            => value.ToString("dd MMM yyyy");

        public static string ToApiDateString(
            this DateTimeOffset value)
            => value.ToString("yyyy-MM-dd");

        public static string ToApiDateString(
            this DateOnly value)
            => value.ToString("yyyy-MM-dd");

        public static string ToTimeString(
            this DateTimeOffset value)
            => value.ToString("HH:mm");

        public static string ToTimeString(
            this TimeOnly? value)
            => value is null ? "-" : value.Value.ToString("HH:mm");

        public static string ToUrlDateString(
            this DateTimeOffset value)
            => value.ToString("yyy-MM-ddTHH:mm:ss.fffZ");

        public static string ToUrlDateString(
            this DateOnly value)
            => value.ToString("yyy-MM-dd");

        public static string ToMoneyString(
            this CurrencyAmountDto value)
            => $"{value.CurrencyCode} {value.Amount.ToString("0.00")}";

        public static string AsOctoUnit(
            this Guid ticketTypeId)
            => $"unit_{ticketTypeId}";

        public static string ToYesNo(
            this bool value) 
            => value ? "YES" : "NO" ;

    }
}