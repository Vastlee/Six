using System.Text.Json;
using System.Timers;

using Timer = System.Timers.Timer;

namespace Six.App;

public class DYEL {
    public enum WorkoutType { None, Push, Pull, Leg, Hybrid, Rest }

    const string CONFIG_FILE_NAME = "DYELConfig.json";

    static Timer newLiftingDayTimer = new(69420) {
        AutoReset = true,
        Enabled = true
    };

    static DYELConfig config = new();

    public static DateTime StartDate => new(2023, 05, 27);

    public static event Action<DYELConfig>? OnDYELChange;

    public static WorkoutDay GetWorkout(int offset = 0) {
        TimeSpan startDateDelta = DateTime.Now - StartDate;

        var workout = (startDateDelta.Days + offset) % 6;
        var day = workout % 3;
        int round = (workout > 2) ? 1 : 2;

        return new WorkoutDay(day switch {
            0 => WorkoutType.Push,
            1 => WorkoutType.Pull,
            2 => WorkoutType.Hybrid,
            _ => WorkoutType.Rest,
        }, round);
    }

    public static async Task StartLiftingUpdates() {
        await ReadinConfigAsync();
        newLiftingDayTimer!.Elapsed += CheckForNewLiftingDay;
    }

    static void CheckForNewLiftingDay(object? sender, ElapsedEventArgs e) {
        if(DateTime.Now.Day != config.LastAnnounced.Day) {
            Task.Run(() => UpdateChannel());
        }
    }

    static async Task UpdateChannel() {
        config = new DYELConfig() { LastAnnounced = DateTime.Now };
        await SaveConfig();
        WorkoutDay todaysWorkout = GetWorkout();
        Program.TPE?.DYELChannel?.ModifyAsync(x => x.Topic = $"Workout: {todaysWorkout.Type} :muscle: Round: {todaysWorkout.Round}");
        Program.TPE?.DYELChannel?.SendMessageAsync($"It's A New Day! Today we're doing {todaysWorkout.Type} #{todaysWorkout.Round}");
    }

    static async Task ReadinConfigAsync() {
        using FileStream createStream = File.Create(CONFIG_FILE_NAME);
        await JsonSerializer.SerializeAsync(createStream, config);
        await createStream.DisposeAsync();
    }

    static async Task SaveConfig() {
        string saveJson = JsonSerializer.Serialize(config);
        await File.WriteAllTextAsync(CONFIG_FILE_NAME, saveJson);
    }
}

