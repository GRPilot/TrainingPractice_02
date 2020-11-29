using Godot;
using System;

public class SpeedModeList : VBoxContainer {
    private int id = 0;
    public override void _Ready() {
        GetNode("../../../../Statistics").Connect("SpeedModeListAppend", this, nameof(Append));
    }
    public void Append(StatisticListItem item) {
        item.SetID(++id);
        AddChild(item);
    }
}


