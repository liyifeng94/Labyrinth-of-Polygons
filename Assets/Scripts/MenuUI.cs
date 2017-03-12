using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour
{
    public Text VersionText;

    void Start()
    {
        VersionText.text = Application.version;
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
