using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class TowerInfoPanel : MonoBehaviour {

    public static TowerInfoPanel Instance;
    public GameObject ThisPanel;
    public Text Type;
    public Text Level;
    public Text CurHp;
    public Text MaxHp;
    public Text Atk;
    public Text Aspd;
    public Text UCost;
    public Text RCost;
    public Text SGain;
    public Text BCost;

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
                Atk.text = "ATK\n" + info[5];
                Aspd.text = "ASpd\n" + info[6] + "/s";
                break;
            case 1:
                Type.text = "Range";
                Atk.text = "ATK\n" + info[5];
                Aspd.text = "ASpd\n" + info[6] + "/s";
                break;
            case 2:
                Type.text = "Slow";
                Atk.text = "SRate\n" + info[5] + "%";
                Aspd.text = "ASpd\n" + info[6] + "/s";
                break;
            case 4:
                Type.text = "Money";
                float money = info[5];
                Atk.text = "Money\n" + money;
                Aspd.text = "MSpd\n0." + info[6] + "/s";
                break;
                // TODO~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        }
        Level.text = "Lvl: " + (info[2]+1);
        CurHp.text = "CurHP\n" + info[3];
        MaxHp.text = "MaxHP\n" + info[4];
        UCost.text = "UCost\n" + info[7] + "G";
        RCost.text = "RCost\n" + info[8] + "G";
        SGain.text = "SGain\n" + info[9] + "G";
        BCost.text = "BCost\n" + info[10] + "G";
    }
}
