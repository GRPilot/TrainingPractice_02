using Godot;
using System;

public class GameWindow : Node2D {
	public override void _Process(float delta) {
		if(Input.IsKeyPressed(Convert.ToInt32(KeyList.Escape))) {
			GetTree().ChangeScene("res://Scenes/MainMenu.tscn");
		}
	}
}
