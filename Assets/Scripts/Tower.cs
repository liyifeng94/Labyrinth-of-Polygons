using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class Tower : MonoBehaviour
{
    public uint X { get; private set; }
    public uint Y { get; private set; }

    public uint AttackRange ;
    public uint HitPoint;
    public uint AttackDamage;
    public uint AttackSpeed;
    // public uint Level = 0;

    private HashSet<Enemy> _enemies = new HashSet<Enemy>();
    private GameBoard _gameBoard;
    private float _loadingTime;
    private TileEventHandler _tileEventHandler;
    void Start ()
	{
	    Build();
	    _gameBoard = GameManager.Instance.CurrentLevelManager.GameBoardSystem;
	    _loadingTime = AttackSpeed;
	    _tileEventHandler = null;

	}

	void LastUpdate () {
	    if (null != _enemies.First())
	    {
	        if (_loadingTime >= AttackSpeed)
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
        /*
	    if (Input.GetMouseButtonDown(0)) // 0 for mouse-left button
	    {
            Debug.Log("Pressed left click.");
            // Todo show remove, upgrade, and tower-info selections
        }
        */
    }

    public void Build()
    {
        bool success = _gameBoard.BuildTower(this);
        if (!success)
        {
            Debug.Log("Tower cannot be build");
        }
    }

    public void Destory()
    {
        _gameBoard.RemoveTower(this);
    }
    
    public void AddEnemy(Enemy t)
    {
        _enemies.Add(t);
    }

    public void AttackEnemy(Enemy t)
    {
        t.GetDamaged(AttackDamage);
    }
    

    public void ReceiveAttack(uint ad)
    {
        if (HitPoint > ad)
        {
            HitPoint -= ad;
        }
        else
        {
            Destory();
        }
    }
}
