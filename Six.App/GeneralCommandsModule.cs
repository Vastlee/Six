using Discord.Commands;

public class GeneralCommandsModule : ModuleBase<SocketCommandContext> {
    [Command("Test")]
    [Summary("Testing")]
    public Task TestReplyAsync() => ReplyAsync("Passed");
}
