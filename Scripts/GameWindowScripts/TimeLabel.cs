using Godot;
using System;

public class TimeLabel : Label {

    private class Time {
        private uint milliseconds = 0;
        private uint seconds = 0;
        private uint minutes = 0;

        public static Time operator+(Time obj, uint seconds) {
            if(seconds == 0) {
                return obj;
            }

            obj.seconds += seconds;
            if(obj.seconds < 60) {
                return obj;
            }

            obj.minutes += obj.seconds / 60;
            obj.seconds %= 60;
            return obj;
        }
        public static Time operator++(Time obj) {
            ++obj.milliseconds;
            if(obj.milliseconds < 100) {
                return obj;
            }
            obj.milliseconds = 0;

            ++obj.seconds;
            if(obj.seconds < 60) {
                return obj;
            }
            obj.seconds = 0;

            ++obj.minutes;
            return obj;
        }
        public override string ToString() {
            return string.Format("{2}:{1}:{0}",
                (milliseconds < 10 ? "0" : "") + milliseconds.ToString(),
                (     seconds < 10 ? "0" : "") + seconds.ToString(),
                (     minutes < 10 ? "0" : "") + minutes.ToString()
            );
        }
        public string Limit() {
            return string.Format("{1}:{0}",
                (     seconds < 10 ? "0" : "") + seconds.ToString(),
                (     minutes < 10 ? "0" : "") + minutes.ToString()
            );
        }
    }

    Time leftTime = new Time();

    public override void _Ready() {
        base._Ready();
        Text = leftTime.ToString();
        DrawAreaContainer drawArea = GetNode<DrawAreaContainer>("../../DrawAreaContainer");

        drawArea.Connect("AllCirclesActivated", this, nameof(OnAllCirclesActivated));
        drawArea.Connect("UserMissClick", this, nameof(OnMissClick));
        GetNode<Timer>("../../Timer")
            .Connect("timeout", this, nameof(OnTimerTick));
    }

    public string GetTime() {
        return leftTime.ToString();
    }

    private void OnTimerTick() {
        Timer timer = GetNode<Timer>("../../Timer");
        ++leftTime;
        Text = (++leftTime).ToString();
        if(leftTime.Limit() == GlobalVariables.GameMode.GetTimeLimit()) {
            timer.Stop();
            EmitSignal(nameof(TimeLeft));
        }
    }
    private void OnAllCirclesActivated(int score) {
        Timer timer = GetNode<Timer>("../../Timer");
        timer.Stop();
    }
    private void OnMissClick() {
        Timer timer = GetNode<Timer>("../../Timer");
        if(timer.IsStopped()) {
            GD.Print("Timer stopped");
            return;
        }
        GD.Print("Miss click");
        leftTime += 1;
    }

    [Signal] public delegate void TimeLeft();

}
