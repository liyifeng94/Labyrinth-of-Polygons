﻿using UnityEngine;
using UnityEngine.UI;
public class TowerInfoPanel : MonoBehaviour {

    public static TowerInfoPanel Instance;
    public GameObject ThisPanel;
    public Text Type;
    public Text Level;
    public Text Hp;
    public Text DpmText;
    public Text DpmNum;
    public Text RTime;
    public Text RTimeNum;
    public Text UCost;
    public Text RCost;
    public Text SGain;
    public Text BCost;

    private int[] _upgradeTowerInfo;
    private int[] _towerInfo;
    private bool _displayUpgradedInfo;
    private bool _repairCase;
    private bool _set;

    private Tower.TowerType _type;
    private TankTower _tankTower;
    private RangeTower _rangeTower;
    private SlowTower _slowTower;
    private HealTower _healTower;
    private GoldTower _goldTower;
    private LevelManager _levelManager;
    private RepairButton _repairButton;

    void Awake()
    {
        Instance = this;
    }


    void Start () {
        ThisPanel.SetActive(false);
        _upgradeTowerInfo = new int[11];
        _towerInfo = new int[11];
        _levelManager = GameManager.Instance.CurrentLevelManager;
        _repairButton = RepairButton.Instance;
        _displayUpgradedInfo = false;
        _repairCase = false;
        _set = false;
    }


    void LateUpdate()
    {
        // record a current selected tower, if no tower is selected at the beginning, _type is null
        if (!_set) return;
        int[] info = new int[11];
        int[] upgratedInfo = new int[11];
        switch (_type)
        {
            case Tower.TowerType.Tank:
                _tankTower.GetTowerInfo(info);
                _tankTower.GetTowerUpgradedInfo(upgratedInfo);
                break;
            case Tower.TowerType.Range:
                _rangeTower.GetTowerInfo(info);
                _rangeTower.GetTowerUpgradedInfo(upgratedInfo);
                break;
            case Tower.TowerType.Slow:
                _slowTower.GetTowerInfo(info);
                _slowTower.GetTowerUpgradedInfo(upgratedInfo);
                break;
            case Tower.TowerType.Heal:
                _healTower.GetTowerInfo(info);
                _healTower.GetTowerUpgradedInfo(upgratedInfo);
                break;
            case Tower.TowerType.Gold:
                _goldTower.GetTowerInfo(info);
                _goldTower.GetTowerUpgradedInfo(upgratedInfo);
                break;
        }
        // dynamically update current tower hp
        UpdateTowerInfo(info, upgratedInfo);
        if (info[3] == info[4])
        {
            _repairButton.SetHpCheckFlag();
        }
        else
        {
            _repairButton.ResetHpCheckFlag();
        }
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
        _set = true;
    }


    public void SetRangeTower(RangeTower tower)
    {
        _type = Tower.TowerType.Range;
        _rangeTower = tower;
        _set = true;
    }


    public void SetSlowTower(SlowTower tower)
    {
        _type = Tower.TowerType.Slow;
        _slowTower = tower;
        _set = true;
    }


    public void SetHealTower(HealTower tower)
    {
        _type = Tower.TowerType.Heal;
        _healTower = tower;
        _set = true;
    }


    public void SetGoldTower(GoldTower tower)
    {
        _type = Tower.TowerType.Gold;
        _goldTower = tower;
        _set = true;
    }


    // when click on an existing tower, store this tower info and display the info
    public void SetTowerInfo(int[] info)
    {
        _repairCase = false;
        _displayUpgradedInfo = false;
        _towerInfo = info;
        Tower.TowerType towerType = (Tower.TowerType)info[1];
        switch (towerType)
        {
            case Tower.TowerType.Tank:
                Type.text = "Tank";
                DpmText.text = "Atk";
                DpmNum.text = "" + info[5];
                RTime.text = "Reload";
                RTimeNum.text = "" + info[6];
                break;
            case Tower.TowerType.Range:
                Type.text = "Range";
                DpmText.text = "Atk";
                DpmNum.text = "" + info[5];
                RTime.text = "Reload";
                RTimeNum.text = "" + info[6];
                break;
            case Tower.TowerType.Slow:
                Type.text = "Slow";
                DpmText.text = "Atk";
                DpmNum.text = "" + info[5];
                RTime.text = "Reload";
                RTimeNum.text = "" + info[6];
                break;
            case Tower.TowerType.Heal:
                Type.text = "Heal";
                DpmText.text = "Heal";
                DpmNum.text = "" + info[5];
                RTime.text = "Reload";
                RTimeNum.text = "" + info[6];
                break;
            case Tower.TowerType.Gold:
                Type.text = "Gold";
                DpmText.text = "Gold";
                DpmNum.text = "" + info[5];
                RTime.text = "Reload";
                RTimeNum.text = "N/A";
                break;
        }
        Level.text = "" + info[2];
        Hp.text = info[3] + "\n/" + info[4];
        UCost.text = info[7] + "G";
        RCost.text = info[8] + "G";
        SGain.text = info[9] + "G";
        BCost.text = info[10] + "G";
    }


