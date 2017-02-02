using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public LevelManager CurrentLevelManager;

	// Use this for initialization
	void Awake ()
	{
	    if (Instance == null)
	    {
	        Instance = this;
            CurrentLevelManager = null;
        }
	    else
	    {
	        Destroy(gameObject);
	    }

	    DontDestroyOnLoad(gameObject);
    }
}
