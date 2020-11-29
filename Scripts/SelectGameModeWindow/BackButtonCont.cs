using Godot;
using System;

public class BackButtonCont : HBoxContainer {
    public override void _Ready() {

    }

    public void OnBackMenuButtonPressed() {
        GetTree().ChangeScene("res://Scenes/MainMenu.tscn");
    }
}
