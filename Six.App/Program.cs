using System.Reflection;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Six.App;

public class Program {
    const char COMMAND_PREFIX = '!';
    const ulong TEST_CHANNEL_ID = 908443546930020382;
    const bool TEST_MODE = false;

    DiscordSocketClient client;
    readonly CommandService commands;
    readonly IServiceProvider? services;

    public static TPEData? TPE { get; private set; }

    public static Task Main() => new Program().MainAsync();

    public Program() {
        DiscordSocketConfig socketConfig;
        CommandServiceConfig commandsConfig;

        socketConfig = new() {
            LogLevel = LogSeverity.Info,
            MessageCacheSize = 50,
            GatewayIntents = GatewayIntents.All,
        };

        client = new(socketConfig);
        client.Ready += SetupOnReady;

        commandsConfig = new() {
            LogLevel = LogSeverity.Info,
            CaseSensitiveCommands = false,
        };

        commands = new(commandsConfig);

        client.Log += Log;
        commands.Log += Log;
    }

    static Task Log(LogMessage message) {
        switch(message.Severity) {
            case LogSeverity.Critical:
            case LogSeverity.Error:
                Console.ForegroundColor = ConsoleColor.Red;
                break;
            case LogSeverity.Warning:
                Console.ForegroundColor = ConsoleColor.Yellow;
                break;
            case LogSeverity.Info:
                Console.ForegroundColor = ConsoleColor.White;
                break;
            case LogSeverity.Verbose:
            case LogSeverity.Debug:
                Console.ForegroundColor = ConsoleColor.DarkGray;
                break;
        }
        Console.WriteLine($"{DateTime.Now,-19} [{message.Severity,8}] {message.Source}: {message.Message} {message.Exception}");
        Console.ResetColor();
        return Task.CompletedTask;
    }

    public async Task MainAsync() {
        await InitCommands();
        string? token = Environment.GetEnvironmentVariable("SIX_TOKEN");

        await client.LoginAsync(TokenType.Bot, token);
        await client.StartAsync();
        await Task.Delay(-1);
    }

    Task SetupOnReady() {
        TPE = new(client);
        DYEL.StartLiftingUpdates();
        return Task.CompletedTask;
    }

    async Task InitCommands() {
        await commands.AddModulesAsync(Assembly.GetEntryAssembly(), services);
        //await commands.AddModuleAsync<GeneralCommandsModule>(services);
        client.MessageReceived += HandleCommandAsync;
    }

    async Task HandleCommandAsync(SocketMessage message) {
        var userMessage = message as SocketUserMessage;
        if(userMessage == null) { return; }
        if(AuthorIsMe()) { return; }
        if(AuthorIsABot()) { return; }
        if(TEST_MODE && !InTestChannel()) { return; }

        int pos = 0;

        if(CommandTriggered()) {
            SocketCommandContext context = new(client, userMessage);
            IResult result = await commands.ExecuteAsync(context, pos, services);
            if(!result.IsSuccess) {
                await userMessage.Channel.SendMessageAsync(result.ErrorReason);
            }
        }

        bool CommandTriggered() =>
            userMessage.HasCharPrefix(COMMAND_PREFIX, ref pos)
            || userMessage.HasMentionPrefix(client.CurrentUser, ref pos);

        bool AuthorIsMe() => userMessage.Author.Id == client.CurrentUser.Id;
        bool AuthorIsABot() => userMessage.Author.IsBot;
        bool InTestChannel() => userMessage!.Channel.Id == TEST_CHANNEL_ID;
    }
}
