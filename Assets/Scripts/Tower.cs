using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class Tower : MonoBehaviour {

	public uint X { get; private set; }
	public uint Y { get; private set; }
	public uint AttackRange { get; private set; }
	public uint HitPoint;
	public uint AttackDamage;
	public uint AttackSpeed;  // trsf only

    private HashSet<Enemy> _enemies = new HashSet<Enemy>();
    private GameBoard _gameBoard;
    private float _loadingTime;

    void Start ()
	{
	    Build();
	    _enemies = null;
	    _gameBoard = GameManager.Instance.CurrentLevelManager.GameBoardSystem;
        _loadingTime = AttackSpeed;
	}

	void LastUpdate () {
	    if (null != _enemies)
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
	}

    public void Build()
    {

        _gameBoard.BuildTower(this);
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
        t.damaged(AttackDamage);
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
