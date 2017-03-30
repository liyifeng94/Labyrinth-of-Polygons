using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class HealTower : Tower
{
    private HashSet<Tower> _towers;
    private List<Tower> _towerList;
    private bool _healSound;


    void Awake()
    {
        CurrentHp = HitPoint;
        CurrentLevel = 1;
        CurrentValue = BuildCost;
        UpgradeCost = (int)(CurrentValue * UpgradeFactor);
        RepairCost = (int)(CurrentValue * RepairFactor * (1 - 1.0 * CurrentHp / HitPoint));
        SellGain = (int)(CurrentValue * SellFactor * CurrentHp / HitPoint);
        AttackDamage = (int)(HitPoint * 0.2);
    }


    void Start()
    {
        DestroyByEnemy = false;
        StartTime = Time.time;
        _towers = new HashSet<Tower>();
        Reloading = false;
        _healSound = true;

        LevelManager = GameManager.Instance.CurrentLevelManager;
        NotificationPanel = NotificationPanel.Instance;
        TowerController = TowerController.Instance;
        TowerAnimator = GetComponent<Animator>();
    }


    void LateUpdate()
    {
        if (DestroyByEnemy) return;
        if (LevelManager.CurrentGamePhase() == GameBoard.GamePhase.BuildingPhase) return;
        if (0 != _towers.Count)
        {
            if (!Reloading)
            {
                if (0 != _towers.Count) 
                for (int i = 0; i < _towers.Count; i++)
                {
                    if (!_towerList[i].IsDestory() && !_towerList[i].IsFullHealth())
                    {
                        HealAlly(_towerList[i]);
                        //break; heal mutiple towers
                    }
                }
                EndTime = Time.time + ReloadTime;
                Reloading = true;
                _healSound = true;
            }
            else
            {
                if (EndTime - Time.time < 0) Reloading = false;
            }
        }
    }


    public void AddTower(Tower t)
    {
        if (DestroyByEnemy) return;
        _towers.Add(t);
        _towerList = _towers.ToList();
    }


    public void RemoveTower(Tower t)
    {
        _towers.Remove(t);
        _towerList = _towers.ToList();
    }


    public void HealAlly(Tower t)
    {
        if (null == t) return;
        if (DestroyByEnemy) return;
        if (_healSound) FireSoundSource.Play();
        _healSound = false;
        Vector3 start = transform.position;
        GameObject target = t.gameObject;
        Transform endTransform = target.transform;
        Vector3 end = endTransform.position;
        Color result = new Color(0, 1, 0, 1.0f);
        DrawLine(start, end, result);
        t.ReceiveHeal(AttackDamage);
    }


    public void DrawLine(Vector3 start, Vector3 end, Color color, float duration = 0.2f)
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
        lr.endWidth = 0.25f;
        //lr.SetWidth(0.05f, 0.25f);
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
        info[3] = CurrentHp + (int)(HitPoint * (1.0 * HealTowerHpFactor - 1));
        info[4] = (int)(HitPoint * HealTowerHpFactor);
        info[5] = (int)(info[4] * HealTowerHealFactor);
        info[6] = ReloadTime;
        info[7] = (int)(upgratedCurrentValue * UpgradeFactor);
        info[8] = (int)(upgratedCurrentValue * RepairFactor * (1 - 1.0 * info[3] / info[4]));
        info[9] = (int)(upgratedCurrentValue * SellFactor * CurrentHp / HitPoint);
        info[10] = BuildCost;
    }
}
