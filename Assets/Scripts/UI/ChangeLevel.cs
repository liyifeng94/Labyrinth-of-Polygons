using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeLevel : MonoBehaviour {

    public static void LoadLevel(int index)
    {
        SceneManager.LoadScene(index);
    }

    public static void LoadLevel(string scene)
    {
        SceneManager.LoadScene(scene);
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
