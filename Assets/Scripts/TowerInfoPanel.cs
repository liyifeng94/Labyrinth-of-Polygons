using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class TowerInfoPanel : MonoBehaviour {

    public static TowerInfoPanel Instance;
    public GameObject ThisPanel;
    public Text Type;
    public Text Level;

    void Awake()
    {
        Instance = this;
    }


    void Start () {
	    ThisPanel.SetActive(false);
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
        //Debug.Log("Tower Level is " + info[1]+1);
        switch (info[1])
        {
            case 0:
                Type.text = "Tank";
                break;
        }
        Level.text = "Lvl: " + (info[2]+1);
        // TODO: display more text about tower info
    }
}
