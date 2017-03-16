using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreLevelUI : MonoBehaviour
{
    public Text ResultText;

	// Use this for initialization
	void Start ()
	{
	    ResultText.text = "";
	    int score = 0;
	    string temp;
	    if (GameManager.Instance != null)
            score = GameManager.Instance.CurrentLevelManager.GetScore();

	    temp = score.ToString();
	    ResultText.text = temp;
	}
	
	// Update is called once per frame
    void Update()
    {
        
    }
}
