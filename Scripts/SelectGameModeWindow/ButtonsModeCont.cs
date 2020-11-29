using Godot;
using System;

public class ButtonsModeCont : HBoxContainer {

    public void OnTimeModeButtonPressed() {
        GlobalVariables.GameMode.CurrentMode = GlobalVariables.GameMode.Mode.Time;
        StartGame();
    }
    public void OnSpeedModeButtonPressed() {
        GlobalVariables.GameMode.CurrentMode = GlobalVariables.GameMode.Mode.Speed;
        StartGame();
    }

    private void StartGame() {
        GetTree().ChangeScene("res://Scenes/GameWindow.tscn");
    }
}
