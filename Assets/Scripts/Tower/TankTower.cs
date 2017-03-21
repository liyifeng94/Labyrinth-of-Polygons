using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class TankTower : Tower
{

    //public int AttackDamage;
    private HashSet<Enemy> _enemies;


    void Start()
    {
        CurrentLevel = 1;
        DestroyByEnemy = false;
        StartTime = Time.time;
        CurrentHp = HitPoint;
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
            EndTime = Time.time;
            if (EndTime - StartTime > (float)(1 / AttackSpeed))
            {
                StartTime = Time.time;
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


    public void AttackEnemy(Enemy t)
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
            t.GetDamaged(AttackDamage);
            //Debug.Log("TT: Attacks");
        }
        else
        {
            //Debug.Log("T: Targeted enemy died");
        }
    }


    public void DrawLine(Vector3 start, Vector3 end, Color color, float duration = 0.05f)
    {
        GameObject myLine = new GameObject();
        myLine.transform.position = start;
        myLine.AddComponent<LineRenderer>();
        LineRenderer lr = myLine.GetComponent<LineRenderer>();
        lr.material = new Material(Shader.Find("Mobile/Particles/Additive"));
        lr.startColor = color;
        lr.endColor = color;
        //lr.SetColors(color, color);
        lr.startWidth = 0.05f;
        lr.endWidth = 0.5f;
        //lr.SetWidth(0.05f, 0.5f);
        lr.SetPosition(0, start);
        lr.SetPosition(1, end);
        lr.sortingLayerName = "Effects";
        GameObject.Destroy(myLine, duration);
    }


    public new int GetTowerInfo(int[] info)
    {
        info[0] = AttackRange;
        info[1] = (int)Type;
        info[2] = CurrentLevel;
        info[3] = CurrentHp;
        info[4] = HitPoint;
        info[5] = AttackDamage;
        info[6] = AttackSpeed;
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
        info[4] = HitPoint / CurrentLevel * CurrentLevel;
        info[5] = AttackDamage;
        info[6] = AttackSpeed;
        info[7] = UpgradeCost / CurrentLevel * CurrentLevel;
        info[8] = RepairCost / CurrentLevel * CurrentLevel;
        info[9] = SellGain / CurrentLevel * CurrentLevel;
        info[10] = BuildCost;
    }
}
