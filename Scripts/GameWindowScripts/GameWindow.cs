using Godot;
using System;

public class GameWindow : Node2D {
    private PackedScene ResultPanelBuilder
        = ResourceLoader.Load("res://Scripts/Prefubs/ResultPanel.cs") as PackedScene;

    public override void _Ready() {
        GetNode("DrawAreaContainer")
            .Connect("StartGame", this, nameof(OnStartGame));

        GetNode("DrawAreaContainer")
            .Connect("AllCirclesActivated", this, nameof(OnAllCirclesActivated));

        GetNode("StatisticContainer/TimeLabel")
            .Connect("TimeLeft", this, nameof(OnTimeLeft));
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
        timer.Start();
    }
    private void OnAllCirclesActivated(int score) {
        ShowResults("You are win!", score);
    }

    private void ShowResults(string title, int score) {
        ResultPanel resultPanel = GetNode<ResultPanel>("ResultPanel");
        resultPanel.SetTitle(title);
        resultPanel.SetScore(score.ToString());
        resultPanel.SetTime(
            GetNode<TimeLabel>("StatisticContainer/TimeLabel").GetTime()
        );
        resultPanel.PopupCentered();
    }

    private void OnTimeLeft() {
        int score = GetNode<DrawAreaContainer>("DrawAreaContainer").GetScore();
        ShowResults("Time left!", score);
    }
}
