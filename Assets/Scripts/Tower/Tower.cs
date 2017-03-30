using UnityEngine;

public abstract class Tower : MonoBehaviour
 {

    [HideInInspector] public int X;// { get; private set; }
    [HideInInspector] public int Y;// { get; private set; }
    [HideInInspector] public int CurrentHp;
    [HideInInspector] public int CurrentLevel;
    [HideInInspector] public bool DestroyByEnemy;
    [HideInInspector] public float StartTime, EndTime;
    [HideInInspector] public bool Reloading;
    [HideInInspector] public LevelManager LevelManager;
    [HideInInspector] public TowerController TowerController;
    [HideInInspector] public NotificationPanel NotificationPanel;
    [HideInInspector] public Animator TowerAnimator;

    public int HitPoint;
    public int AttackRange;
    public int ReloadTime;
    public int BuildCost;
    public int AttackDamage;
    [HideInInspector] public int UpgradeCost;
    [HideInInspector] public int CurrentValue;


    [HideInInspector] public int RepairCost;
    [HideInInspector] public int SellGain;

    [HideInInspector] public const float TankTowerHpFactor = 1.3f;
    [HideInInspector] public const float RangeTowerHpFactor = 1.1f;
    [HideInInspector] public const float SlowTowerHpFactor = 1.2f;
    [HideInInspector] public const float HealTowerHpFactor = 1.1f;
    [HideInInspector] public const float GoldTowerHpFactor = 1.1f;

    [HideInInspector] public const int TankTowerAtkIncrement = 1;
    [HideInInspector] public const int RangeTowerAtkIncrement = 4;
    [HideInInspector] public const int SlowTowerAtkIncrement = 1;
    [HideInInspector] public const float HealTowerHealFactor = 0.2f;
    [HideInInspector] public const float GoldTowerGoldFactor= 0.15f;

    [HideInInspector] public const float UpgradeFactor = 0.5f;
    [HideInInspector] public const float RepairFactor = 0.2f;
    [HideInInspector] public const float SellFactor = 0.4f;

    public enum TowerType { Tank = 1, Range = 3, Slow = 4, Heal = 2, Gold = 0 }
    public TowerType Type;



    public virtual void AddEnemy(Enemy t) { /*Debug.Log("T: Enemy added");*/ }
    public virtual void GetTowerUpgradedInfo(int[] info) { }
    public virtual void UpdateSellGain() { }

    private AudioSource[] _sounds;
    [HideInInspector] public AudioSource FireSoundSource;
    private AudioSource _destroySoundSource;


    public bool IsDestory()
    {
        return DestroyByEnemy;
    }


    public void Setup(TileEventHandler tileEventHandler)
    {
        X = tileEventHandler.GridX;
        Y = tileEventHandler.GridY;
        _sounds = GetComponents<AudioSource>();
        FireSoundSource = _sounds[0];
        _destroySoundSource = _sounds[1];
    }


    public void Remove()
    {
        Destroy(gameObject);
    }


    public void ReceiveAttack(int ad)
    {
        if (DestroyByEnemy) return;
        
        if (CurrentHp > ad)
        {
            TowerAnimator.SetTrigger("TowerDamaged");
            CurrentHp -= ad;
            RepairCost = (int)(CurrentValue * 0.2 * (1 - 1.0 * CurrentHp / HitPoint));
            SellGain = (int)(CurrentValue * 0.4 * CurrentHp / HitPoint);
        }
        else
        {
            CurrentHp = 0;
            RepairCost = (int) (CurrentValue * 0.2);
            SellGain = 0;
            TowerAnimator.SetTrigger("TowerDestroyed");
            _destroySoundSource.Play();
            DestroyByEnemy = true;
        }
    }


    public void ReceiveHeal(int heal)
    {
        if (CurrentHp + heal < HitPoint)
        {
            CurrentHp += heal;
        }
        else
        {
            CurrentHp = HitPoint;
            TowerAnimator.SetTrigger("TowerFixed");
        }
    }


    public void Upgrade()
    {
        LevelManager.UseGold(UpgradeCost);
        CurrentLevel += 1;
        CurrentValue += UpgradeCost;
        UpgradeCost = (int)(CurrentValue * UpgradeFactor);
        RepairCost = (int)(CurrentValue * RepairFactor * CurrentHp / HitPoint);
        SellGain = (int)(CurrentValue * SellFactor * CurrentHp / HitPoint);
        switch (Type)
        {
            case TowerType.Tank:
                CurrentHp += (int)(HitPoint * (1.0 * TankTowerHpFactor - 1));
                HitPoint = (int)(HitPoint * TankTowerHpFactor);
                AttackDamage += TankTowerAtkIncrement;
                break;
            case TowerType.Range:
                CurrentHp += (int)(HitPoint * (1.0 * RangeTowerHpFactor - 1));
                HitPoint = (int)(HitPoint * RangeTowerHpFactor);
                AttackDamage += RangeTowerAtkIncrement;
                break;
            case TowerType.Slow:
                CurrentHp += (int)(HitPoint * (1.0 * SlowTowerHpFactor - 1));
                HitPoint = (int)(HitPoint * SlowTowerHpFactor);
                AttackDamage += SlowTowerAtkIncrement;
                break;
            case TowerType.Heal:
                CurrentHp += (int)(HitPoint * (1.0 * HealTowerHpFactor - 1));
                HitPoint = (int)(HitPoint * HealTowerHpFactor);
                AttackDamage = (int)(HitPoint * HealTowerHealFactor);
                break;
            case TowerType.Gold:
                CurrentHp += (int)(HitPoint * (1.0 * GoldTowerHpFactor - 1));
                HitPoint = (int)(HitPoint * GoldTowerHpFactor);
                AttackDamage = (int)(CurrentValue * GoldTowerGoldFactor);
                break;
        }
    }


    public void Repair()
    {
        if (CurrentHp == HitPoint)
        {
            NotificationPanel.SetNotificationType("RepairWithFullHp");
            NotificationPanel.Appear();
            return;
        }
        CurrentHp = HitPoint;
        if (LevelManager.CurrentGamePhase() == GameBoard.GamePhase.BattlePhase) LevelManager.UseGold(RepairCost);
        RepairCost = (int)(CurrentValue * RepairFactor * CurrentHp / HitPoint);
        if (DestroyByEnemy)
        {
            DestroyByEnemy = false;
        }
        TowerAnimator.SetTrigger("TowerFixed");
    }


    public int GetLevel()
    {
        return CurrentLevel;
    }


    public bool IsFullHealth()
    {
        return CurrentHp == HitPoint;
    }


    public int GetTowerInfo(int[] info)
    {
        info[0] = AttackRange;
        info[1] = (int)Type;
        info[2] = CurrentLevel;
        info[3] = CurrentHp;
        info[4] = HitPoint;
        info[5] = AttackDamage;
        info[6] = ReloadTime;
        info[7] = UpgradeCost;
        info[8] = RepairCost;
        info[9] = SellGain;
        info[10] = BuildCost;
        return info[0];
    }
}
