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

    private int[] _upgradeTowerInfo;
    private int[] _towerInfo;

    private Tower.TowerType _type;
    private TankTower _tankTower;
    private RangeTower _rangeTower;
    private SlowTower _slowTower;
    private HealTower _healTower;
    private GoldTower _goldTower;
    private LevelManager _levelManager;

    void Awake()
    {
        Instance = this;
    }


    void Start () {
        ThisPanel.SetActive(false);
        _upgradeTowerInfo = new int[11];
        _towerInfo = new int[11];
        _levelManager = GameManager.Instance.CurrentLevelManager;
    }


    void LateUpdate()
    {
        if (GameBoard.GamePhase.BuildingPhase == _levelManager.CurrentGamePhase()) { return; }
        int[] info = new int[11];
        switch (_type)
        {
            case Tower.TowerType.Tank:
                _tankTower.GetTowerInfo(info);
                break;
            case Tower.TowerType.Range:
                _rangeTower.GetTowerInfo(info);
                break;
            case Tower.TowerType.Slow:
                _slowTower.GetTowerInfo(info);
                break;
            case Tower.TowerType.Heal:
                _healTower.GetTowerInfo(info);
                break;
            case Tower.TowerType.Gold:
                _goldTower.GetTowerInfo(info);
                break;
        }
        UpdateTowerCurrentHp(info[3]);
    }


    public void Appear()
    {
        ThisPanel.SetActive(true);
    }


    public void DisAppear()
    {
        ThisPanel.SetActive(false);
    }


    public void SetTankTower(TankTower tower)
    {
        _type = Tower.TowerType.Tank;
        _tankTower = tower;
    }


    public void SetRangeTower(RangeTower tower)
    {
        _type = Tower.TowerType.Range;
        _rangeTower = tower;
    }


    public void SetSlowTower(SlowTower tower)
    {
        _type = Tower.TowerType.Slow;
        _slowTower = tower;
    }


    public void SetHealTower(HealTower tower)
    {
        _type = Tower.TowerType.Heal;
        _healTower = tower;
    }


    public void SetGoldTower(GoldTower tower)
    {
        _type = Tower.TowerType.Gold;
        _goldTower = tower;
    }


    public void SetTowerInfo(int[] info)
    {
        _towerInfo = info;
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
        Level.text = "Lvl: " + info[2];
        CurHp.text = "CurHP\n" + info[3];
        MaxHp.text = "MaxHP\n" + info[4];
        UCost.text = "UCost\n" + info[7] + "G";
        RCost.text = "RCost\n" + info[8] + "G";
        SGain.text = "SGain\n" + info[9] + "G";
        BCost.text = "BCost\n" + info[10] + "G";
    }


    public void SetUpgradedTowerInfo(int[] info)
    {
        _upgradeTowerInfo = info;
        //Debug.Log("TIP: New tower HP is  " + info[4]);
    }


    public void DisplayUpgradedInfo()
    {
        switch (_upgradeTowerInfo[1])
        {
            case 0:
                Atk.text = "ATK\n" + _upgradeTowerInfo[5];
                break;
            case 1:
                Atk.text = "ATK\n" + _upgradeTowerInfo[5];
                break;
            case 2:
                Atk.text = "SRate\n" + _upgradeTowerInfo[5] + "%";
                break;
            case 3:
                Atk.text = "HAmt\n" + _upgradeTowerInfo[5];
                break;
            case 4:
                Atk.text = "GoldG\n" + _upgradeTowerInfo[5];
                break;
                // TODO~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        }
        Level.text = "Lvl: " + _upgradeTowerInfo[2];
        //CurHp.text = "CurHP\n" + _upgradeTowerInfo[4];
        MaxHp.text = "MaxHP\n" + _upgradeTowerInfo[4];
        UCost.text = "UCost\n" + _upgradeTowerInfo[7] + "G";
        RCost.text = "RCost\n" + _upgradeTowerInfo[8] + "G";
        SGain.text = "SGain\n" + _upgradeTowerInfo[9] + "G";
    }


    public void SetOriginalowerInfo()
    {
        switch (_towerInfo[1])
        {
            case 0:
                Atk.text = "ATK\n" + _towerInfo[5];
                break;
            case 1:
                Atk.text = "ATK\n" + _towerInfo[5];
                break;
            case 2:
                Atk.text = "SRate\n" + _towerInfo[5] + "%";
                break;
            case 3:
                Atk.text = "HAmt\n" + _towerInfo[5];
                break;
            case 4:
                Atk.text = "GoldG\n" + _towerInfo[5];
                break;
                // TODO~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        }
        Level.text = "Lvl: " + _towerInfo[2];
        CurHp.text = "CurHP\n" + _towerInfo[3];
        MaxHp.text = "MaxHP\n" + _towerInfo[4];
        UCost.text = "UCost\n" + _towerInfo[7] + "G";
        RCost.text = "RCost\n" + _towerInfo[8] + "G";
        SGain.text = "SGain\n" + _towerInfo[9] + "G";
    }


    public void UpdateTowerCurrentHp(int hp)
    {
        CurHp.text = "CurHP\n" + hp;
    }

    public void SetUpgradingColor()
    {
        Level.color = new Color(0, 1, 0, 1);
        Atk.color = new Color(0, 1, 0, 1);
        CurHp.color = new Color(0, 1, 0, 1);
        MaxHp.color = new Color(0, 1, 0, 1);
        UCost.color = new Color(1, 0, 0, 1);
        RCost.color = new Color(1, 0, 0, 1);
        SGain.color = new Color(0, 1, 0, 1);
    }


    public void ResetTextColor()
    {
        Level.color = new Color(1, 1, 1, 1);
        CurHp.color = new Color(1, 1, 1, 1);
        Atk.color = new Color(1, 1, 1, 1);
        MaxHp.color = new Color(1, 1, 1, 1);
        UCost.color = new Color(1, 1, 1, 1);
        RCost.color = new Color(1, 1, 1, 1);
        SGain.color = new Color(1, 1, 1, 1);
    }
}
