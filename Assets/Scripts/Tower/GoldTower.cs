using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class GoldTower : Tower
{

    private float _currentReservedMoney;
    private bool _reservedMoneyTransfered;
    private GameBoard _gameBoard;
    //private GameBoard _gameBoard;


    void Start()
    {
        CurrentLevel = 1;
        DestroyByEnemy = false;
        StartTime = Time.time;
        CurrentHp = HitPoint;
        _currentReservedMoney = 0;
        _reservedMoneyTransfered = true;

        LevelManager = GameManager.Instance.CurrentLevelManager;
        _gameBoard = LevelManager.GameBoardSystem;
        NotificationPanel = NotificationPanel.Instance;
        TowerController = TowerController.Instance;
        //_gameBoard = LevelManager.GameBoardSystem;
        TowerAnimator = GetComponent<Animator>();
    }


    void LateUpdate()
    {
        if (DestroyByEnemy) return;
        if (LevelManager.CurrentGamePhase() == GameBoard.GamePhase.BattlePhase)
        {
            _gameBoard.ClearHighlightTileAt(X, Y);
            EndTime = Time.time;
            if (EndTime - StartTime > (float)(1 / AttackSpeed))
            {
                StartTime = Time.time;
                _gameBoard.HighlightTileAt(X, Y, new Color(1, 1, 0, 1));
                MoneyGain((float)(GoldPerTenSec) /10);
            }
            _reservedMoneyTransfered = false;
        }

        // after the first round, it starts transfering the money beforing entering the further rounds
        if (LevelManager.CurrentGamePhase() == GameBoard.GamePhase.BuildingPhase && !_reservedMoneyTransfered)
        {
            LevelManager.AddGold((int)_currentReservedMoney);
            Debug.Log("MT: " + (int)_currentReservedMoney + " gold gained this round");
            _currentReservedMoney = 0;
            _reservedMoneyTransfered = true;
        }
    }


    public void MoneyGain(float money)
    {
        _currentReservedMoney += money;
        //Debug.Log("MT: Total reserved money is " + _currentReservedMoney);
    }


    public new int GetTowerInfo(int[] info)
    {
        info[0] = AttackRange;
        info[1] = (int)Type;
        info[2] = CurrentLevel;
        info[3] = CurrentHp;
        info[4] = HitPoint;
        info[5] = GoldPerTenSec;
        info[6] = AttackSpeed;
        info[7] = UpgradeCost;
        info[8] = RepairCost;
        info[9] = SellGain;
        info[10] = BuildCost;
        return info[0];
    }
}
