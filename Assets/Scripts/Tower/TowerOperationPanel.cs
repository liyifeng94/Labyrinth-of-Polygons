﻿using UnityEngine;

public class TowerOperationPanel : MonoBehaviour {

    public static TowerOperationPanel Instance;
    public GameObject ThisPanel;


    void Awake()
    {
        Instance = this;
    }


    void Start () {
        ThisPanel.SetActive(false);
    }


    public void Appear()
    {
        ThisPanel.SetActive(true);
    }


    public void DisAppear()
    {
        ThisPanel.SetActive(false);
    }
}
