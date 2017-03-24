﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderBoardUI : MonoBehaviour
{

    public Text NameResultText;
    public Text TimeResultText;
    public Text ScoreResultText;

	// Use this for initialization
	void Start ()
	{
	    NameResultText.text = "";
	    TimeResultText.text = "";
	    ScoreResultText.text = "";

	    HighScoreBoard temp = GameManager.Instance.LocalHighScoreBoard;
	    List<HighScoreBoard.HighScoreEntry> HighScoreList = temp.HighScoreList;
	    foreach (var entry in HighScoreList)
	    {
	        NameResultText.text += entry.Name + "\n";
	        TimeResultText.text += entry.DateTime + "\n";
	        ScoreResultText.text += entry.Score.ToString() + "\n";
	    }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
