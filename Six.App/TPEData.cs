using Discord;
using Discord.WebSocket;

namespace Six.App;

public class TPEData {    
    const ulong GUILD_ID = 839645961147121684;
    const ulong DYEL_CHANNEL_ID = 842617395440517161;
    //const ulong DYEL_CHANNEL_ID = 908443546930020382; Test Channel

    public DiscordSocketClient Client { get; init; }
    public SocketGuild Server { get; init; }
    public ITextChannel? DYELChannel { get; init; }

    public TPEData(DiscordSocketClient client) {
        Client = client;
        Server = client.GetGuild(839645961147121684);
        DYELChannel = Server.GetChannel(DYEL_CHANNEL_ID) as ITextChannel;
    }
}
