using UnityEngine;

public class GameLoader : MonoBehaviour
{

    public GameObject GameManagerPrefab;

	// Use this for initialization of game.
    void Awake()
    {
        if(GameManager.Instance == null)
        {
            Instantiate(GameManagerPrefab);
        }
    }
}
