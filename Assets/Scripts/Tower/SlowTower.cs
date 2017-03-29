using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class SlowTower : Tower
{

    private HashSet<Enemy> _enemies;


    void Awake()
    {
        CurrentHp = HitPoint;
        CurrentLevel = 1;
        CurrentValue = BuildCost;
        UpgradeCost = (int)(CurrentValue * UpgradeFactor);
        RepairCost = (int)(CurrentValue * RepairFactor * (1 - 1.0 * CurrentHp / HitPoint));
        SellGain = (int)(CurrentValue * SellFactor * CurrentHp / HitPoint);
    }


    void Start()
    {
        DestroyByEnemy = false;
        StartTime = Time.time;
        _enemies = new HashSet<Enemy>();
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
            _enemies.Clear();
            return;
        }
        //Debug.Log("ST: Tower 0 searching");
        if (0 != _enemies.Count)
        {
            if (!Reloading)
            {
                List<Enemy> enemyList = _enemies.ToList();
                int lessOrEqualThanThree;
                if (_enemies.Count < 3)
                {
                    lessOrEqualThanThree = _enemies.Count;
                }
                else
                {
                    lessOrEqualThanThree = 2;
                }
                for (int i = 0; i < lessOrEqualThanThree; i++)
                {
                    SlowEnemy(enemyList[i]);
                    AttackEnemy(enemyList[i]);
                }
                FireSoundSource.Play();
                EndTime = Time.time + ReloadTime;
                Reloading = true;
            }
            else
            {
                if (EndTime - Time.time < 0) Reloading = false;
            }
        }
        _enemies.Clear();
    }


    public override void AddEnemy(Enemy t)
    {
        if (DestroyByEnemy) return;
        _enemies.Add(t);
        TowerController.AddEnemy(t);
    }


    public void SlowEnemy(Enemy t)
    {
        if (TowerController.CheckIfEnemyAlive(t))
        {
            Vector3 start = transform.position;
            GameObject target = t.gameObject;
            Transform endTransform = target.transform;
            Vector3 end = endTransform.position;
            Color result = new Color(1, 0, 1, 1.0f);
            DrawLine(start, end, result);
            t.SlowDown(0.5f);
        }
        else
        {
            //Debug.Log("ST: Targeted enemy died");
        }
    }


    public void AttackEnemy(Enemy t)
    {
        if (DestroyByEnemy) return;
        //Debug.Log("ST: AttackEnemy~~~~~~~~~~~~~~~~~");
        if (TowerController.CheckIfEnemyAlive(t))
        {
            t.GetDamaged(AttackDamage);
            //Debug.Log("ST: Attacks, damage is  "+ AttackDamage);
        }
        else
        {
            //Debug.Log("ST: Targeted enemy died");
        }
    }


    public void DrawLine(Vector3 start, Vector3 end, Color color, float duration = 0.1f)
    {
        GameObject myLine = new GameObject();
        myLine.transform.position = start;
        myLine.AddComponent<LineRenderer>();
        LineRenderer lr = myLine.GetComponent<LineRenderer>();
        lr.material = new Material(Shader.Find("Mobile/Particles/Additive"));
        lr.startColor = color;
        lr.endColor = color;
        //lr.SetColors(color, color);
        lr.startWidth = 0.25f;
        lr.endWidth = 0.25f;
        //lr.SetWidth(0.25f, 0.25f);
        lr.SetPosition(0, start);
        lr.SetPosition(1, end);
        lr.sortingLayerName = "Effects";
        GameObject.Destroy(myLine, duration);
    }


    public new void GetTowerUpgradedInfo(int[] info)
    {
        int upgratedCurrentValue = CurrentValue + UpgradeCost;
        info[0] = AttackRange;
        info[1] = (int)Type;
        info[2] = CurrentLevel + 1;
        info[3] = CurrentHp + (int)(HitPoint * 0.1);
        info[4] = (int)(HitPoint * 1.1);
        info[5] = AttackDamage + 1;
        info[6] = ReloadTime;
        info[7] = (int)(upgratedCurrentValue * UpgradeFactor);
        info[8] = (int)(upgratedCurrentValue * RepairFactor * (1 - 1.0 * info[3] / info[4]));
        info[9] = (int)(upgratedCurrentValue * SellFactor * CurrentHp / HitPoint);
        info[10] = BuildCost;
    }
}
