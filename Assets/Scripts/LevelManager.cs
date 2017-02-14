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
    public GameBoard GameBoardSystem { get; private set; }

    public GameObject GameBoardObject;

    // Use this for initialization
    void Start ()
	{
        _currentLevelState = new LevelState();
        GameBoardSystem = gameObject.GetComponent<GameBoard>();
        GameManager.Instance.UpdateLevelManager(this);
    }
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    //Adds the score value of the enemy
    public void DestoryEnemy(Enemy enemyPtr)
    {
        GameBoardSystem.RemoveEnemy(enemyPtr);
        //TODO: add the score value of the enemy
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
}
