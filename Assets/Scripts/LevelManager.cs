using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour
{
    private GameManager _gameManagerInstance;
    public GameBoard GameBoardSystem { get; private set; }

    public GameObject GameBoardObject;

    // Use this for initialization
    void Start ()
	{
	    GameManager.Instance.CurrentLevelManager = this;
        GameBoardSystem = gameObject.GetComponent<GameBoard>();
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    public void DestoryEnemy(Enemy enemyPtr)
    {
        GameBoardSystem.RemoveEnemy(enemyPtr);
    }
}
