using System.Timers;
using Discord;

namespace Six.App;

public class DYEL {
    public enum WorkoutType { None, Push, Pull, Leg, Hybrid, Rest }

    static System.Timers.Timer? newLiftingDayTimer = new(MillisecondsUntilMidnight);

    static double MillisecondsUntilMidnight => DateTime.Today.AddDays(1).Subtract(DateTime.Now).TotalMilliseconds;

    public static DateTime StartDate => new(2023, 05, 27);

    public static WorkoutDay GetWorkout(int offset = 0, bool isRobby = false) {
        TimeSpan startDateDelta = DateTime.Now - StartDate;

        int cycleDays = isRobby ? 4 : 3;
        var workout = (startDateDelta.Days + offset) % (cycleDays * 2);
        var day = workout % cycleDays;
        int round = (workout >= cycleDays) ? 1 : 2;

        return new WorkoutDay(day switch {
            0 => WorkoutType.Push,
            1 => WorkoutType.Pull,
            2 => isRobby ? WorkoutType.Leg : WorkoutType.Hybrid,
            3 => WorkoutType.Rest,
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
        WorkoutDay robbysTodayWorkout = GetWorkout(0, true);
        Program.TPE?.DYELChannel?.ModifyAsync(x => x.Topic = $"Workout: {todaysWorkout.Type} :muscle: Round: {todaysWorkout.Round}");
        Program.TPE?.DYELChannel?.SendMessageAsync($"It's A New Day! Today we're doing {todaysWorkout.Type} #{todaysWorkout.Round}. Robby you're doing {todaysWorkout.Type}");
    }
}

