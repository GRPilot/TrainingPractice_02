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

    public string GetUsername() {
        return GetNode<LineEdit>("CenterContainer/ControlsArea/UsernameInputBox").Text;
    }

    public int GetScore() {
        return Convert.ToInt32(
            GetNode<Label>("CenterContainer/ControlsArea/ScoreArea/Score").Text
        );
    }
    public string GetTime() {
        return GetNode<Label>("CenterContainer/ControlsArea/TimeArea/Time").Text;
    }
    private void OnSaveResultButtonPressed() {
        string username = GetUsername();
        if(username.Length == 0) {
            IncorrectUsername();
            return;
        }
        StatisticsManager manager = new StatisticsManager(
            GlobalVariables.GameMode.GetCurrentModeStr()
        );
        StatisticsContainer userdata = new StatisticsContainer(
            GetUsername(), GetScore(), GetTime()
        );
        manager.Save(userdata);

        GetTree().ChangeScene("res://Scenes/MainMenu.tscn");
    }

    private void IncorrectUsername() {
        LineEdit inputbox = GetNode<LineEdit>("CenterContainer/ControlsArea/UsernameInputBox");
        inputbox.PlaceholderText = "There is a lil empty!";
    }
}


