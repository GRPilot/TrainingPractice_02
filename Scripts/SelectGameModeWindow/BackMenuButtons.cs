using Godot;
using System;

public class BackMenuButton : Button {
    public void OnBackMenuButtonPressed() {
        GetTree().ChangeScene("res://Scenes/MainMenu.tscn");
    }
}
