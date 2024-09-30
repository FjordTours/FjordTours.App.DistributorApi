namespace FjordTours.App.DistributorApi.Infrastructure.Extensions
{
    internal static class StringExtensions
    {

        internal static string HyphenIfNullOrEmpty(
            this string? value)
            => value is null ? "-" : value;

    }
}