using Godot;
using System;

public class GlobalVariables : Node {
    public const string GameShortName = "IYHSS";
    public struct Statistics {
        public const string WinPath = GameShortName + "/"; //"%APPDATA%/" + GameShortName + "/";
        public const string MacOSPath = GameShortName + "/"; //"~/Library/Application Support/" + GameShortName + "/";
        public const string LinuxPath = GameShortName + "/"; //"~/.local/share/" + GameShortName + "/";
    }

    public class GameMode {
        public enum Mode {
            Speed,
            Time,
        }
        public static Mode currentMode = Mode.Speed;
        public const string Speed = "SpeedMode";
        public const string Time = "TimeMode";

        public static readonly Vector2 SpeedDotsCount = new Vector2(10, 10);
        public static readonly Vector2 TimeDotsCount = new Vector2(20, 20);

        public static Vector2 GetDotsCount() {
            switch(currentMode) {
                case Mode.Speed:
                    return SpeedDotsCount;
                case Mode.Time:
                    return TimeDotsCount;
            }
            return SpeedDotsCount;
        }
    }
}
