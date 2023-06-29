using Discord.Commands;
using Six.App;

public class GeneralCommandsModule : ModuleBase<SocketCommandContext> {
    [Command("DYEL")]
    [Summary("Responds with the Lifting Day!")]
    public Task DYELReplyAsync(int offset = 0) {
        WorkoutDay todaysWorkout = DYEL.GetWorkout(offset);
        return ReplyAsync($"Damn Right I Do! Today's focus is {todaysWorkout.Type} Round #{todaysWorkout.Round}");
    }
}
