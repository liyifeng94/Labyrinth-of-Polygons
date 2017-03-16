using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class RangeTower : Tower
{

    public int[] AttackDamage;
    private HashSet<Enemy> _enemies;


    void Start()
    {
        CurrentLevel = 0;
        DestroyByEnemy = false;
        StartTime = Time.time;
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
        //Debug.Log("RT: Tower 1 searching");
        if (0 != _enemies.Count)
        {
            EndTime = Time.time;
            if (EndTime - StartTime > (float)(1/AttackSpeed[CurrentLevel]))
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
        //Debug.Log("RT: Enemy added");
        _enemies.Add(t);
        TowerController.AddEnemy(t);
    }


    public void AttackEnemy(Enemy t)
    {
        if (DestroyByEnemy) return;
        //Debug.Log("RT: AttackEnemy~~~~~~~~~~~~~~~~~");
        if (TowerController.CheckIfEnemyAlive(t))
        {
            Vector3 start = transform.position;
            GameObject target = t.gameObject;
            Transform endTransform = target.transform;
            Vector3 end = endTransform.position;
            Color result = new Color(1, 0, 0, 1.0f);
            DrawLine(start, end, result);
            t.GetDamaged(AttackDamage[CurrentLevel]);
            //Debug.Log("RT: Attacks");
        }
        else
        {
            //Debug.Log("RT: Targeted enemy died");
        }
    }


    public void DrawLine(Vector3 start, Vector3 end, Color color, float duration = 0.05f)
    {
        GameObject myLine = new GameObject();
        myLine.transform.position = start;
        myLine.AddComponent<LineRenderer>();
        LineRenderer lr = myLine.GetComponent<LineRenderer>();
        lr.material = new Material(Shader.Find("Mobile/Particles/Additive"));
        lr.SetColors(color, color);
        lr.SetWidth(0.25f, 0.05f);
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
