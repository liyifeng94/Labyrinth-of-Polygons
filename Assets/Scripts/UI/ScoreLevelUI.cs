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

	// Use this for initialization
	void Start ()
	{
        TitleText.text = "";
	    ScoreText.text = "";
	    int score = 0;
	    int health = 0;
	    int level = 0;
	    uint obstacle = 0;
	    string titletemp, scoretemp;

        /* should only be true in testing */
        if (GameManager.Instance != null)
	    {
	        score = GameManager.Instance.CurrentLevelManager.GetScore();
	        health = GameManager.Instance.CurrentLevelManager.GetHealth();
	        level = GameManager.Instance.CurrentLevelManager.GetCurrentLevel();
	        obstacle = GameManager.Instance.Obstacles;
	    }

	    titletemp = BASICSCORE + "\n";
	    scoretemp = "+" + score.ToString() + "\n";

        titletemp += HEALTH + "\n";
        scoretemp += health.ToString() + "\n";

        titletemp += LEVEL + "\n";
        scoretemp += level.ToString() + "\n";

        titletemp += OBSTACLES + "\n";
        scoretemp += obstacle.ToString() + "\n";

        titletemp += BOSSKILLED + "\n";
        scoretemp += "yes" + "\n";

	    titletemp += "\n";
	    scoretemp += "\n";

	    titletemp += FINALSCORE + "\n";
	    scoretemp += "0 \n";

        TitleText.text = titletemp;
	    ScoreText.text = scoretemp;
	}
	
	// Update is called once per frame
    void Update()
    {
        
    }
}
