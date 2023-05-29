using System.Timers;
using Discord;

namespace Six.App;

public class DYEL {
    public enum WorkoutType { None, Push, Pull, Leg, Hybrid, Rest }

    static System.Timers.Timer? newLiftingDayTimer = new(MillisecondsUntilMidnight);

    static double MillisecondsUntilMidnight => DateTime.Today.AddDays(1).Subtract(DateTime.Now).TotalMilliseconds;

    public static DateTime StartDate => new(2023, 05, 27);


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

    public static void StartLiftingUpdates() {
        newLiftingDayTimer!.Elapsed += NewLiftingDay;
        newLiftingDayTimer.Start();
        UpdateChannel();
    }

    static void NewLiftingDay(object? sender, ElapsedEventArgs e) {
        UpdateChannel();

        newLiftingDayTimer!.Interval = MillisecondsUntilMidnight;
        newLiftingDayTimer.Start();
    }

    static void UpdateChannel() {
        WorkoutDay todaysWorkout = GetWorkout();
        Program.TPE?.DYELChannel?.ModifyAsync(x => x.Topic = $"Workout: {todaysWorkout.Type} :muscle: Round: {todaysWorkout.Round}");
        Program.TPE?.DYELChannel?.SendMessageAsync($"It's A New Day! Today we're doing {todaysWorkout.Type} #{todaysWorkout.Round}");
    }
}

