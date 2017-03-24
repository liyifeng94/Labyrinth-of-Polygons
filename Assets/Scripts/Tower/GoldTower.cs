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
        Reloading = false;

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
            if (!Reloading)
            {
                _gameBoard.HighlightTileAt(X, Y, new Color(1, 1, 0, 1));
                MoneyGain((float)(GoldPerMinute) / 60);
                FireSoundSource.Play();
                EndTime = Time.time + ReloadTime;
                Reloading = true;
            }
            else
            {
                if (EndTime - Time.time < 0) Reloading = false;
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
        info[5] = GoldPerMinute;
        info[6] = ReloadTime;
        info[7] = UpgradeCost;
        info[8] = RepairCost;
        info[9] = SellGain;
        info[10] = BuildCost;
        return info[0];
    }


    public new void GetTowerUpgradedInfo(int[] info)
    {
        info[0] = AttackRange;
        info[1] = (int)Type;
        info[2] = CurrentLevel + 1;
        info[3] = CurrentHp;
        info[4] = HitPoint / CurrentLevel * (CurrentLevel + 1);
        info[5] = GoldPerMinute / CurrentLevel * (CurrentLevel + 1);
        info[6] = ReloadTime;
        info[7] = UpgradeCost / CurrentLevel * (CurrentLevel + 1);
        info[8] = RepairCost / CurrentLevel * (CurrentLevel + 1);
        info[9] = SellGain / CurrentLevel * (CurrentLevel + 1);
        info[10] = BuildCost;
    }
}
