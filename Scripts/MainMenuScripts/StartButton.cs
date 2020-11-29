using Godot;
using System;

public class StartButton : Button {
    private void OnStartButtonPressed() {
        GetTree().ChangeScene("res://Scenes/SelectGameModeWindow.tscn");
    }
}
