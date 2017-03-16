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

    public static void LoadLevelByIndex(int index)
    {
        SceneManager.LoadScene(index);
    }

    public static void LoadLevelByName(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}
