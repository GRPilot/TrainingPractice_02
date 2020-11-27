using Godot;
using System;

public class ExitButton : Button {
	public override void _Ready() {
		
	}

	private void OnExitButtonPressed() {
		GetTree().Quit();
	}
}
