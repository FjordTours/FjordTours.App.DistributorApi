namespace FjordTours.App.DistributorApi.Infrastructure.Extensions
{
    public static class FilterExtensions
    {

        public static IEnumerable<T> FuzzySearch<T>(
            this IEnumerable<T>? candidates,
            Func<T, string?> propertySelector,
            string? criteria)
        {
            return (candidates ?? new List<T>())
                .Where(x => x is not null)
                .Where(candidate => criteria.FuzzyMatches(propertySelector(candidate)))
                .OrderBy(propertySelector);
        }

        public static Task<IEnumerable<T>> FuzzySearchAsync<T>(
            this IEnumerable<T>? candidates,
            Func<T, string?> propertySelector,
            string? criteria)
        {
            return Task.FromResult(candidates.FuzzySearch(propertySelector, criteria));
        }

        public static bool FuzzyMatches(this string? criteria, string? text)
        {
            if (string.IsNullOrWhiteSpace(criteria))
            {
                return true;
            }

            if (string.IsNullOrWhiteSpace(text))
            {
                return false;
            }
            return text.WithoutWhitespace()
                       .Contains(criteria.WithoutWhitespace(), StringComparison.InvariantCultureIgnoreCase);
        }

        public static string WithoutWhitespace(this string value)
        {
            return new string(
                value.ToCharArray()
                    .Where(c => !char.IsWhiteSpace(c))
                    .ToArray()
            );
        }

    }
}