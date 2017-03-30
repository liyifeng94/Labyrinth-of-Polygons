using UnityEngine;

public class GoldTower : Tower
{

    private bool _transfered;

    void Awake()
    {
        CurrentHp = HitPoint;
        CurrentLevel = 1;
        CurrentValue = BuildCost;
        AttackDamage = (int)(CurrentValue * 0.15);
        UpgradeCost = (int)(CurrentValue * UpgradeFactor);
        RepairCost = (int)(CurrentValue * RepairFactor * (1 - 1.0 * CurrentHp / HitPoint));
        SellGain = (int)(CurrentValue * SellFactor * CurrentHp / HitPoint);
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
        if (DestroyByEnemy)
        {
            _transfered = true;
            return;
        }
        if (LevelManager.CurrentGamePhase() == GameBoard.GamePhase.BattlePhase)
        {
            _transfered = false;
        }

        // after the first round, it starts transfering the money beforing entering the further rounds
        if (LevelManager.CurrentGamePhase() == GameBoard.GamePhase.BuildingPhase && !_transfered)
        {
            LevelManager.AddGold((int)AttackDamage);
            _transfered = true;
        }
    }


    public new void GetTowerUpgradedInfo(int[] info)
    {
        int upgratedCurrentValue = CurrentValue + UpgradeCost;
        info[0] = AttackRange;
        info[1] = (int)Type;
        info[2] = CurrentLevel + 1;
        info[3] = CurrentHp + (int)(HitPoint * (1.0 * GoldTowerHpFactor - 1));
        info[4] = (int)(HitPoint * GoldTowerHpFactor);
        info[5] = (int)(upgratedCurrentValue * GoldTowerGoldFactor);
        info[6] = ReloadTime;
        info[7] = (int)(upgratedCurrentValue * UpgradeFactor);
        info[8] = (int)(upgratedCurrentValue * RepairFactor * (1 - 1.0 * info[3] / info[4]));
        info[9] = (int)(upgratedCurrentValue * SellFactor * CurrentHp / HitPoint);
        info[10] = BuildCost;
    }
}
