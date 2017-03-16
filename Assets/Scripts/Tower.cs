using UnityEngine;
using UnityEngine.UI;

using System.Collections.Generic;
using System.Linq;

 public abstract class Tower : MonoBehaviour
 {

    [HideInInspector] public uint X;// { get; private set; }
    [HideInInspector] public uint Y;// { get; private set; }
    [HideInInspector] public int CurrentHp;
    [HideInInspector] public int CurrentLevel;
    [HideInInspector] public bool DestroyByEnemy;
    [HideInInspector] public float StartTime, EndTime;
    [HideInInspector] public LevelManager LevelManager;
    [HideInInspector] public TowerController TowerController;
    [HideInInspector] public NotificationPanel NotificationPanel;
    public int MaxLevel; 
    public int[] HitPoint;
    public int AttackRange;
    public int[] AttackSpeed;
    public int BuildCost;
    public int UpgradeCost;
    public int[] RepairCost;
    public int[] SellGain;
    public enum TowerType { Tank = 0, Range = 1, Slow = 2, Heal = 3, Money = 4 }
    public TowerType Type;
    public Animator TowerAnimator;


    public virtual void AddEnemy(Enemy t) { /*Debug.Log("T: Enemy added");*/ }
    public virtual int GetTowerInfo(int[] info) { return 0; }


    public bool IsDestory()
    {
        return DestroyByEnemy;
    }


    public void Setup(TileEventHandler tileEventHandler)
    {
        X = tileEventHandler.GridX;
        Y = tileEventHandler.GridY;
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
            //Remove();
            DestroyByEnemy = true;
            //Debug.Log("T: Tower at " + X + " " + Y + " is destoryed by enemy");
        }
    }

    public void ReceiveHeal(int heal)
    {
        //Debug.Log("T: Tower at " + X + " " + Y + " was received " + heal + " heal");
        if (CurrentHp + heal <= HitPoint[CurrentLevel])
        {
            CurrentHp += heal;
        }
        else
        {
            CurrentHp = HitPoint[CurrentLevel];
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
        if (CurrentLevel < MaxLevel - 1)
        {
            CurrentLevel += 1;
            CurrentHp = HitPoint[CurrentLevel];
            LevelManager.UseGold(UpgradeCost);
            //Debug.Log("T: Tower upgraded to level" + CurrentLevel);
        }
        else
        {
            //Debug.Log("T: Max Level");
            NotificationPanel.SetNotificationType("MaxLevel");
            NotificationPanel.Appear();
        }
    }


    public void Repair()
    {
        if (CurrentHp == HitPoint[CurrentLevel])
        {
            NotificationPanel.SetNotificationType("RepairWithFullHp");
            NotificationPanel.Appear();
            return;
        }
        Debug.Log(CurrentHp + " " + HitPoint[CurrentLevel]);
        CurrentHp = HitPoint[CurrentLevel];
        LevelManager.UseGold(RepairCost[CurrentLevel]);
        DestroyByEnemy = false;
        TowerAnimator.SetTrigger("TowerFixed");
        //Debug.Log("T: Tower Repaired, HP is " + _currentHp);
    }


    public int GetLevel()
    {
        return CurrentLevel;
    }


    public bool CheckMaxLevel()
    {
        return CurrentLevel == MaxLevel - 1;
    }


     public bool IsFullHealth()
     {
         return CurrentHp == HitPoint[CurrentLevel];
     }

    /*
    public void DrawLine(Vector3 start, Vector3 end, Color color, float duration = 0.05f)
    {
        GameObject myLine = new GameObject();
        myLine.transform.position = start;
        myLine.AddComponent<LineRenderer>();
        LineRenderer lr = myLine.GetComponent<LineRenderer>();
        lr.material = new Material(Shader.Find("Mobile/Particles/Additive"));
        lr.SetColors(color, color);
        lr.SetWidth(0.05f, 0.5f);
        lr.SetPosition(0, start);
        lr.SetPosition(1, end);
        lr.sortingLayerName = "Effects";
        GameObject.Destroy(myLine, duration);
    }
    */


    /*
    public uint X { get; private set; }
    public uint Y { get; private set; }

    public int buildCost;
    public int AttackRange;
    public int[] HitPoint;
    public int[] AttackDamage;
    public int[] AttackSpeed;
    public int upgradeCost;
    public int[] sellGain;
    public int[] repairCost;
    public int MaxLevel;
    public enum TowerType { Tank = 0, Range = 1, Slow = 2, Heal = 3, Money = 4 }
    public TowerType Type;
    private int _currentHp;
    private int _currentLevel;

    private HashSet<Enemy> _enemies;
    private LevelManager _levelManager;
    private float _loadingTime;
    private TowerController _towerController;

    private NotificationPanel _notificationPanel;


    void Start ()
    {
        _levelManager = GameManager.Instance.CurrentLevelManager;
        _enemies = new HashSet<Enemy>();
        _towerController = TowerController.Instance;
        _currentLevel = 0;
        _loadingTime = AttackSpeed[_currentLevel];
        _currentHp = HitPoint[_currentLevel];
        _notificationPanel = NotificationPanel.Instance;
    }


    void LateUpdate ()
    {
        //Debug.Log("T: Tower 1 searching");
        if (0 != _enemies.Count)
        {
            float attackS = (float)AttackSpeed[_currentLevel] - 0.95f;
            if (_loadingTime >= attackS)
            {
                AttackEnemy(_enemies.First()); // make this simple, just attack the first one
                _loadingTime = 0.0f;
                _enemies.Clear();
                _towerController.ClearEnemis();
            }
            else
            {
                _loadingTime += Time.deltaTime;
            }
        }
    }


    public void Setup(TileEventHandler tileEventHandler)
    {
        //_tileEventHandler = tileEventHandler;
        X = tileEventHandler.GridX;
        Y = tileEventHandler.GridY;
    }


    public void Remove()
    {
        Destroy(gameObject);
    }


    public void AddEnemy(Enemy t)
    {
        _enemies.Add(t);
        _towerController.AddEnemy(t);
    }


    public void AttackEnemy(Enemy t)
    {
        if (_towerController.CheckIfEnemyAlive(t))
        {
            Vector3 start = transform.position;
            GameObject target = t.gameObject;
            Transform endTransform = target.transform;
            Vector3 end = endTransform.position;
            DrawLine(start, end, Color.yellow);
            t.GetDamaged(AttackDamage[_currentLevel]);
            Debug.Log("T: Attacks");
        }
        else
        {
            Debug.Log("T: Targeted enemy died");
        }
    }


    public void ReceiveAttack(int ad)
    {
        if (_currentHp > ad)
        {
            _currentHp -= ad;
        }
        else
        {
            Remove();
            Debug.Log("T: Tower destoryed");
        }
    }


    public void Upgrade()
    {
        if (_currentLevel < MaxLevel - 1)
        {
            _currentLevel += 1;
            _currentHp = HitPoint[_currentLevel];
            _levelManager.UseGold(upgradeCost);
            Debug.Log("T: Tower upgraded to level" + _currentLevel);
        }
        else
        {
            Debug.Log("T: Max Level");
            _notificationPanel.SetNotificationType("MaxLevel");
        }
    }


    public void Repair()
    {
        _currentHp = HitPoint[_currentLevel];
        _levelManager.UseGold(repairCost[_currentLevel]);
        Debug.Log("T: Tower Repaired, HP is " + _currentHp);
    }


    public int GetLevel()
    {
        return _currentLevel;
    }


    public bool CheckMaxLevel()
    {
        return _currentLevel == MaxLevel - 1;
    }


    void DrawLine(Vector3 start, Vector3 end, Color color, float duration = 0.05f)
    {
        GameObject myLine = new GameObject();
        myLine.transform.position = start;
        myLine.AddComponent<LineRenderer>();
        LineRenderer lr = myLine.GetComponent<LineRenderer>();
        lr.material = new Material(Shader.Find("Mobile/Particles/Additive"));
        lr.SetColors(color, color);
        lr.SetWidth(0.05f, 0.7f);
        lr.SetPosition(0, start);
        lr.SetPosition(1, end);
        lr.sortingLayerName = "Effects"; 
        GameObject.Destroy(myLine, duration);
    }


    public int GetAttackRange()
    {
        return AttackRange;
    }


    public void GetTowerInfo(int[] info)
    {
        info[0] = AttackRange;
        info[1] = _currentLevel;
        info[2] = (int)Type;
        info[3] = _currentHp;
        info[4] = HitPoint[_currentLevel];
        info[5] = AttackDamage[_currentLevel];
        info[6] = AttackSpeed[_currentLevel];
        info[7] = upgradeCost;
        info[8] = repairCost[_currentLevel];
        info[9] = sellGain[_currentLevel];
        info[10] = buildCost;
    }
    */
}
