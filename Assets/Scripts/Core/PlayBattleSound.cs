using UnityEngine;

public class PlayBattleSound : MonoBehaviour {

    [HideInInspector] public GameManager GameManager;

    void Start()
    {
        GameManager = GameManager.Instance;
    }


    public void PlaySound()
    {
        GameManager.PlaybattleSound();
    }
}
