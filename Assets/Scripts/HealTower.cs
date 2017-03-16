using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class HealTower : Tower
{

    public int[] HealAmount;
    private HashSet<Tower> _towers;
    private List<Tower> _towerList;
    private bool _setAllyTower;
    private bool _clearAllyTower;


    void Start()
    {
        CurrentLevel = 0;
        DestroyByEnemy = false;
        StartTime = Time.time;
        CurrentHp = HitPoint[CurrentLevel];
        _towers = new HashSet<Tower>();

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
            EndTime = Time.time;
            if (EndTime - StartTime > (float)(1 / AttackSpeed[CurrentLevel]))
            {
                //Debug.Log("~~~~~~~~~~~~~~" + EndTime + " " + StartTime);
                StartTime = Time.time;
                for (int i = 0; i < _towers.Count; i++)
                {
                    if (!_towerList[i].IsDestory() && !_towerList[i].IsFullHealth())
                    {
                        HealAlly(_towerList[i]);
                        //break; heal mutiple towers
                    }
                }
            }
        }
    }


    public void AddTower(Tower t)
    {
        if (DestroyByEnemy) return;
        _towers.Add(t);
        //Debug.Log("HT: Add ally successfully " + _towers.Count);
        _towerList = _towers.ToList();
    }


    public void RemoveTower(Tower t)
    {
        _towers.Remove(t);
        //Debug.Log("HT: Remove ally successfully " + _towers.Count);
        _towerList = _towers.ToList();
    }


    public void HealAlly(Tower t)
    {
        if (DestroyByEnemy) return;
        // TODO: check if ally tower is destroied
        Vector3 start = transform.position;
        GameObject target = t.gameObject;
        Transform endTransform = target.transform;
        Vector3 end = endTransform.position;
        Color result = new Color(0, 1, 0, 1.0f);
        DrawLine(start, end, result);
        t.ReceiveHeal(HealAmount[CurrentLevel]);
        //Debug.Log("HT: Heals ally tower");
    }


    public void DrawLine(Vector3 start, Vector3 end, Color color, float duration = 0.2f)
    {
        GameObject myLine = new GameObject();
        myLine.transform.position = start;
        myLine.AddComponent<LineRenderer>();
        LineRenderer lr = myLine.GetComponent<LineRenderer>();
        lr.material = new Material(Shader.Find("Mobile/Particles/Additive"));
        lr.SetColors(color, color);
        lr.SetWidth(0.05f, 0.25f);
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
        info[5] = HealAmount[CurrentLevel];
        info[6] = AttackSpeed[CurrentLevel];
        info[7] = UpgradeCost;
        info[8] = RepairCost[CurrentLevel];
        info[9] = SellGain[CurrentLevel];
        info[10] = BuildCost;
        return info[0];
    }
}
