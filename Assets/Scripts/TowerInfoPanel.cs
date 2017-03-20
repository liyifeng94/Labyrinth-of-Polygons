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

    /*private TankTower _tankTower;
    private RangeTower _rangeTower;
    private SlowTower _slowTower;
    private HealTower _healTower;
    private GoldTower _goldTower;
    private Tower _tower;
    private LevelManager _levelManager;*/

    void Awake()
    {
        Instance = this;
    }


    void Start () {
        ThisPanel.SetActive(false);
        //_levelManager = GameManager.Instance.CurrentLevelManager;
    }


    /*void LateUpdate()
    {
        if (GameBoard.GamePhase.BuildingPhase == _levelManager.CurrentGamePhase()) { return; }
        DisplayTowerInfo();
    }*/


    public void Appear()
    {
        ThisPanel.SetActive(true);
    }


    public void DisAppear()
    {
        ThisPanel.SetActive(false);
    }


    /*public void SetTower(TankTower tower, Tower.TowerType type)
    {
        if (Tower.TowerType.Tank == type)
        {
            _tankTower = tower;
        }
    }


    public void DisplayTowerInfo()
    {
        Debug.Log("TIP: Display tower info called");
        int[] info = new int[11];
        _tankTower.GetTowerInfo(info);
        SetTowerInfo(info);
    }*/


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
            case 3:
                Type.text = "Heal";
                Atk.text = "HAmt\n" + info[5];
                Aspd.text = "HSpd\n" + info[6] + "/s";
                break;
            case 4:
                Type.text = "Gold";
                Atk.text = "GoldG\n" + info[5];
                Aspd.text = "GSpd\n0." + info[6] + "/s";
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
