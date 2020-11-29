using Godot;
using System;
using System.Collections.Generic;

public class Statistics : Node2D {
    PackedScene StatisticItemGenerator
        = ResourceLoader.Load("res://Prefubs/StatisticListItem.tscn")
        as PackedScene;

    public override void _Ready() {
        LoadSpeedModeStatistics();
        LoadTimeModeStatistics();
    }

    private void LoadSpeedModeStatistics() {
        LoadModeStatistics(GlobalVariables.GameMode.Speed);
    }

    private void LoadTimeModeStatistics() {
        LoadModeStatistics(GlobalVariables.GameMode.Time);
    }

    private void LoadModeStatistics(string mode) {
        StatisticsManager manager = new StatisticsManager(mode);

        List<StatisticsContainer> statistics = manager.GetStatistics();
        foreach(StatisticsContainer line in statistics) {
            Append(mode, line);
        }
    }

    private void Append(string mode, StatisticsContainer line) {
        StatisticListItem item = StatisticItemGenerator.Instance() as StatisticListItem;
        item.SetUsername(line.Username);
        item.SetTime(line.Time);
        item.SetScore(line.Score);
        switch(mode) {
            case GlobalVariables.GameMode.Speed:
                EmitSignal(nameof(SpeedModeListAppend), item);
                break;
            case GlobalVariables.GameMode.Time:
                EmitSignal(nameof(TimeModeListAppend), item);
                break;
        }
    }

    [Signal] public delegate void TimeModeListAppend(StatisticListItem item);
    [Signal] public delegate void SpeedModeListAppend(StatisticListItem item);
}
