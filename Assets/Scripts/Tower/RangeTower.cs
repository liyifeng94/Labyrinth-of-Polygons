using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class RangeTower : Tower
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
        //Debug.Log("RT: Tower 1 searching");
        if (0 != _enemies.Count)
        {
            if (!Reloading)
            {
                AttackEnemy(_enemies.First()); // make this simple, just attack the first one
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
            t.GetDamaged(AttackDamage);
            //Debug.Log("RT: Attacks");
        }
        else
        {
            //Debug.Log("RT: Targeted enemy died");
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
        lr.endWidth = 0.05f;
        //lr.SetWidth(0.25f, 0.05f);
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
        info[3] = CurrentHp + (int)(HitPoint * (1.0 * RangeTowerHpFactor - 1));
        info[4] = (int)(HitPoint * RangeTowerHpFactor);
        info[5] = AttackDamage + RangeTowerAtkIncrement;
        info[6] = ReloadTime;
        info[7] = (int)(upgratedCurrentValue * UpgradeFactor);
        info[8] = (int)(upgratedCurrentValue * RepairFactor * (1 - 1.0 * info[3] / info[4]));
        info[9] = (int)(upgratedCurrentValue * SellFactor * CurrentHp / HitPoint);
        info[10] = BuildCost;
    }
}
