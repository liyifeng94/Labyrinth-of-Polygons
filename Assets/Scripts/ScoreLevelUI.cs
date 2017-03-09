﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreLevelUI : MonoBehaviour
{
    public Text ScoreCount;

	// Use this for initialization
	void Start ()
	{
	    int score = 0;
	    if (GameManager.Instance != null)
            score = GameManager.Instance.CurrentLevelManager.GetScore();
        ScoreCount.text = score.ToString();
    }
	
	// Update is called once per frame
    void Update()
    {
        
    }
}
