using Discord.Commands;
using Six.App;

public class GeneralCommandsModule : ModuleBase<SocketCommandContext> {
    [Command("DYEL")]
    [Summary("Responds with the Lifting Day!")]
    public Task DYELReplyAsync(int offset = 0) {
        WorkoutDay todaysWorkout = DYEL.GetWorkout(offset);
        // Example: if(Context.User.Id == 284370066873647105) { DoTheSpecialRobbyStuff(); }
        return ReplyAsync($"Damn Right I Do! Today's focus is {todaysWorkout.Type} Round #{todaysWorkout.Round}");
    }

    [Command("joke")]
    [Summary("Says a Random Joke.")]
    public Task JokeReplyAsync()
    {
        var author = Context.Message.Author.Mention;
        var joke = Joke.GetNewJokeAsync(author).Result;
        return ReplyAsync(joke.Value);  
    }
}
