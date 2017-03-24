using UnityEngine;
using UnityEngine.UI;
public class TowerInfoPanel : MonoBehaviour {

    public static TowerInfoPanel Instance;
    public GameObject ThisPanel;
    public Text Type;
    public Text Level;
    //public Text CurHp;
    public Text MaxHp;
    public Text AtkText;
    public Text AtkNum;
    public Text Aspd;
    public Text AspdNum;
    public Text UCost;
    public Text RCost;
    public Text SGain;
    public Text BCost;

    private int[] _upgradeTowerInfo;
    private int[] _towerInfo;
    private bool _displayUpgradedInfo;
    private int _upgradedHp;
    private bool _requirCase;

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
        _displayUpgradedInfo = false;
        _requirCase = false;
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
        UpdateTowerCurrentHp(info[3], _towerInfo[4], info[2]);
        //Debug.Log("info: " + info[3] + info[4]);
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
        _requirCase = false;
        _displayUpgradedInfo = false;
        _towerInfo = info;
        //Debug.Log("Tower Level is " + info[1]+1);
        switch (info[1])
        {
            case 0:
                Type.text = "Tank";
                AtkText.text = "ATK";
                AtkNum.text = "" + info[5];
                Aspd.text = "ASpd";
                AspdNum.text = "" + info[6] + "/s";
                break;
            case 3:
                Type.text = "Range";
                AtkText.text = "ATK";
                AtkNum.text = "" + info[5];
                Aspd.text = "ASpd";
                AspdNum.text = "" + info[6] + "/s";
                break;
            case 4:
                Type.text = "Slow";
                AtkText.text = "Slow";
                AtkNum.text = "" + info[5] + "%";
                Aspd.text = "ASpd";
                AspdNum.text = "" + info[6] + "/s";
                break;
            case 1:
                Type.text = "Heal";
                AtkText.text = "Heal";
                AtkNum.text = "" + info[5];
                Aspd.text = "HSpd";
                AspdNum.text = "" + info[6] + "/s";
                break;
            case 2:
                Type.text = "Gold";
                AtkText.text = "Gold";
                AtkNum.text = "" + info[5];
                Aspd.text = "GSpd";
                AspdNum.text = "" + info[6] + "/s";
                break;
                // TODO~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        }
        Level.text = "" + info[2];
        //CurHp.text = "" + info[3];
        MaxHp.text = info[3] + "/" + info[4];
        UCost.text = "" + info[7] + "G";
        RCost.text = "" + info[8] + "G";
        SGain.text = "" + info[9] + "G";
        BCost.text = "" + info[10] + "G";
        _upgradedHp = _towerInfo[4];
    }


    public void SetUpgradedTowerInfo(int[] info)
    {
        _upgradeTowerInfo = info;
        //Debug.Log("TIP: New tower curHP is  " + info[3]);
    }


    public void DisplayUpgradedInfo()
    {
        _requirCase = false;
        //Debug.Log("TIP: New tower curHP is  " + _upgradeTowerInfo[3]);
        _displayUpgradedInfo = true;
        switch (_upgradeTowerInfo[1])
        {
            case 0:
                AtkText.text = "ATK";
                AtkNum.text = "" + _upgradeTowerInfo[5];
                break;
            case 3:
                AtkText.text = "ATK";
                AtkNum.text = "" + _upgradeTowerInfo[5];
                break;
            case 4:
                AtkText.text = "Slow";
                AtkNum.text = "" + _upgradeTowerInfo[5] + "%";
                break;
            case 1:
                AtkText.text = "Heal";
                AtkNum.text = "" + _upgradeTowerInfo[5];
                break;
            case 2:
                AtkText.text = "Gold";
                AtkNum.text = "" + _upgradeTowerInfo[5];
                break;
                // TODO~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        }
        Level.text = "" + _upgradeTowerInfo[2];
        int updatedCurHp = _upgradeTowerInfo[3] + _upgradeTowerInfo[4] / _upgradeTowerInfo[2] * (_upgradeTowerInfo[2] - 1);
        MaxHp.text = updatedCurHp + "/" + _upgradeTowerInfo[4];
        //MaxHp.text = _upgradeTowerInfo[3] + "/" + _upgradeTowerInfo[4];
        UCost.text = "" + _upgradeTowerInfo[7] + "G";
        RCost.text = "" + _upgradeTowerInfo[8] + "G";
        SGain.text = "" + _upgradeTowerInfo[9] + "G";
        _upgradedHp = _upgradeTowerInfo[4];
    }


    public void SetOriginalowerInfo()
    {
        _displayUpgradedInfo = false;
        switch (_towerInfo[1])
        {
            case 0:
                AtkText.text = "ATK";
                AtkNum.text = "" + _towerInfo[5];
                break;
            case 3:
                AtkText.text = "ATK";
                AtkNum.text = "" + _towerInfo[5];
                break;
            case 4:
                AtkText.text = "Slow";
                AtkNum.text = "" + _towerInfo[5] + "%";
                break;
            case 1:
                AtkText.text = "Heal";
                AtkNum.text = "" + _towerInfo[5];
                break;
            case 2:
                AtkText.text = "Gold";
                AtkNum.text = "" + _towerInfo[5];
                break;
                // TODO~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        }
        Level.text = "" + _towerInfo[2];
        MaxHp.text = _towerInfo[3]  + "/" + _towerInfo[4];
        UCost.text = "" + _towerInfo[7] + "G";
        RCost.text = "" + _towerInfo[8] + "G";
        SGain.text = "" + _towerInfo[9] + "G";
    }


    public void UpdateTowerCurrentHp(int curHp, int maxHp, int level)
    {
        //Debug.Log("difference is " + curHp + " " + maxHp + " " + level);
        //CurHp.text = curHp + "";
        if (_displayUpgradedInfo)
        {
            int updatedCurHp;
            if (1 == level)
            {
                updatedCurHp = curHp + maxHp;
            }
            else
            {
                updatedCurHp = curHp + maxHp / level * (level - 1);
            }
            MaxHp.text = updatedCurHp + "/" + _upgradedHp;
        }
        else if (_requirCase)
        {
            //Debug.Log("repair case");
            MaxHp.text = "<color=#00ff00ff>" + curHp + "</color>/" + _towerInfo[4];
        }
        else
        {
            MaxHp.text = curHp + "/" + _towerInfo[4];
        }
    }

    public void SetUpgradingColor()
    {
        Level.color = new Color(0, 1, 0, 1);
        AtkNum.color = new Color(0, 1, 0, 1);
        //CurHp.color = new Color(0, 1, 0, 1);
        MaxHp.color = new Color(0, 1, 0, 1);
        UCost.color = new Color(1, 0, 0, 1);
        RCost.color = new Color(1, 0, 0, 1);
        SGain.color = new Color(0, 1, 0, 1);
    }


    public void ResetTextColor()
    {
        Level.color = new Color(1, 1, 1, 1);
        //CurHp.color = new Color(1, 1, 1, 1);
        AtkNum.color = new Color(1, 1, 1, 1);
        MaxHp.color = new Color(1, 1, 1, 1);
        UCost.color = new Color(1, 1, 1, 1);
        RCost.color = new Color(1, 1, 1, 1);
        SGain.color = new Color(1, 1, 1, 1);
    }


    public void RequireCase()
    {
        _requirCase = true;
    }
}
