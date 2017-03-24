using System;
using System.Collections.Generic;

public class HighScoreBoard
{
    [Serializable]
    public class HighScoreEntry
    {
        public string Name;
        public uint Score;
        public string DateTime;
    }

    [Serializable]
    public class HighScoreList
    {
        public List<HighScoreEntry> MainList;

        public HighScoreList()
        {
            MainList = new List<HighScoreEntry>();
        }
    }

    public HighScoreList ScoreList;

    //Creates a score board sorted by time
    public HighScoreBoard(HighScoreList scoreList)
    {
        ScoreList = scoreList;
        if (ScoreList == null)
        {
            ScoreList = new HighScoreList();
        }
    }

    public void AddEntry(uint score, string playerName)
    {
        HighScoreEntry newHighScoreEntry = new HighScoreEntry();
        newHighScoreEntry.Name = playerName;
        newHighScoreEntry.Score = score;
        newHighScoreEntry.DateTime = DateTime.UtcNow.ToString();

        ScoreList.MainList.Add(newHighScoreEntry);
    }
}
