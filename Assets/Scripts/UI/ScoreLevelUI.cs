using UnityEngine;
using System.Collections;
using System.Xml.Schema;
using UnityEngine.SocialPlatforms.Impl;
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
    private uint obstacle = 0;
    private uint NormalKilled = 0;
    private uint AttackKilled = 0;
    private uint FlyingKilled = 0;
    private uint FastKilled = 0;
    private uint BossKilled = 0;
    private uint finalscore = 0;

    private GameManager gm;

    // Use this for initialization
    void Start ()
	{
        TitleText.text = "";
	    ScoreText.text = "";
	    
        string titletemp, scoretemp;
	    gm = GameManager.Instance;
        /* should only be true in testing */
        if (gm != null)
	    {
            gm.LocalHighScoreBoard.AddEntry(gm.LastLevelState.FinalScore,gm.PlayerName);
	        score = (uint) gm.CurrentLevelManager.GetScore();
	        level = (uint)gm.CurrentLevelManager.GetCurrentLevel();
	        obstacle = GameManager.Instance.Obstacles;
	        NormalKilled = GameManager.Instance.LastLevelState.NormalKilled;
	        AttackKilled = GameManager.Instance.LastLevelState.AttackKilled;
            FlyingKilled = GameManager.Instance.LastLevelState.FlyingKilled;
	        FastKilled = GameManager.Instance.LastLevelState.FastKilled;
	        BossKilled = GameManager.Instance.LastLevelState.BossKilled;
	        finalscore = GameManager.Instance.LastLevelState.FinalScore;
	    }

	    titletemp = BASICSCORE + "\n";
	    scoretemp = score.ToString() + "\n";

        titletemp += LEVEL + "\n";
        scoretemp += "* " + level.ToString() + "\n";

        titletemp += OBSTACLES + "\n";
        scoretemp += "+ " + obstacle.ToString() + "\n";

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
