using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InstructionsUI : MonoBehaviour
{
    public Button PreviousBtn;
    public Button NextBtn;

    public List<GameObject> Pages;

    private int _currentPage;

	// Use this for initialization
	void Start ()
    {
        foreach (var page in Pages)
        {
            page.SetActive(false);
        }
        _currentPage = 0;
        NextBtn.interactable = false;
        PreviousBtn.interactable = false;
        OnChange();
        Pages[_currentPage].SetActive(true);
    }
	
	// Update is called once per frame
	void Update ()
	{
	}

    void OnChange()
    {
        NextBtn.interactable = true;
        PreviousBtn.interactable = true;

        if (_currentPage >= Pages.Count - 1)
        {
            NextBtn.interactable = false;
        }
        else if(_currentPage <= 0)
        {
            PreviousBtn.interactable = false;
        }
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("MenuLevel");
    }

    public void NextPage()
    {
        Pages[_currentPage].SetActive(false);
        ++_currentPage;
        Pages[_currentPage].SetActive(true);
        OnChange();
    }

    public void PreviousPage()
    {
        Pages[_currentPage].SetActive(false);
        --_currentPage;
        Pages[_currentPage].SetActive(true);
        OnChange();
    }
}
