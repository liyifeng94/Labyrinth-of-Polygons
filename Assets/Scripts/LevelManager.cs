using UnityEngine;
using System.Collections;
using System.Runtime.Serialization;

public class LevelManager : MonoBehaviour
{
    private class LevelState
    {
        public int Gold;
        public int Score;
        public int Health;
    }

    private LevelState _currentLevelState;
    private GameManager _gameManagerInstance;

    public int StartingHealth = 100;
    public int StartingGold = 100;

    public GameBoard GameBoardSystem;
    public GameObject TowerController;
    public GameObject EnemyController;

    // Use this for initialization
    void Start ()
	{
        _currentLevelState = new LevelState();
	    _currentLevelState.Gold = StartingGold;
	    _currentLevelState.Health = StartingHealth;
        GameManager.Instance.UpdateLevelManager(this);
	    GameObject towerControllerGameObject = Instantiate(TowerController, new Vector3(0f, 0f, 0f), Quaternion.identity) as GameObject;
        towerControllerGameObject.transform.SetParent(transform);
        GameObject enemyControllerGameObject = Instantiate(EnemyController, new Vector3(0f, 0f, 0f), Quaternion.identity) as GameObject;
        enemyControllerGameObject.transform.SetParent(transform);
    }
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    public void EnterBattePhase()
    {
        GameBoardSystem.EnterBattlePhase();
    }

    public void EnterBuildingPhase()
    {
        GameBoardSystem.EnterBuildingPhase();
    }

    public GameBoard.GamePhase CurrentGamePhase()
    {
        return GameBoardSystem.CurrentGamePhase;
    }

    //Adds the score value of the enemy
    public void DestoryEnemy(Enemy enemyPtr)
    {
        GameBoardSystem.RemoveEnemy(enemyPtr);
        //TODO: add the score value of the enemy
    }

    public int GetGold()
    {
        return _currentLevelState.Gold;
    }

    public void UseGold(int amount)
    {
        _currentLevelState.Gold -= amount;
    }

    public void AddGold(int amount)
    {
        _currentLevelState.Gold += amount;
    }

    public void RemoveHealth(int damage)
    {
        _currentLevelState.Health -= damage;
    }

    public int GetHealth()
    {
        return _currentLevelState.Health;
    }

    public void AddScore(int score)
    {
        _currentLevelState.Score += score;
    }

    public int GetScore()
    {
        return _currentLevelState.Score;
    }
}
