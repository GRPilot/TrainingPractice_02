using Godot;
using System;

public class ResultPanel : PopupPanel {
	public void SetTitle(string title) {
		GetNode<Label>("CenterContainer/ControlsArea/Title").Text = title;
	}
	public void SetScore(string score) {
		GetNode<Label>("CenterContainer/ControlsArea/ScoreArea/Score").Text = score;
	}
	public void SetTime(string time) {
		GetNode<Label>("CenterContainer/ControlsArea/TimeArea/Time").Text = time;
	}

	private void OnBackButtonPressed() {
		GetTree().ChangeScene("res://Scenes/MainMenu.tscn");
	}

	private void OnSaveResultButtonPressed() {
		// saving statistics
		GetTree().ChangeScene("res://Scenes/MainMenu.tscn");
	}
}


