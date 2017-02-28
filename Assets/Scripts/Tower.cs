using UnityEngine;
using System.Collections.Generic;
using System.Linq;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class Tower : MonoBehaviour
{
    public uint X { get; private set; }
    public uint Y { get; private set; }

    public int buildCost;
    public int AttackRange;
    public int[] HitPoint;
    public int[] AttackDamage;
    public int[] AttackSpeed;
    public int[] upgradeCost;

    private int _currentHP;
    private int _level;
    private HashSet<Enemy> _enemies = new HashSet<Enemy>();
    private GameBoard _gameBoard;
    private float _loadingTime;
    private TileEventHandler _tileEventHandler;
    private TowerController _towerController;

    void Start ()
    {
        _gameBoard = GameManager.Instance.CurrentLevelManager.GameBoardSystem;
        _towerController = TowerController.Instance;
        //_tileEventHandler = towerGameObject.GetComponent<Tower>(); // get scripts
        Build();
        _level = 0;
        _loadingTime = AttackSpeed[_level];
        _currentHP = HitPoint[_level];
    }

	void LateUpdate () {
        //Debug.Log("T: Tower 1 searching");
        //if (null != _enemies.First())
        if (0 != _enemies.Count)
        {
            float attackS = (float)AttackSpeed[_level] - 0.95f;

            //if (_loadingTime >= AttackSpeed[_level])
            if (_loadingTime >= attackS)
	        {
                AttackEnemy(_enemies.First()); // make this simple, just attack the first one
                _loadingTime = 0.0f;
                _enemies.Clear();
	        }
	        else
	        {
                _loadingTime += Time.deltaTime;
	        }
        }
    }

    public void Setup(TileEventHandler tileEventHandler)
    {
        _tileEventHandler = tileEventHandler;
        X = tileEventHandler.GridX;
        Y = tileEventHandler.GridY;
    }

    public void Build()
    {
        bool success = _gameBoard.BuildTower(this);
        if (!success)
        {
            //Debug.Log("T: Tower cannot be build");
            _tileEventHandler.RemoveTower();
            _tileEventHandler.SetTowerExist(false);
        }
        _tileEventHandler.SetTowerExist(true);

    }

    public void Remove()
    {
        //_gameBoard.RemoveTower(this);
        _tileEventHandler.SetTowerExist(false);
        Destroy(gameObject);
    }
    
    public void AddEnemy(Enemy t)
    {
        _enemies.Add(t);
    }

    public void AttackEnemy(Enemy t)
    {
        Vector3 start = transform.position;
        Vector3 end = _enemies.First().transform.position;
        DrawLine(start, end, Color.blue);
        Debug.Log("attacks");
        t.GetDamaged(AttackDamage[_level]);
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
        lr.material = new Material(Shader.Find("Particles/Alpha Blended Premultiply"));
        lr.SetColors(color, color);
        lr.SetWidth(0.1f, 0.1f);
        lr.SetPosition(0, start);
        lr.SetPosition(1, end);
        lr.sortingLayerName = "Effects"; 
        GameObject.Destroy(myLine, duration);
    }

}
