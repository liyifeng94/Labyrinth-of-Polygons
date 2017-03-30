using UnityEngine;

public class TowerBuildPanel : MonoBehaviour {

    public static TowerBuildPanel Instance;
    public GameObject ThisPanel;
    private TileEventHandler _tileEventHandler;

    void Awake()
    {
        Instance = this;
    }

    void Start () {
        ThisPanel.SetActive(false);
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
