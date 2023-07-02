using Discord;
using Discord.WebSocket;

namespace Six.App;

public class TPEData {
    static readonly ulong SERVER_ID = ulong.Parse(Environment.GetEnvironmentVariable("SIX_SERVER_ID") ?? throw new InvalidOperationException("SIX_SERVER_ID is not set"));
    static readonly ulong CHANNEL_ID = ulong.Parse(Environment.GetEnvironmentVariable("SIX_CHANNEL_ID") ?? throw new InvalidOperationException("SIX_CHANNEL_ID is not set"));

    public DiscordSocketClient Client { get; init; }
    public SocketGuild Server { get; init; }
    public ITextChannel? DYELChannel { get; init; }

    public TPEData(DiscordSocketClient client) {
        Client = client;
        Server = client.GetGuild(SERVER_ID);
        DYELChannel = Server.GetChannel(CHANNEL_ID) as ITextChannel;
    }
}
