using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeLevel : MonoBehaviour {


    public static void LoadLevelByIndex(int index)
    {
        SceneManager.LoadScene(index);
    }

    public static void LoadLevelByName(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}
