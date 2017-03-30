using UnityEngine;
using UnityEngine.UI;

using System.Collections.Generic;
using System.Linq;

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
    [HideInInspector] public const float UpgradeFactor = 0.5f;
    [HideInInspector] public int CurrentValue;


    [HideInInspector] public int RepairCost;
    [HideInInspector] public const float RepairFactor = 0.2f;
    [HideInInspector] public int SellGain;
    [HideInInspector] public const float SellFactor = 0.4f;

    public enum TowerType { Tank = 0, Range = 3, Slow = 4, Heal = 1, Gold = 2 }
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
        if (CurrentHp + heal <= HitPoint)
        {
            CurrentHp += heal;
        }
        else
        {
            CurrentHp = HitPoint;
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
                CurrentHp += (int)(HitPoint * 0.7);
                HitPoint = (int)(HitPoint * 1.7);
                AttackDamage += 2;
                break;
            case TowerType.Range:
                CurrentHp += (int)(HitPoint * 0.5);
                HitPoint = (int)(HitPoint * 1.5);
                AttackDamage += 4;
                break;
            case TowerType.Slow:
                CurrentHp += (int)(HitPoint * 0.4);
                HitPoint = (int)(HitPoint * 1.4);
                AttackDamage += 1;
                break;
            case TowerType.Heal:
                CurrentHp += (int)(HitPoint * 0.5);
                HitPoint = (int)(HitPoint * 1.5);
                AttackDamage = (int)(HitPoint * 0.1);
                break;
            case TowerType.Gold:
                CurrentHp += (int)(HitPoint * 0.4);
                HitPoint = (int)(HitPoint * 1.4);
                AttackDamage = (int)(CurrentValue * 0.3);
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
