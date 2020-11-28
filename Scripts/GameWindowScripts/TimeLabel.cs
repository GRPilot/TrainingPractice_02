using Godot;
using System;

public class TimeLabel : Label {

	private class Time {
		private uint milliseconds = 0;
		private uint seconds = 0;
		private uint minutes = 0;

		public static Time operator++(Time obj) {
			++obj.milliseconds;
			if(obj.milliseconds < 100) {
				return obj;
			}
			obj.milliseconds = 0;

			++obj.seconds;
			if(obj.seconds < 60) {
				return obj;
			}
			obj.seconds = 0;

			++obj.minutes;
			return obj;
		}
		public override string ToString() {
			string ms_str = "";
			if(milliseconds < 100) {
				ms_str += "0";
			}
			if(milliseconds < 10) {
				ms_str += "0";
			}
			ms_str += milliseconds.ToString();
			return string.Format("{2}:{1}:{0}", ms_str,
				(seconds < 10 ? "0" : "") + seconds.ToString(),
				(minutes < 10 ? "0" : "") + minutes.ToString()
			);
		}
	}

	Time leftTime = new Time();

	private bool running = false;

	public override void _Ready() {
		base._Ready();
		Text = leftTime.ToString();
		GetNode("../../DrawAreaContainer")
			.Connect("AllCirclesActivated", this, nameof(OnAllCirclesActivated));
		GetNode<Timer>("../../Timer")
			.Connect("timeout", this, nameof(OnTimerTick));
	}

	private void OnTimerTick() {
		++leftTime;
		Text = leftTime.ToString();
	}
	private void OnAllCirclesActivated() {
		Timer timer = GetNode<Timer>("../../Timer");
		timer.Stop();
	}
}
