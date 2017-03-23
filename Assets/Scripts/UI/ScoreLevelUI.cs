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

    // Use this for initialization
    void Start ()
	{
        TitleText.text = "";
	    ScoreText.text = "";
	    
        string titletemp, scoretemp;

        /* should only be true in testing */
        if (GameManager.Instance != null)
	    {
	        score = (uint) GameManager.Instance.CurrentLevelManager.GetScore();
	        level = (uint) GameManager.Instance.CurrentLevelManager.GetCurrentLevel();
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
        scoretemp += "*" + level.ToString() + "\n";

        titletemp += OBSTACLES + "\n";
        scoretemp += "+" + obstacle.ToString() + "\n";

	    titletemp += NORMALK + "*" + NormalKilled.ToString() + "\n";
	    scoretemp += NormalKilled.ToString() + "* 100" + "\n";

        titletemp += ATTK + "*" + AttackKilled.ToString() + "\n";
        scoretemp += AttackKilled.ToString() + "* 200" + "\n";

        titletemp += FASTK + "*" + FastKilled.ToString() + "\n";
	    scoretemp += FastKilled.ToString() + "* 300" + "\n";

        titletemp += FLYK + "*" + FlyingKilled.ToString() + "\n";
	    scoretemp += FlyingKilled.ToString() + "* 300" + "\n";

        titletemp += BOSSKILLED + " *" + BossKilled.ToString() + "\n";
        scoretemp += BossKilled + " x 1000" + "\n";

	    titletemp += "\n";
	    scoretemp += "\n";

	    titletemp += FINALSCORE + "\n";
	    scoretemp += finalscore.ToString() + "\n";

        TitleText.text = titletemp;
	    ScoreText.text = scoretemp;
	}
	
	// Update is called once per frame
    void Update()
    {
        
    }
}
