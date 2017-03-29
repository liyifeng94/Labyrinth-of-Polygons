using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour
{
    public Slider ObstaclesSlider;
    public Text NumOfObstacles;
    public Text BonusMulti;
    public InputField NameInput;
    public Text NamePlaceHolder;
    public Text VersionText;
    public GameObject MainMenuPanel;
    public GameObject StartOptionsPanel;
    public GameObject CreditPanel;

    private GameManager _gameManager;

    void Start()
    {
        _gameManager = GameManager.Instance;
        NamePlaceHolder.text = _gameManager.PlayerName;
        MainMenuPanel.SetActive(true);
        StartOptionsPanel.SetActive(false);
        CreditPanel.SetActive(false);
        UpdateObstacles();
        if (VersionText != null)
        {
            VersionText.text = Application.version;
#if UNITY_IOS
            VersionText.text += " iOS not official supported.";
#elif UNITY_ANDROID
            VersionText.text += " Android";
#endif
        }
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
        int bonus = _gameManager.GetDifficultyMultiplier(obsacles);
        BonusMulti.text = "x" + (float)bonus / 10;
        _gameManager.Obstacles = obsacles;
    }

    public void BackToMainMenu()
    {
        StartOptionsPanel.SetActive(false);
        CreditPanel.SetActive(false);
        MainMenuPanel.SetActive(true);
    }

    public void SetStartOptions()
    {
        StartOptionsPanel.SetActive(true);
        CreditPanel.SetActive(false);
        MainMenuPanel.SetActive(false);
    }

    public void ShowCredits()
    {
        CreditPanel.SetActive(true);
        StartOptionsPanel.SetActive(false);
        MainMenuPanel.SetActive(false);
    }

    public void GoToHelpLevel()
    {
        SceneManager.LoadScene("HelpLevel");
    }
}
