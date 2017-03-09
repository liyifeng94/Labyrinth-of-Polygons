using UnityEngine;
using System.Collections;

public class TowerBuildPanel : MonoBehaviour {

    public static TowerBuildPanel Instance;
    public GameObject ThisPanel;
    private TileEventHandler _tileEventHandler;

    void Awake()
    {
        Instance = this;
    }

    // Use this for initialization
    void Start () {
        ThisPanel.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {

    }

    public void Appear()
    {
        ThisPanel.SetActive(true);
    }

    public void DisAppear()
    {
        Debug.Log("!!!!!!!!!!!!!!!!!!!");
        ThisPanel.SetActive(false);
    }
}
