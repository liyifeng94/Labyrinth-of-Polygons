using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelUI : MonoBehaviour
{

    public Text ScoreCount;
    public Text HealthCount;
    public Text GoldCount;
    public Text WaveCount;

    public GameObject BuildingPhasePanel;
    public GameObject BattlePhasePanel;

    private LevelManager _levelManager;

    private GameBoard.GamePhase _currentGamePhase;

	// Use this for initialization
	void Start ()
	{
	    _levelManager = GameManager.Instance.CurrentLevelManager;
	    ScoreCount.text = "";
	    HealthCount.text = "";
	    GoldCount.text = "";
	    WaveCount.text = "";
	}
	
	// Update is called once per frame
	void Update ()
	{
	    if (_levelManager == null)
	    {
            _levelManager = GameManager.Instance.CurrentLevelManager;
        }
	    _currentGamePhase = _levelManager.CurrentGamePhase();
	    if (_currentGamePhase == GameBoard.GamePhase.BuildingPhase)
	    {
	        BuildingPhasePanel.SetActive(true);
            BattlePhasePanel.SetActive(false);

        }

        int intScoreCount = _levelManager.GetScore();
	    int intHealthCount = _levelManager.GetHealth();
	    int intGoldCount = _levelManager.GetGold();
	    int intWaveCount = _levelManager.GetCurrentLevel();

	    ScoreCount.text = intScoreCount.ToString();
	    HealthCount.text = intHealthCount.ToString();
	    GoldCount.text = intGoldCount.ToString();
	    WaveCount.text = intWaveCount.ToString();

	}

    public void EnterBattlePhase()
    {
        if (_levelManager == null)
        {
            _levelManager = GameManager.Instance.CurrentLevelManager;
        }
        _levelManager.EnterBattePhase();
        BuildingPhasePanel.SetActive(false);
        BattlePhasePanel.SetActive(true);
    }
}
