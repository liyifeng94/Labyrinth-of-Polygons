using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeLevel : MonoBehaviour {


    public void LoadLevelByIndex(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void LoadLevelByName(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}