    // record a updated tower info
    public void SetUpgradedTowerInfo(int[] info)
    {
        _upgradeTowerInfo = info;
    }


    // when upgrade button is clicked, this function would be called to show updated info
    public void DisplayUpgradedInfo()
    {
        _repairCase = false;
        _displayUpgradedInfo = true;
        Tower.TowerType towerType = (Tower.TowerType)_upgradeTowerInfo[1];
        switch (towerType)
        {
            case Tower.TowerType.Tank:
                DpmText.text = "Atk";
                DpmNum.text = "" + _upgradeTowerInfo[5];
                break;
            case Tower.TowerType.Range:
                DpmText.text = "Atk";
                DpmNum.text = "" + _upgradeTowerInfo[5];
                break;
            case Tower.TowerType.Slow:
                DpmText.text = "Atk";
                DpmNum.text = "" + _upgradeTowerInfo[5];
                break;
            case Tower.TowerType.Heal:
                DpmText.text = "Heal";
                DpmNum.text = "" + _upgradeTowerInfo[5];
                break;
            case Tower.TowerType.Gold:
                DpmText.text = "Gold";
                DpmNum.text = "" + _upgradeTowerInfo[5];
                break;
        }
        Level.text = "" + _upgradeTowerInfo[2];
        //Debug.Log("HP: "+ _upgradeTowerInfo[3]+" "+ _upgradeTowerInfo[4]);
        Hp.text = _upgradeTowerInfo[3] + "\n/" + _upgradeTowerInfo[4];
        UCost.text = _upgradeTowerInfo[7] + "G";
        RCost.text = _upgradeTowerInfo[8] + "G";
        SGain.text = _upgradeTowerInfo[9] + "G";
    }


    // when repair button and sell button are clicked, this function would be 
    // called  in case upgrade button early modify the displayed tower info 
    public void SetOriginalowerInfo()
    {
        _displayUpgradedInfo = false;
        Tower.TowerType towerType = (Tower.TowerType)_towerInfo[1];
        switch (towerType)
        {
            case Tower.TowerType.Tank:
                DpmText.text = "Atk";
                DpmNum.text = "" + _towerInfo[5];
                break;
            case Tower.TowerType.Range:
                DpmText.text = "Atk";
                DpmNum.text = "" + _towerInfo[5];
                break;
            case Tower.TowerType.Slow:
                DpmText.text = "Atk";
                DpmNum.text = "" + _towerInfo[5];
                break;
            case Tower.TowerType.Heal:
                DpmText.text = "Heal";
                DpmNum.text = "" + _towerInfo[5];
                break;
            case Tower.TowerType.Gold:
                DpmText.text = "Gold";
                DpmNum.text = "" + _towerInfo[5];
                break;
        }
        Level.text = "" + _towerInfo[2];
        Hp.text = _towerInfo[3]  + "\n/" + _towerInfo[4];
        UCost.text =  _towerInfo[7] + "G";
        RCost.text = _towerInfo[8] + "G";
        SGain.text = _towerInfo[9] + "G";
    }


    void UpdateTowerInfo(int[] info, int[] updatedInfo)
    {
        if (_displayUpgradedInfo) // if upgrade button is clicked in battle phace
        {
            Level.text = updatedInfo[2] + "";
            int updatedCurHp = info[3] + updatedInfo[4] - info[4];
            Hp.text = updatedCurHp + "\n/" + updatedInfo[4];
            DpmNum.text = "" + updatedInfo[5];
            UCost.text = "" + updatedInfo[7];
            RCost.text = updatedInfo[8] + "G";
            SGain.text = updatedInfo[9] + "G";
        }
        else if (_repairCase) // if repair button is clicked in battle phace
        {
            Hp.text = "<color=#00ff00ff>" + info[4] + "\n</color>/" + info[4];
            if (GameBoard.GamePhase.BuildingPhase == _levelManager.CurrentGamePhase())
            {
                RCost.text = 0 + "G"; 
            }
        }
        else // sell button or no button is clicked in battoe phace
        {
            Hp.text = info[3] + "\n/" + info[4];
            SGain.text = info[9] + "G";
            RCost.text = info[8] + "G";
        }
    }


    // change upgrading tower info color to green
    public void SetUpgradingColor()
    {
        Level.color = new Color(0, 1, 0, 1);
        DpmNum.color = new Color(0, 1, 0, 1);
        Hp.color = new Color(0, 1, 0, 1);
        UCost.color = new Color(1, 0, 0, 1);
        RCost.color = new Color(1, 0, 0, 1);
        SGain.color = new Color(0, 1, 0, 1);
    }


    // change back all tower info color to white
    public void ResetTextColor()
    {
        Level.color = new Color(1, 1, 1, 1);
        DpmNum.color = new Color(1, 1, 1, 1);
        Hp.color = new Color(1, 1, 1, 1);
        UCost.color = new Color(1, 1, 1, 1);
        RCost.color = new Color(1, 1, 1, 1);
        SGain.color = new Color(1, 1, 1, 1);
    }


    public void RepairCase()
    {
        _repairCase = true;
    }


    // used for sellbutton when previous clicked button is repair button
    public void ResetRepairCase()
    {
        _repairCase = false;
    }
}
