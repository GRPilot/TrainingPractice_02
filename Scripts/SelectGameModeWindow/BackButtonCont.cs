using Godot;
using System;

public class BackButtonCont : HBoxContainer {

    public void OnBackMenuButtonPressed() {
        GetTree().ChangeScene("res://Scenes/MainMenu.tscn");
    }
}
