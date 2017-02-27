using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelUI : MonoBehaviour
{

    public Text ScoreCount;
    public Text HealthCount;
    public Text GoldCount;

    private LevelManager _levelManager;

	// Use this for initialization
	void Start ()
	{
	    _levelManager = GameManager.Instance.CurrentLevelManager;
	    ScoreCount.text = "";
	    HealthCount.text = "";
	    GoldCount.text = "";
	}
	
	// Update is called once per frame
	void Update ()
	{
	    if (_levelManager == null)
	    {
            _levelManager = GameManager.Instance.CurrentLevelManager;
        }
	    int intScoreCount = _levelManager.GetScore();
	    int intHealthCount = _levelManager.GetHealth();
	    int intGoldCount = _levelManager.GetGold();

	    ScoreCount.text = intScoreCount.ToString();
	    HealthCount.text = intHealthCount.ToString();
	    GoldCount.text = intGoldCount.ToString();
	}
}
