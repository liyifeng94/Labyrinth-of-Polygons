using System;
using System.Collections.Generic;

[Serializable]
public class HighScoreBoard
{
    [Serializable]
    public class HighScoreEntry
    {
        public string Name;
        public uint Score;
        public string DateTime;
    }

    public List<HighScoreEntry> HighScoreList { get; private set; }

    //Creates a score board sorted by time
    public HighScoreBoard()
    {
        HighScoreList = new List<HighScoreEntry>();
    }

    public void AddEntry(uint score, string playerName)
    {
        HighScoreEntry newHighScoreEntry = new HighScoreEntry();
        newHighScoreEntry.Name = playerName;
        newHighScoreEntry.Score = score;
        newHighScoreEntry.DateTime = DateTime.UtcNow.ToString();

        HighScoreList.Add(newHighScoreEntry);
    }
}
