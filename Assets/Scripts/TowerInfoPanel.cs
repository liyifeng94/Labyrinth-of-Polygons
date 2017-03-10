using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class TowerInfoPanel : MonoBehaviour {

    public static TowerInfoPanel Instance;
    public GameObject ThisPanel;
    public Text Level;

    void Awake()
    {
        Instance = this;
    }

    // Use this for initialization
    void Start () {
	    ThisPanel.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Appear()
    {
        ThisPanel.SetActive(true);
    }

    public void DisAppear()
    {
        ThisPanel.SetActive(false);
    }

    public void SetTowerInfo(int[] info)
    {
        Debug.Log("Tower Level is " + info[1]+1);
        Level.text = "Lvl: " + info[1];
    }
}
