using UnityEngine;
using System.Collections;
using System.Xml.Schema;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class ScoreLevelUI : MonoBehaviour
{
    public Text ResultText;

    private string BASICSCORE = "Basic Score: ";
    private string HEALTH = "Health: ";
    private string LEVEL = "Level: ";
    private string OBSTACLES = "#ofObstacles: ";
    private string BOSSKILLED = "BossKilled: ";

	// Use this for initialization
	void Start ()
	{
        /* should only be true in testing */
        if (GameManager.Instance == null)
        {
            ResultText.text = "0";
            return;
        }
        ResultText.text = "";
	    int score = 0;
	    int health = 0;
	    int level = 0;
	    int obstacle = 0;
	    string temp;
	    
        score = GameManager.Instance.CurrentLevelManager.GetScore();
	    health = GameManager.Instance.CurrentLevelManager.GetHealth();
	    level = GameManager.Instance.CurrentLevelManager.GetCurrentLevel();
	    //obstacle = GameManager.Instance.LastLevelState.Level;

        temp = BASICSCORE + score.ToString() + "\n";
	    temp += HEALTH + health.ToString() + "\n";
	    temp += LEVEL + level.ToString() + "\n";
	    temp += OBSTACLES + obstacle.ToString() + "\n";
	    temp += BOSSKILLED + "yes" + "\n";

	    ResultText.text = temp;
	}
	
	// Update is called once per frame
    void Update()
    {
        
    }
}
