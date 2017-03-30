using System.Collections.Generic;
using System.Linq;
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
        var query = highScoreList.OrderBy(w => w.Score).ToList();

        for (int i = highScoreList.Count - 1; i >= 0; i--)
	    {

            GameObject Child2 = Instantiate(ChildPanel);
            Child2.transform.SetParent(ContentPanel.transform, false);
            
            var childlist = Child2.GetComponentsInChildren<Text>();

            childlist[0].text = query[i].Name;
            childlist[1].text = query[i].DateTime.Substring(0, 10);
            childlist[2].text = query[i].Score.ToString();
        }

        ChildPanel.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
