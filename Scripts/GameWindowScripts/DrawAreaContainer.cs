using Godot;
using System;

public class DrawAreaContainer : PanelContainer {
	private int score = 0;
	private int DotsInRow;
	private int DotsInColumn;
	private int maxScore = 0;
	private PackedScene CirclePrefub = ResourceLoader.Load("res://Prefubs/Circle.tscn") as PackedScene;

	public DrawAreaContainer() {
		// TODO: Make settings singltone class which contain count of dots
		DotsInRow = 5;
		DotsInColumn = 5;
		maxScore = DotsInColumn * DotsInRow;
	}
	public override void _Ready() {
		SetProcessInput(true);
		InitializeCircles(DotsInRow, DotsInColumn);
		base._Ready();
	}

	public override void _Input(InputEvent @event) {
		if(Input.IsActionJustReleased("mouse_left")) {
			if(IsMouseInField()) {
				EmitSignal(nameof(DrawAreaPressed), GetLocalMousePosition());
			}
			if(score == maxScore) {
				WinGame();
			}
		}
		base._Input(@event);
	}

	public override void _PhysicsProcess(float delta) {
		base._PhysicsProcess(delta);
	}

	private void WinGame() {
		// Send all status in statistics class
		EmitSignal(nameof(AllCirclesActivated));
	}

	private bool IsMouseInField() {
		Vector2 mousePos = GetLocalMousePosition();
		if(mousePos.x < RectPosition.x || mousePos.y < RectPosition.x) {
			return false;
		}
		if(mousePos.x > RectSize.x ||
		   mousePos.y > RectSize.y) {
			return false;
		}
		return true;
	}

	private void InitializeCircles(int countInRow, int countInColumn) {
		const int scale = 2;
		Vector2 sz = GetViewportRect().Size;
		int horizontalPadding = Convert.ToInt32(sz.x) / countInRow / scale;
		int verticalPadding = Convert.ToInt32(sz.y) / countInColumn / scale;
		for(int h = 1; h <= countInColumn; ++h) {
			for(int w = 1; w <= countInRow; ++w) {
				Vector2 pos = new Vector2(
					((Convert.ToInt32(sz.x) / countInRow) * w) - horizontalPadding,
					(((Convert.ToInt32(sz.y) - verticalPadding) / countInColumn) * h) - verticalPadding
				);
				SpawnCircle(pos);
			}
		}
	}

	private void SpawnCircle(Vector2 position) {
		Circle obj = CirclePrefub.Instance() as Circle;
		obj.Position = position;
		obj.Scale = new Vector2(0.1f, 0.1f);
		AddChild(obj);
		obj.Owner = this;
		obj.Connect("Activated", this, nameof(OnActivated));
	}

	private void OnActivated() {
		++score;
		GD.Print("Score updated: " + score.ToString());
	}

	[Signal] public delegate void DrawAreaPressed(Vector2 MousePosition);
	[Signal] public delegate void AllCirclesActivated();
}
