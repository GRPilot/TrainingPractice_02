using Godot;
using System;
using System.Collections.Generic;
using System.IO;

public class StatisticsContainer {
    public string Username = "<unnamed>";
    public int Score = 0;
    public string Time = "00:00:00";
    public StatisticsContainer(string username, int score, string time) {
        Username = username;
        Score = score;
        Time = time;
    }
    public StatisticsContainer(string UserInfo) {
        string[] splited = UserInfo.Split('|');
        if(splited.Length != 3) {
            return;
        }

        Username = splited[0];
        if(!Int32.TryParse(splited[1], out Score)) {
            Score = 0;
        }
        Time = splited[2];
    }
    public override string ToString() {
        return $"{Username}|{Score}|{Time}\n";
    }
}

public class StatisticsManager : Node {

    static string StatisticsPath;
    string StatisticsName = "Statistics";

    public StatisticsManager() {
        InitOsPath();
    }
    public StatisticsManager(string filename) {
        StatisticsName = filename;
        InitOsPath();
    }

    public bool Save(StatisticsContainer data) {
        if(!System.IO.Directory.Exists(StatisticsPath)) {
            System.IO.Directory.CreateDirectory(StatisticsPath);
        }
        if(!System.IO.File.Exists(StatisticsPath + StatisticsName)) {
            System.IO.File.Create(StatisticsPath + StatisticsName).Close();
        }

        string[] lines = System.IO.File.ReadAllLines(StatisticsPath + StatisticsName);
        for(int i = 0; i < lines.Length; ++i) {
            if(!lines[i].Contains(data.Username)) {
                continue;
            }
            string[] splitted = lines[i].Split('|');
            if(splitted.Length != 3) {
                continue;
            }


            string sc = splitted[1];
            string tm = splitted[2];
            if(Convert.ToInt32(sc) < data.Score || timeComparing(tm, data.Time) > 0) {
                   lines[i] = data.ToString();
                   System.IO.File.WriteAllLines(StatisticsPath + StatisticsName, lines);
            }
            return true;
        }

        System.IO.File.AppendAllText(StatisticsPath + StatisticsName, data.ToString());
        return true;
    }

    public List<StatisticsContainer> GetStatistics() {
        if(!System.IO.File.Exists(StatisticsPath + StatisticsName)) {
            return null;
        }
        List<StatisticsContainer> data = new List<StatisticsContainer>();

        string[] lines = System.IO.File.ReadAllLines(StatisticsPath + StatisticsName);
        foreach(string line in lines) {
            if(line.Empty()) {
                continue;
            }
            data.Add(new StatisticsContainer(line));
        }
        return data;
    }

    private void InitOsPath() {
        switch(OS.GetName()) {
            case "Windows":
                StatisticsPath = GlobalVariables.Statistics.WinPath;
                break;
            case "OSX":
            case "MacOS":
                StatisticsPath = GlobalVariables.Statistics.MacOSPath;
                break;
            case "Linux":
                StatisticsPath = GlobalVariables.Statistics.LinuxPath;
                break;
            default:
                StatisticsPath = GlobalVariables.Statistics.WinPath;
                break;
        }
    }
    /**
     * return:
     *  -> -1 : the first is greater than second
     *  ->  0 : the first and second are equals
     *  ->  1 : the second is greater than first
     */
    private int timeComparing(string first, string second) {
        if(first.Empty() && second.Empty()) {
            return 0;
        }
        if(first.Empty()) {
            return -1;
        }
        if(second.Empty()) {
            return 1;
        }
        int delimetrPosition1 = first.Find(":");
        int fMinutes = Convert.ToInt32(first.Substr(0, delimetrPosition1 - 1));
        int sMinutes = Convert.ToInt32(second.Substr(0, delimetrPosition1 - 1));
        if(fMinutes > sMinutes) {
            return -1;
        }
        if(fMinutes < sMinutes) {
            return 1;
        }

        int delimetrPosition2 = first.FindLast(":");
        int fSeconds = Convert.ToInt32(first.Substr(delimetrPosition1 + 1, 2));
        int sSeconds = Convert.ToInt32(second.Substr(delimetrPosition1 + 1, 2));
        if(fSeconds > sSeconds) {
            return -1;
        }
        if(fSeconds < sSeconds) {
            return 1;
        }

        int fMilliseconds = Convert.ToInt32(first.Substring(delimetrPosition2 + 1));
        int sMilliseconds = Convert.ToInt32(second.Substring(delimetrPosition2 + 1));
        if(fMilliseconds > sMilliseconds) {
            return -1;
        }
        if(fMilliseconds < sMilliseconds) {
            return 1;
        }

        return 0;
    }
}
