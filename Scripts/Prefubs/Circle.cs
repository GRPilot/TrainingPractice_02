using Godot;
using System;

public class Circle : Node2D {
	public bool enable = false;

	public override void _Input(InputEvent @event) {
		if(enable) {
			return;
		}

		if(!Input.IsActionJustPressed("mouse_left")) {
			return;
		}

		if(!IsMouseInCircle()) {
			return;
		}

		EmitSignal(nameof(Activated));
		Enable();

		base._Input(@event);
	}

	public void Enable() {
		if(enable) {
			return;
		}
		enable = true;
		Switch(enable);
	}
	public void Disable() {
		if(!enable) {
			return;
		}
		enable = false;
		Switch(enable);
	}

	private bool IsMouseInCircle() {
		float radius = GetChild<Sprite>(0).Texture.GetSize().x / 2;
		radius *= Scale.x;
		Vector2 mPos = GetGlobalMousePosition();
		float distance = Position.DistanceTo(mPos);

		return distance < radius;
	}
	private void Switch(bool active) {
		Sprite red = GetChild<Sprite>(0);
		red.Visible = !active;

		Sprite green = GetChild<Sprite>(1);
		green.Visible = active;
	}

	[Signal] private delegate void Activated();
}
