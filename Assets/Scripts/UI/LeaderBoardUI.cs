using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderBoardUI : MonoBehaviour
{

    public Text NameResultText;
    public Text TimeResultText;
    public Text ScoreResultText;
    public GameObject ContentPanel;
    public GameObject ChildPanel;

	// Use this for initialization
	void Start ()
	{
	    NameResultText.text = "";
	    TimeResultText.text = "";
	    ScoreResultText.text = "";
        
        HighScoreBoard temp = GameManager.Instance.LocalHighScoreBoard;
	    List<HighScoreBoard.HighScoreEntry> highScoreList = temp.ScoreList.MainList;
	    for(int i = 0; i < highScoreList.Count; i++)
	    {

            GameObject Child2 = Instantiate(ChildPanel);
            Child2.transform.SetParent(ContentPanel.transform, false);

            Text nameresult = Instantiate(NameResultText);
            nameresult.transform.SetParent(Child2.transform, false);
            nameresult.fontSize = 1;
            nameresult.GetComponent<Text>().text = highScoreList[i].Name;

            Text timeresult = Instantiate(TimeResultText) as Text;
            timeresult.transform.SetParent(Child2.transform, false);
            timeresult.fontSize = 1;
            timeresult.GetComponent<Text>().text = highScoreList[i].DateTime;

            Text scoreresult = Instantiate(TimeResultText) as Text;
            scoreresult.transform.SetParent(Child2.transform, false);
            scoreresult.fontSize = 1;
            scoreresult.GetComponent<Text>().text = highScoreList[i].Score.ToString();
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
