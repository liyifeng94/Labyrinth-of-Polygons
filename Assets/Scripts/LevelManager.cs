using UnityEngine;
using System.Collections;
using System.Runtime.Serialization;
using Debug = System.Diagnostics.Debug;

public class LevelManager : MonoBehaviour
{
    private LevelState _currentLevelState;
    private GameManager _gameManagerInstance;
    private GameOptions _currentGameOptions;

    public TowerController TowerController;
    public EnemyController EnemyController;

    public int StartingHealth = 100;
    public int StartingGold = 100;

    public GameBoard GameBoardSystem;
    public GameObject TowerControllerPrefab;
    public GameObject EnemyControllerPrefab;

    // Use this for initialization
    void Start ()
	{
        _currentLevelState = new LevelState();
	    _currentLevelState.Gold = StartingGold;
	    _currentLevelState.Health = StartingHealth;

        _currentGameOptions = GameManager.Instance.StartGameLevel(this);
        GameBoardSystem.GameBoardSetup(_currentGameOptions);

	    GameObject towerControllerGameObject = Instantiate(TowerControllerPrefab, new Vector3(0f, 0f, 0f), Quaternion.identity) as GameObject;
	    Debug.Assert(towerControllerGameObject != null, "towerControllerGameObject != null");
	    towerControllerGameObject.transform.SetParent(transform);
	    TowerController = towerControllerGameObject.GetComponent<TowerController>();

        GameObject enemyControllerGameObject = Instantiate(EnemyControllerPrefab, new Vector3(0f, 0f, 0f), Quaternion.identity) as GameObject;
	    Debug.Assert(enemyControllerGameObject != null, "enemyControllerGameObject != null");
	    EnemyController = enemyControllerGameObject.GetComponent<EnemyController>();
        enemyControllerGameObject.transform.SetParent(transform);
    }
	
	// Update is called once per frame
	void Update ()
    {
	    
	}

    public void EnterBattePhase()
    {
        GameBoardSystem.EnterBattlePhase();
        EnemyController.StartSpawning();

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

    void OnDestory()
    {
        EndGame();
    }

    public void EndGame()
    {
        _gameManagerInstance.SaveGameState(_currentLevelState);
    }
}
