using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class TankTower : Tower
{

    public int[] AttackDamage;
    private HashSet<Enemy> _enemies;


    void Start()
    {
        CurrentLevel = 0;
        DestroyByEnemy = false;
        _start = Time.time;
        CurrentHp = HitPoint[CurrentLevel];
        _enemies = new HashSet<Enemy>();

        LevelManager = GameManager.Instance.CurrentLevelManager;
        NotificationPanel = NotificationPanel.Instance;
        TowerController = TowerController.Instance;
        TowerAnimator = GetComponent<Animator>();
    }


    void LateUpdate()
    {
        if (DestroyByEnemy) return;
        //Debug.Log("TT: Tower 0 searching");
        if (0 != _enemies.Count)
        {
            _end = Time.time;
            if (_end - _start > (float)(1 / AttackSpeed[CurrentLevel]))
            {
                _start = Time.time;
                AttackEnemy(_enemies.First()); // make this simple, just attack the first one
                _enemies.Clear();
            }
        }
    }


    public override void AddEnemy(Enemy t)
    {
        if (DestroyByEnemy) return;
        //Debug.Log("TT: Enemy added");
        _enemies.Add(t);
        TowerController.AddEnemy(t);
    }


    public new void AttackEnemy(Enemy t)
    {
        if (DestroyByEnemy) return;
        //Debug.Log("TT: AttackEnemy~~~~~~~~~~~~~~~~~");
        if (TowerController.CheckIfEnemyAlive(t))
        {
            Vector3 start = transform.position;
            GameObject target = t.gameObject;
            Transform endTransform = target.transform;
            Vector3 end = endTransform.position;
            Color result = new Color(0, 1, 1, 1.0f);
            DrawLine(start, end, result);
            t.GetDamaged(AttackDamage[CurrentLevel]);
            //Debug.Log("TT: Attacks");
        }
        else
        {
            //Debug.Log("T: Targeted enemy died");
        }
    }


    public new int GetAttackRange()
    {
        return AttackRange;
    }


    public new int GetTowerInfo(int[] info)
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
        return info[0];
    }
}
