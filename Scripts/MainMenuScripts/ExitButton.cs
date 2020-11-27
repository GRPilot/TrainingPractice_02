using Godot;
using System;

public class ExitButton : Button {
	private void OnExitButtonPressed() {
		GetTree().Quit();
	}
}
