using Godot;
using System;

public class DrawAreaContainer : PanelContainer {
    private bool gameDone = false;
    private bool gameRunning = false;
    private int score = 0;
    private int clicks = 0;
    private int lastScore = 0;
    private int DotsInRow;
    private int DotsInColumn;
    private int maxScore = 0;
    private PackedScene CirclePrefub = ResourceLoader.Load("res://Prefubs/Circle.tscn") as PackedScene;

    public DrawAreaContainer() {
        Vector2 dotsCount = GlobalVariables.GameMode.GetDotsCount();
        DotsInRow = Convert.ToInt32(dotsCount.x);
        DotsInColumn = Convert.ToInt32(dotsCount.y);
        maxScore = DotsInColumn * DotsInRow;
    }
    public override void _Ready() {
        SetProcessInput(true);
        InitializeCircles(DotsInRow, DotsInColumn);
        base._Ready();
    }

    public override void _Input(InputEvent @event) {
        if(gameDone) {
            return;
        }
        if(!Input.IsActionJustReleased("mouse_left")) {
            return;
        }
        UpdateClicks();

        if(IsMouseInField() && !gameRunning) {
            EmitSignal(nameof(StartGame));
            gameRunning = false;
        }

        if(score == maxScore) {
            WinGame();
        }

        if(lastScore == score) {
            EmitSignal(nameof(UserMissClick));
        }

        base._Input(@event);
    }

    private void WinGame() {
        EmitSignal(nameof(AllCirclesActivated), score);
        gameDone = true;
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
        Vector2 sz = RectSize;
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
        if(GlobalVariables.GameMode.CurrentMode == GlobalVariables.GameMode.Mode.Time) {
            obj.Scale -= new Vector2(0.006f, 0.006f);
        }
        AddChild(obj);
        obj.Owner = this;
        obj.Connect("Activated", this, nameof(OnActivated));
    }

    public int GetScore() {
        return score;
    }
    private void OnActivated() {
        ++score;
        UpdateLabel("ScoreCount", score);
    }

    private void UpdateClicks() {
        ++clicks;
        UpdateLabel("ClickCount", clicks);
    }

    private void UpdateLabel(string name, int value) {
        GetNode<Label>($"../StatisticContainer/{name}").Text = $" {value}";
    }
    [Signal] public delegate void StartGame();
    [Signal] public delegate void AllCirclesActivated(int score);
    [Signal] public delegate void UserMissClick();


}
