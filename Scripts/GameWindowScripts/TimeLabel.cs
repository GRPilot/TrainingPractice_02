using Godot;
using System;

public class TimeLabel : Label {
	const string textFormat = "{0}:{1}";

	private int seconds = 0;
	private int minutes = 0;
	private bool running = false;

	public override void _Ready() {
		base._Ready();
		Text = string.Format(textFormat, "00", "00");
		Node dac = GetNode("../../DrawAreaContainer");
		dac.Connect("DrawAreaPressed", this, nameof(OnDrawAreaPressed));
		dac.Connect("AllCirclesActivated", this, nameof(OnAllCirclesActivated));
	}

	private void OnDrawAreaPressed(Vector2 MousePosition) {
		if(running) {
			return;
		}
		running = true;
		Timer timer = GetNode<Timer>("../../Timer");
		if(timer == null) {
			Text = "Timer was broken";
		}
		timer.Connect("timeout", this, nameof(OnTimerTick));
		timer.WaitTime = 1.0f;
		timer.Start();
	}

	private void OnTimerTick() {
		++seconds;
		if(seconds >= 60) {
			seconds = 0;
			++minutes;
		}
		if(minutes >= 60) {
			minutes = 0;
		}

		string sec_str = "";
		string min_str = "";

		if(seconds <= 9) {
			sec_str = "0";
		}
		if(minutes <= 9) {
			min_str = "0";
		}
		sec_str += seconds.ToString();
		min_str += minutes.ToString();

		Text = string.Format(textFormat, min_str, sec_str);
	}
	private void OnAllCirclesActivated() {
		Timer timer = GetNode<Timer>("../../Timer");
		timer.Stop();
	}
}
