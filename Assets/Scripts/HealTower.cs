using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class HealTower : Tower
{

    public int[] HealAmount;
    private HashSet<Tower> _towers;
    private bool _setAllyTower;
    private bool _clearAllyTower;


    void Start()
    {
        CurrentLevel = 0;
        DestroyByEnemy = false;
        StartTime = Time.time;
        CurrentHp = HitPoint[CurrentLevel];
        _towers = new HashSet<Tower>();
        _setAllyTower = false;
        _clearAllyTower = true;

        LevelManager = GameManager.Instance.CurrentLevelManager;
        NotificationPanel = NotificationPanel.Instance;
        TowerController = TowerController.Instance;
        TowerAnimator = GetComponent<Animator>();
    }


    void LateUpdate()
    {
        if (DestroyByEnemy) return;
        if (!_setAllyTower && LevelManager.CurrentGamePhase() == GameBoard.GamePhase.BattlePhase)
        {
            //TODO: at the beginning of battlePhase, add all ally nearby tower once
            _setAllyTower = true;
            _clearAllyTower = false;
        }
        if (!_clearAllyTower && LevelManager.CurrentGamePhase() == GameBoard.GamePhase.BuildingPhase)
        {
            //TODO: clear all nearby towers
            _setAllyTower = false;
            _clearAllyTower = true;
        }
        //Debug.Log("TT: Tower 0 searching");
        /*
        if (0 != _enemies.Count)
        {
            EndTime = Time.time;
            if (EndTime - StartTime > (float)(1 / AttackSpeed[CurrentLevel]))
            {
                StartTime = Time.time;
                AttackEnemy(_enemies.First()); // make this simple, just attack the first one
                _enemies.Clear();
            }
        }
        */
    }


    public void AddTower(Tower t)
    {
        if (DestroyByEnemy) return;
        //Debug.Log("HT: Enemy added");
        _towers.Add(t);
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
        Debug.Log("HT: Heals ally tower");
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
