﻿using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour
{
    public Slider ObstaclesSlider;
    public Text NumOfObstacles;
    public InputField NameInput;
    public Text NamePlaceHolder;
    public Text VersionText;
    public GameObject MainMenuPanel;
    public GameObject StartOptionsPanel;

    private GameManager _gameManager;

    void Start()
    {
        _gameManager = GameManager.Instance;
        NamePlaceHolder.text = _gameManager.PlayerName;
        if (VersionText!=null)
            VersionText.text = Application.version;
    }

    public void ChangeName()
    {
        string newName = NameInput.text;
        _gameManager.PlayerName = newName;
    }

    public void UpdateObstacles()
    {
        uint obsacles = (uint)ObstaclesSlider.value;
        NumOfObstacles.text = obsacles.ToString();
        _gameManager.Obstacles = obsacles;
    }

    public void BackToMainMenu()
    {
        StartOptionsPanel.SetActive(false);
        MainMenuPanel.SetActive(true);
    }

    public void SetStartOptions()
    {
        StartOptionsPanel.SetActive(true);
        MainMenuPanel.SetActive(false);
    }
}
