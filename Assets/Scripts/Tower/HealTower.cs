using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class HealTower : Tower
{
    private HashSet<Tower> _towers;
    private List<Tower> _towerList;
    //private bool _setAllyTower;
    //private bool _clearAllyTower;


    void Start()
    {
        CurrentLevel = 1;
        DestroyByEnemy = false;
        StartTime = Time.time;
        CurrentHp = HitPoint;
        _towers = new HashSet<Tower>();
        Reloading = false;

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
                for (int i = 0; i < _towers.Count; i++)
                {
                    if (!_towerList[i].IsDestory() && !_towerList[i].IsFullHealth())
                    {
                        HealAlly(_towerList[i]);
                        //break; heal mutiple towers
                    }
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
        if (null == t) return;
        //Debug.Log("HT: Allay destoried case");
        if (DestroyByEnemy) return;
        // TODO: check if ally tower is destroied
        Vector3 start = transform.position;
        GameObject target = t.gameObject;
        Transform endTransform = target.transform;
        Vector3 end = endTransform.position;
        Color result = new Color(0, 1, 0, 1.0f);
        DrawLine(start, end, result);
        t.ReceiveHeal(HealAmount);
        //Debug.Log("HT: Heals ally tower");
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


    public new int GetTowerInfo(int[] info)
    {
        info[0] = AttackRange;
        info[1] = (int)Type;
        info[2] = CurrentLevel;
        info[3] = CurrentHp;
        info[4] = HitPoint;
        info[5] = HealAmount;
        info[6] = ReloadTime;
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
        info[4] = HitPoint / CurrentLevel * (CurrentLevel + 1);
        info[5] = HealAmount / CurrentLevel * (CurrentLevel + 1);
        info[6] = ReloadTime;
        info[7] = UpgradeCost / CurrentLevel * (CurrentLevel + 1);
        info[8] = RepairCost / CurrentLevel * (CurrentLevel + 1);
        info[9] = SellGain / CurrentLevel * (CurrentLevel + 1);
        info[10] = BuildCost;
    }
}
