using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class GoldTower : Tower
{

    private bool _transfered;


    void Awake()
    {
        CurrentHp = HitPoint;
        CurrentLevel = 1;
        CurrentValue = BuildCost;
        GlodPerRound = (int)(CurrentValue * 0.3);
        UpgradeCost = (int)(CurrentValue * 0.8);
        RepairCost = (int)(CurrentValue * 0.3);
        SellGain = (int)(CurrentValue * 0.4 * CurrentHp / HitPoint);
    }


    void Start()
    {
        DestroyByEnemy = false;
        StartTime = Time.time;
        _transfered = true;
        Reloading = false;

        LevelManager = GameManager.Instance.CurrentLevelManager;
        NotificationPanel = NotificationPanel.Instance;
        TowerController = TowerController.Instance;
        TowerAnimator = GetComponent<Animator>();
    }


    void LateUpdate()
    {
        if (DestroyByEnemy) return;
        if (LevelManager.CurrentGamePhase() == GameBoard.GamePhase.BattlePhase)
        {
            //_gameBoard.ClearHighlightTileAt(X, Y);
            _transfered = false;
        }

        // after the first round, it starts transfering the money beforing entering the further rounds
        if (LevelManager.CurrentGamePhase() == GameBoard.GamePhase.BuildingPhase && !_transfered)
        {
            LevelManager.AddGold((int)GlodPerRound);
            _transfered = true;
        }
    }


    public new int GetTowerInfo(int[] info)
    {
        info[0] = AttackRange;
        info[1] = (int)Type;
        info[2] = CurrentLevel;
        info[3] = CurrentHp;
        info[4] = HitPoint;
        info[5] = GlodPerRound;
        info[6] = ReloadTime;
        info[7] = UpgradeCost;
        info[8] = RepairCost;
        info[9] = SellGain;
        info[10] = BuildCost;
        return info[0];
    }


    public new void GetTowerUpgradedInfo(int[] info)
    {
        int upgratedCurrentValue = CurrentValue + UpgradeCost;
        info[0] = AttackRange;
        info[1] = (int)Type;
        info[2] = CurrentLevel + 1;
        info[3] = CurrentHp + (int)(HitPoint * 0.1);
        info[4] = (int)(HitPoint * 1.1);
        info[5] = (int)(upgratedCurrentValue * 0.3);
        info[6] = ReloadTime;
        info[7] = (int)(upgratedCurrentValue * 0.8);
        info[8] = (int)(upgratedCurrentValue * 0.3);
        info[9] = (int)(upgratedCurrentValue * 0.4 * CurrentHp / HitPoint);
        info[10] = BuildCost;
    }
}
