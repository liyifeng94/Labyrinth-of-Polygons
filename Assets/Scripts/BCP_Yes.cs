using UnityEngine;
using System.Collections;

public class BCP_Yes : MonoBehaviour {

    private TowerBuildPanel _towerBuildPanel;
    private BuildCheckPanel _buildCheckPanel;
    private TowerInfoPanel _towerInfoPanel;

    // Use this for initialization
    void Start () {
        _towerBuildPanel = TowerBuildPanel.Instance;
        _buildCheckPanel = BuildCheckPanel.Instance;
        _towerInfoPanel = TowerInfoPanel.Instance;
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void BuildConfirmed()
    {
        Debug.Log("TankTowerButton clicked");
        // TODO call controller
        _towerBuildPanel.DisAppear();
        _buildCheckPanel.DisAppear();
        _towerInfoPanel.DisAppear();

    }
}
