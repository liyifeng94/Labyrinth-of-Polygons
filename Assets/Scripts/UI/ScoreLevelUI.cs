﻿using UnityEngine;
using UnityEngine.UI;

public class ScoreLevelUI : MonoBehaviour
{
    public Text TitleText;
    public Text ScoreText;

    private string BASICSCORE = "Basic Score: ";
    private string LEVEL = "Level: ";
    private string OBSTACLES = "#ofObstacles: ";
    private string BOSSKILLED = "BossKilled: ";
    private string NORMALK = "Normal Enemy Killed: ";
    private string FLYK = "Flying Enemy Killed: ";
    private string FASTK = "Fast Enemy Killed: ";
    private string ATTK = "Attack Enemy Killed: ";
    private string FINALSCORE = "Final score: ";

    private uint score = 0;
    private uint level = 0;
    private float obstacle = 0;
    private uint NormalKilled = 0;
    private uint AttackKilled = 0;
    private uint FlyingKilled = 0;
    private uint FastKilled = 0;
    private uint BossKilled = 0;
    private uint finalscore = 0;

    private GameManager gm;
    private LevelState gmi;

    // Use this for initialization
    void Start ()
	{
        TitleText.text = "";
	    ScoreText.text = "";

	    gm = GameManager.Instance;
	    gmi = gm.LastLevelState;
	    gm.LocalHighScoreBoard.AddEntry(gm.LastLevelState.FinalScore, gm.PlayerName);
	    score = (uint) gm.CurrentLevelManager.GetScore();
	    level = (uint) gm.CurrentLevelManager.GetCurrentLevel();
	    obstacle = gm.GetDifficultyMultiplier(gm.Obstacles);
	    NormalKilled = gmi.NormalKilled;
	    AttackKilled = gmi.AttackKilled;
	    FlyingKilled = gmi.FlyingKilled;
	    FastKilled = gmi.FastKilled;
	    BossKilled = gmi.BossKilled;
	    finalscore = gmi.FinalScore;
	    
	    var titletemp = BASICSCORE + "\n";
	    var scoretemp = score.ToString() + "\n";

        titletemp += LEVEL + "\n";
        scoretemp += "* " + level.ToString() + "\n";

        titletemp += OBSTACLES + "\n";
        scoretemp += "* " + obstacle/10 + "\n";

	    titletemp += NORMALK + "\n";
	    scoretemp += "+" + NormalKilled.ToString() + "* 100" + "\n";

        titletemp += ATTK + "\n";
        scoretemp += "+" + AttackKilled.ToString() + "* 200" + "\n";

        titletemp += FASTK + "\n";
	    scoretemp += "+" + FastKilled.ToString() + "* 300" + "\n";

        titletemp += FLYK + "\n";
	    scoretemp += "+" + FlyingKilled.ToString() + "* 300" + "\n";

        titletemp += BOSSKILLED + "\n";
        scoretemp += "+" + BossKilled + " x 1000" + "\n";

	    titletemp += "\n";
	    scoretemp += "\n";

	    titletemp += FINALSCORE + "\n";
	    scoretemp += finalscore.ToString() + "\n";

        TitleText.text = titletemp;
	    ScoreText.text = scoretemp;

    
	    gm.SaveHighScoreBoard();
	}
	
	// Update is called once per frame
    void Update()
    {
        
    }
}
