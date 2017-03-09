using UnityEngine;
using System.Collections;

public class TowerOperationPanel : MonoBehaviour {

    public static TowerOperationPanel Instance;
    public GameObject ThisPanel;

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
        ThisPanel.SetActive(false);
    }
}
