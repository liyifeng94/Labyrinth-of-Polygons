using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayStartSound : MonoBehaviour {

    [HideInInspector] public GameManager GameManager;

    void Start ()
    {
        GameManager = GameManager.Instance;
    }

    public void PlaySound()
    {
        GameManager.PlayStartSound();
    }
}
