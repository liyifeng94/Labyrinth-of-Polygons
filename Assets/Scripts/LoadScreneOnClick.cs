using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadScreneOnClick : MonoBehaviour
{
    public void LoadByIndex(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void LoadByName(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}
