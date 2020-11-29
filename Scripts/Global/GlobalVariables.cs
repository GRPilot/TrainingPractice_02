using Godot;
using System;

public class GlobalVariables : Node {
    public class GameMode {
        public enum Mode {
            Speed,
            Time,
        }

        public static Mode CurrentMode = Mode.Speed;
        public const string Speed = "SpeedMode";
        public const string Time = "TimeMode";

        public static readonly Vector2 SpeedDotsCount = new Vector2(10, 10);
        public static readonly Vector2 TimeDotsCount = new Vector2(20, 20);

        public const string SpeedModeTimeLimit = "01:00";
        public const string TimeModeTimeLimit = "99:99";
        public static Vector2 GetDotsCount() {
            switch(CurrentMode) {
                case Mode.Speed:
                    return SpeedDotsCount;
                case Mode.Time:
                    return TimeDotsCount;
            }
            return TimeDotsCount;
        }

        public static string GetTimeLimit() {
            switch(CurrentMode) {
                case Mode.Speed:
                    return SpeedModeTimeLimit;
                case Mode.Time:
                    return TimeModeTimeLimit;
            }
            return TimeModeTimeLimit;
        }

        public static string GetCurrentModeStr() {
            switch(CurrentMode) {
                case Mode.Speed:
                    return Speed;
                case Mode.Time:
                    return Time;
            }
            return Time;
        }
    }
    public const string GameShortName = "IYHSS";
    public struct Statistics {
        public const string WinPath = GameShortName + "/"; //"%APPDATA%/" + GameShortName + "/";
        public const string MacOSPath = GameShortName + "/"; //"~/Library/Application Support/" + GameShortName + "/";
        public const string LinuxPath = GameShortName + "/"; //"~/.local/share/" + GameShortName + "/";
    }

}
