using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class Tower : MonoBehaviour
{
    public uint X { get; private set; }
    public uint Y { get; private set; }

    public int buildCost;
    public int AttackRange;
    public int[] HitPoint;
    public int[] AttackDamage;
    public int[] AttackSpeed;
    public int upgradeCost;
    public int[] sellGain;
    public int[] repairCost;

    private int _currentHP;
    private int _level;
    private HashSet<Enemy> _enemies;
    private LevelManager _levelManager;
    //private GameBoard _gameBoard;
    private float _loadingTime;
    //private TileEventHandler _tileEventHandler;
    private TowerController _towerController;

    void Start ()
    {
        _levelManager = GameManager.Instance.CurrentLevelManager;
        //_gameBoard = _levelManager.GameBoardSystem;
        _enemies = new HashSet<Enemy>();
        _towerController = TowerController.Instance;
        _level = 0;
        _loadingTime = AttackSpeed[_level];
        _currentHP = HitPoint[_level];
    }

	void LateUpdate ()
    {
        //Debug.Log("T: Tower 1 searching");
        if (0 != _enemies.Count)
        {
            float attackS = (float)AttackSpeed[_level] - 0.95f;
            if (_loadingTime >= attackS)
	        {
                // todo check if target enemy is still alive from controller
                AttackEnemy(_enemies.First()); // make this simple, just attack the first one
                _loadingTime = 0.0f;
                _enemies.Clear();
                _towerController.ClearEnemis();
	        }
	        else
	        {
                _loadingTime += Time.deltaTime;
	        }
        }
    }

    public void Setup(TileEventHandler tileEventHandler)
    {
        //_tileEventHandler = tileEventHandler;
        X = tileEventHandler.GridX;
        Y = tileEventHandler.GridY;
    }

    public void Remove()
    {
        Destroy(gameObject);
    }
    
    public void AddEnemy(Enemy t)
    {
        _enemies.Add(t);
        _towerController.AddEnemy(t);
    }

    public void AttackEnemy(Enemy t)
    {
        Vector3 start = transform.position;
        GameObject target = t.gameObject;
        Transform endTransform = target.transform;
        Vector3 end = endTransform.position;
        DrawLine(start, end, Color.blue);
        Debug.Log("attacks");
        if (_towerController.CheckIfEnemyAlive(t))
        {
            t.GetDamaged(AttackDamage[_level]);
        }
        else
        {
            Debug.Log("T: Targeted enemy died");
        }
    }
    

    public void ReceiveAttack(int ad)
    {
        if (_currentHP > ad)
        {
            _currentHP -= ad;
        }
        else
        {
            Remove();
            Debug.Log("T: Tower destoryed");
        }
    }

    public void Upgrade()
    {
        if (_level < 2)
        {
            _level += 1;
            _currentHP = HitPoint[_level];
            _levelManager.UseGold(upgradeCost);
            Debug.Log("T: Tower upgraded to level" + _level);
        }
        else
        {
            Debug.Log("T: Max Level");
        }
    }

    public void Repair()
    {
        _currentHP = HitPoint[_level];
        _levelManager.UseGold(repairCost[_level]);
        Debug.Log("T: Tower Repaired, HP is " + _currentHP);
    }

    public int getLevel()
    {
        return _level;
    }

    void DrawLine(Vector3 start, Vector3 end, Color color, float duration = 0.05f)
    {
        GameObject myLine = new GameObject();
        myLine.transform.position = start;
        myLine.AddComponent<LineRenderer>();
        LineRenderer lr = myLine.GetComponent<LineRenderer>();
        lr.material = new Material(Shader.Find("Mobile/Particles/Additive"));
        lr.SetColors(color, color);
        lr.SetWidth(0.1f, 0.1f);
        lr.SetPosition(0, start);
        lr.SetPosition(1, end);
        lr.sortingLayerName = "Effects"; 
        GameObject.Destroy(myLine, duration);
    }

}
