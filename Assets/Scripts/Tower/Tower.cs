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
    //public int MaxLevel; 
    public int HitPoint;
    public int AttackRange;
    //public int AttackSpeed;
    public int ReloadTime;
    public int BuildCost;
    public int UpgradeCost;
    public int RepairCost;
    public int SellGain;

    public int AttackDamage;
    public int SlowPercent;
    public int HealAmount;
    public int GoldPerMinute;
    public enum TowerType { Tank = 0, Range = 3, Slow = 4, Heal = 1, Gold = 2 }
    public TowerType Type;

    public virtual void AddEnemy(Enemy t) { /*Debug.Log("T: Enemy added");*/ }
    public virtual int GetTowerInfo(int[] info) { return 0; }
    public virtual void GetTowerUpgradedInfo(int[] info) { }

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
            TowerAnimator.SetTrigger("TowerDestroyed");
            _destroySoundSource.Play();
            DestroyByEnemy = true;
            //Debug.Log("T: Tower at " + X + " " + Y + " is destoryed by enemy");
        }
    }

    public void ReceiveHeal(int heal)
    {
        //Debug.Log("T: Tower at " + X + " " + Y + " was received " + heal + " heal");
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
        if (DestroyByEnemy)
        {
            NotificationPanel.SetNotificationType("UpgradeWhenDestroied");
            NotificationPanel.Appear();
            return;
        }
        LevelManager.UseGold(UpgradeCost);


        int previoutLevel = CurrentLevel;
        CurrentLevel += 1;
        CurrentHp += HitPoint / previoutLevel;
        HitPoint = HitPoint / previoutLevel * CurrentLevel;
        UpgradeCost = UpgradeCost / previoutLevel * CurrentLevel;
        RepairCost = RepairCost / previoutLevel * CurrentLevel;
        SellGain = SellGain / previoutLevel * CurrentLevel;
        switch (Type)
        {
            case TowerType.Tank:
                AttackDamage = AttackDamage / previoutLevel * CurrentLevel;
                break;
            case TowerType.Range:
                AttackDamage = AttackDamage / previoutLevel * CurrentLevel;
                break;
            case TowerType.Slow:
                //SlowPercent = SlowPercent / PrevioutLevel * CurrentLevel;
                break;
            case TowerType.Heal:
                HealAmount = HealAmount / previoutLevel * CurrentLevel;
                break;
            case TowerType.Gold:
                GoldPerMinute = GoldPerMinute / previoutLevel * CurrentLevel;
                break;
        }
        /*if (CurrentLevel < MaxLevel - 1)
        {
            CurrentLevel += 1;
            CurrentHp = HitPoint;
            LevelManager.UseGold(UpgradeCost);
            //Debug.Log("T: Tower upgraded to level" + CurrentLevel);
        }
        else
        {
            //Debug.Log("T: Max Level");
            NotificationPanel.SetNotificationType("MaxLevel");
            NotificationPanel.Appear();
        }*/
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

    
    /*public bool CheckMaxLevel()
    {
        return CurrentLevel == MaxLevel - 1;
    }*/


    public bool IsFullHealth()
    {
        return CurrentHp == HitPoint;
    }
}
