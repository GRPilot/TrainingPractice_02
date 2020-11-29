using Godot;
using System;

public class StatisticListItem : HBoxContainer {

    public void SetID(int id) {
        SetChildText("ID", id.ToString());
    }

    public void SetUsername(string username) {
        SetChildText("Username", username);
    }

    public void SetTime(string time) {
        SetChildText("Time", time);
    }

    public void SetScore(int score) {
        SetChildText("Score", score.ToString());
    }

    private void SetChildText(string child, string text) {
        GetNode<Label>(child).Text = text;
    }

}
