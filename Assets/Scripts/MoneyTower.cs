using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class MoneyTower : Tower
{

    public int[] MoneyPerTenSec;
    private float _currentReservedMoney;
    private bool _reservedMoneyTransfered;
    //private GameBoard _gameBoard;


    void Start()
    {
        CurrentLevel = 0;
        DestroyByEnemy = false;
        StartTime = Time.time;
        CurrentHp = HitPoint[CurrentLevel];
        _currentReservedMoney = 0;
        _reservedMoneyTransfered = true;

                LevelManager = GameManager.Instance.CurrentLevelManager;
        NotificationPanel = NotificationPanel.Instance;
        TowerController = TowerController.Instance;
        //_gameBoard = LevelManager.GameBoardSystem;
        TowerAnimator = GetComponent<Animator>();
    }


    void LateUpdate()
    {
        if (DestroyByEnemy) return;
        if (LevelManager.CurrentGamePhase() == GameBoard.GamePhase.BattlePhase)
        {
            EndTime = Time.time;
            if (EndTime - StartTime > (float)(1 / AttackSpeed[CurrentLevel]))
            {
                StartTime = Time.time;
                MoneyGain((float)(MoneyPerTenSec[CurrentLevel])/10);
            }
            _reservedMoneyTransfered = false;
        }

        // after the first round, it starts transfering the money beforing entering the further rounds
        if (LevelManager.CurrentGamePhase() == GameBoard.GamePhase.BuildingPhase && !_reservedMoneyTransfered)
        {
            LevelManager.AddGold((int)_currentReservedMoney);
            Debug.Log("MT: " + (int)_currentReservedMoney + " gold gained this round");
        }
    }


    public void MoneyGain(float money)
    {
        _currentReservedMoney += money;
        Debug.Log("MT: Total reserved money is " + _currentReservedMoney);
        //TODO: call highlight from gameboard
        _currentReservedMoney = 0;
        _reservedMoneyTransfered = true;
    }


    public void DrawSquare(Vector3 start, Vector3 end, Color color, float duration = 0.2f)
    {
        GameObject myLine = new GameObject();
        myLine.transform.position = start;
        myLine.AddComponent<LineRenderer>();
        LineRenderer lr = myLine.GetComponent<LineRenderer>();
        lr.material = new Material(Shader.Find("Mobile/Particles/Additive"));
        lr.SetColors(color, color);
        lr.SetWidth(0.5f, 0.5f);
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
        info[5] = MoneyPerTenSec[CurrentLevel];
        info[6] = AttackSpeed[CurrentLevel];
        info[7] = UpgradeCost;
        info[8] = RepairCost[CurrentLevel];
        info[9] = SellGain[CurrentLevel];
        info[10] = BuildCost;
        return info[0];
    }
}
