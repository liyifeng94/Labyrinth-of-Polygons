using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour
{
    public Text VersionText;
    public GameObject MainMenuPanel;
    public GameObject StartOptionsPanel;

    void Start()
    {
        if (VersionText!=null)
            VersionText.text = Application.version;
    }

    public void BackToMainMenu()
    {
        
    }

    public void SetStartOptions()
    {
        
    }

    public void LoadLevelByIndex(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void LoadLevelByName(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}
