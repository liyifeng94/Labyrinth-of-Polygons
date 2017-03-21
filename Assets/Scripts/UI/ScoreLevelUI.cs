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
    private string HEALTH = "Health: ";
    private string LEVEL = "Level: ";
    private string OBSTACLES = "#ofObstacles: ";
    private string BOSSKILLED = "BossKilled: ";
    private string FINALSCORE = "Final score: ";

    private string SPACE = "  ";

	// Use this for initialization
	void Start ()
	{
        TitleText.text = "";
	    ScoreText.text = "";
	    int score = 0;
	    int health = 0;
	    int level = 0;
	    int obstacle = 0;
	    string tiltetemp, scoretemp;

        /* should only be true in testing */
        if (GameManager.Instance != null)
	    {
	        score = GameManager.Instance.CurrentLevelManager.GetScore();
	        health = GameManager.Instance.CurrentLevelManager.GetHealth();
	        level = GameManager.Instance.CurrentLevelManager.GetCurrentLevel();
	        //obstacle = GameManager.Instance.LastLevelState.Level;
	    }

	    tiltetemp = SPACE + BASICSCORE + "\n";
	    scoretemp = score.ToString() + SPACE + "\n";

        tiltetemp += HEALTH + "\n";
        scoretemp += health.ToString() + "\n";

        tiltetemp += LEVEL + "\n";
        scoretemp += level.ToString() + "\n";

        tiltetemp += OBSTACLES + "\n";
        scoretemp += obstacle.ToString() + "\n";

        tiltetemp += BOSSKILLED + "\n";
        scoretemp += "yes" + "\n";

        TitleText.text = tiltetemp;
	    ScoreText.text = scoretemp;
	}
	
	// Update is called once per frame
    void Update()
    {
        
    }
}
