using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace NotificationService.Hubs;

[Authorize]
public class NotificationHub : Hub
{
    private readonly Dictionary<Guid, string> usersConnections = [];

    public async override Task OnConnectedAsync()
    {
        bool valid = Guid.TryParse(Context.UserIdentifier, out Guid userId);
        if (valid)
            usersConnections[userId] = Context.ConnectionId;

        await base.OnConnectedAsync();
    }

    public async override Task OnDisconnectedAsync(Exception? exception)
    {

        bool valid = Guid.TryParse(Context.UserIdentifier, out Guid userId);
        if (valid)
            usersConnections.Remove(userId);

        await base.OnDisconnectedAsync(exception);
    }
}
