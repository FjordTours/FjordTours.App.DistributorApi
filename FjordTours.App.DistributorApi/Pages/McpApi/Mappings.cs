namespace FjordTours.App.DistributorApi.Pages.McpApi
{
    internal static class Mappings
    {

        internal static Microsoft.Extensions.AI.ChatRole ToRole(
            this ModelContextProtocol.Protocol.Role role)
            => role switch {
                ModelContextProtocol.Protocol.Role.User => Microsoft.Extensions.AI.ChatRole.User,
                ModelContextProtocol.Protocol.Role.Assistant => Microsoft.Extensions.AI.ChatRole.Assistant,
                _ => Microsoft.Extensions.AI.ChatRole.System
            };

    }
}