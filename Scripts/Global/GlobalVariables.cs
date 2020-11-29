using Godot;
using System;

public class GlobalVariables : Node {
    public const string GameShortName = "IYHSS";
    public struct Statistics {
        public const string WinPath = GameShortName + "/"; //"%APPDATA%/" + GameShortName + "/";
        public const string MacOSPath = GameShortName + "/"; //"~/Library/Application Support/" + GameShortName + "/";
        public const string LinuxPath = GameShortName + "/"; //"~/.local/share/" + GameShortName + "/";
    }

    public struct GameMode {
        public const string Speed = "SpeedMode";
        public const string Time = "TimeMode";
    }
}
