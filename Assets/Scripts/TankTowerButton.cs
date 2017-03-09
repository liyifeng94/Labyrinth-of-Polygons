using UnityEngine;
using System.Collections;

public class TankTowerButton : MonoBehaviour {

    //public BuildCheckPanel BuildCheckPanel;
    private BuildCheckPanel _buildCheckPanel;
    // Use this for initialization
    void Start () {
        _buildCheckPanel = BuildCheckPanel.Instance;
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void TankTowerSelected()
    {
        Debug.Log("TankTowerButton clicked");
        // TODO call controller
        // TODO show tower info
        _buildCheckPanel.Appear();
    }
}
