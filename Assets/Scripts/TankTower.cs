using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class TankTower : Tower
{

    public int[] AttackDamage;
    private float _loadingTime;
    private HashSet<Enemy> _enemies;


    void Start()
    {
        CurrentLevel = 0;
        DestroyByEnemy = false;
        _loadingTime = AttackSpeed[CurrentLevel];
        CurrentHp = HitPoint[CurrentLevel];
        _enemies = new HashSet<Enemy>();

        LevelManager = GameManager.Instance.CurrentLevelManager;
        NotificationPanel = NotificationPanel.Instance;
        TowerController = TowerController.Instance;
        TowerAnimator = new Animator();
        //TowerAnimator.GetComponent<Animator>();
        //GetComponent < Animator > = TowerAnimator;
    }


    void LateUpdate()
    {
        //Debug.Log("TT: Tower 1 searching");
        if (0 != _enemies.Count)
        {
            float attackS = (float)AttackSpeed[CurrentLevel] - 0.95f;
            if (_loadingTime >= attackS)
            {
            // todo check if target enemy is still alive from controller
                AttackEnemy(_enemies.First()); // make this simple, just attack the first one
                _loadingTime = 0.0f;
                _enemies.Clear();
            }
            else
            {
                _loadingTime += Time.deltaTime;
            }
        }
    }

    /*
    public new void Setup(TileEventHandler tileEventHandler)
    {
        X = tileEventHandler.GridX;
        Y = tileEventHandler.GridY;
    }


    public new void Remove()
    {
        Destroy(gameObject);
    }
    */

    public override void AddEnemy(Enemy t)
    {
        //Debug.Log("TT: Enemy added");
        _enemies.Add(t);
        TowerController.AddEnemy(t);
    }


    public new void AttackEnemy(Enemy t)
    {
        //Debug.Log("TT: AttackEnemy~~~~~~~~~~~~~~~~~");
        if (TowerController.CheckIfEnemyAlive(t))
        {
            Vector3 start = transform.position;
            GameObject target = t.gameObject;
            Transform endTransform = target.transform;
            Vector3 end = endTransform.position;
            DrawLine(start, end, Color.yellow);
            t.GetDamaged(AttackDamage[CurrentLevel]);
            //Debug.Log("TT: Attacks");
        }
        else
        {
            //Debug.Log("T: Targeted enemy died");
        }
    }

    /*
    public new void ReceiveAttack(int ad)
    {
        if (CurrentHp > ad)
        {
            CurrentHp -= ad;
        }
        else
        {
            Remove();
            Debug.Log("T: Tower destoryed");
        }
    }
    */

    public new void Upgrade()
    {
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

    /*
    public new void Repair()
    {
        CurrentHp = HitPoint[CurrentLevel];
        LevelManager.UseGold(RepairCost[CurrentLevel]);
        //Debug.Log("T: Tower Repaired, HP is " + _currentHp);
    }


    public new int GetLevel()
    {
        return CurrentLevel;
    }


    public new bool CheckMaxLevel()
    {
        return CurrentLevel == MaxLevel - 1;
    }
    */

    public new void DrawLine(Vector3 start, Vector3 end, Color color, float duration = 0.05f)
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


    public new int GetAttackRange()
    {
        return AttackRange;
    }


    public new void GetTowerInfo(int[] info)
    {
        info[0] = AttackRange;
        info[1] = (int)Type;
        info[2] = CurrentLevel;
        info[3] = CurrentHp;
        info[4] = HitPoint[CurrentLevel];
        info[5] = AttackDamage[CurrentLevel];
        info[6] = AttackSpeed[CurrentLevel];
        info[7] = UpgradeCost;
        info[8] = RepairCost[CurrentLevel];
        info[9] = SellGain[CurrentLevel];
        info[10] = BuildCost;
    }
}
