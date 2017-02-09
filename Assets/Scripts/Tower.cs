using UnityEngine;

public class Tower : MonoBehaviour {

	public uint X { get; private set; }
	public uint Y { get; private set; }
	public uint AttackRange { get; private set; }


	public uint HitPoint;
	public uint AttackDamage;
	public uint AttackSpeed;  // trsf only
	//private uint TargetEnemy;
	//private Manager Nanager; // class name issue
	//private Block[] Block;   // class name issue
	private uint BIndex;
	//private Enemy Target;    // class name issue

	// Use this for initialization
	void Start ()
	{
	    Build();
	}
	
	// Update is called once per frame
	void LastUpdate () {
	
	}

    public void Build()
    {
        //m.build(x, y, r, this);
    }

    public void Destory()
    {
        for (int i = 0; i < BIndex; i++)
        {
            //block[i].deLink(this);
        }
    }
    /*
    public void SetTarget(Enemy t)
    {
        Target = t;
    }

    public void AttackEnemy(Enemy t)
    {
        t.ReceiveAttack(ad);
    }
    */

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
    /*
    public void assignBlock(Block b)
    {
        Block[bIndex] = b;
        BIndex++;
    }
    */
}
