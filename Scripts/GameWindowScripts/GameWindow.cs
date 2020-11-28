using Godot;
using System;

public class GameWindow : Node2D {
	public override void _Ready() {
		GetNode("DrawAreaContainer")
			.Connect("StartGame", this, nameof(OnStartGame));
	}

	public override void _Process(float delta) {
		if(Input.IsKeyPressed(Convert.ToInt32(KeyList.Escape))) {
			GetTree().ChangeScene("res://Scenes/MainMenu.tscn");
		}
	}

	private void OnStartGame() {
		Timer timer = GetNode<Timer>("Timer");
		if(null == timer) {
			GetNode<Label>("StatisticContainer/TimeLabel")
				.Text = "Timer was broken by someone";
		}
		timer.WaitTime = .01f;
		timer.Start();
	}
	private void OnAllCirclesActivated() {
		Timer timer = GetNode<Timer>("Timer");
		timer.Stop();
	}

}
