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
    public int SlowPercent;
    [HideInInspector] public int UpgradeCost;
    [HideInInspector] public int CurrentValue;
    [HideInInspector] public int GlodPerRound;


    [HideInInspector] public int RepairCost;
    [HideInInspector] public int SellGain;

    public enum TowerType { Tank = 0, Range = 3, Slow = 4, Heal = 1, Gold = 2 }
    public TowerType Type;



    public virtual void AddEnemy(Enemy t) { /*Debug.Log("T: Enemy added");*/ }
    public virtual int GetTowerInfo(int[] info) { return 0; }
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
            CurrentHp -= ad;
        }
        else
        {
            CurrentHp = 0;
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
        UpgradeCost = (int)(CurrentValue * 0.8);
        RepairCost = (int)(CurrentValue * 0.3 * CurrentHp / HitPoint);
        SellGain = (int)(CurrentValue * 0.4 * CurrentHp / HitPoint);
        switch (Type)
        {
            case TowerType.Tank:
                CurrentHp += (int)(HitPoint * 0.3);
                HitPoint = (int)(HitPoint * 1.3);
                AttackDamage += 2;
                break;
            case TowerType.Range:
                CurrentHp += (int)(HitPoint * 0.2);
                HitPoint = (int)(HitPoint * 1.2);
                AttackDamage += 4;
                break;
            case TowerType.Slow:
                CurrentHp += (int)(HitPoint * 0.1);
                HitPoint = (int)(HitPoint * 1.1);
                AttackDamage += 1;
                break;
            case TowerType.Heal:
                CurrentHp += (int)(HitPoint * 0.1);
                HitPoint = (int)(HitPoint * 1.1);
                AttackDamage += 2;
                break;
            case TowerType.Gold:
                CurrentHp += (int)(HitPoint * 0.1);
                HitPoint = (int)(HitPoint * 1.1);
                GlodPerRound = (int)(CurrentValue * 0.3);
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
        LevelManager.UseGold(RepairCost);
        if (DestroyByEnemy)
        {
            TowerAnimator.SetTrigger("TowerFixed");
            DestroyByEnemy = false;
        }
    }


    public int GetLevel()
    {
        return CurrentLevel;
    }


    public bool IsFullHealth()
    {
        return CurrentHp == HitPoint;
    }
}
