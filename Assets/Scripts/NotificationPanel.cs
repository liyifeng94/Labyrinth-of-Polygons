using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NotificationPanel : MonoBehaviour {

    public static NotificationPanel Instance;
    public GameObject ThisPanel;
    public Text FixText;
    //public Text AutoDisappearText;
    private string _type;
    private string towerOperationReuqst;

    void Awake()
    {
        Instance = this;
    }


    void Start()
    {
        ThisPanel.SetActive(false);
        _type = "Nop";
        towerOperationReuqst = " request received.\nPress Yes to confirm.\nPress No to refuse.";
    }


    public void Appear()
    {
        ThisPanel.SetActive(true);
        switch (_type)
        {
            case "Nop":
                break;
            case "Tank":
                FixText.text = "Tank Tower creation" + towerOperationReuqst;
                break;
            case "NotEnoughMoney":
                FixText.text = "Not enough gold to build.\nClick another tile or EnterBattle to continue.";
                break;
            case "Block":
                FixText.text = "You were tried to build a tower that blocks the last path.\nClick another tile or EnterBattle to continue.";
                break;
            case "Upgrade":
                FixText.text = _type + towerOperationReuqst;
                break;
            case "MaxLevel":
                FixText.text = "Max level reached.\nPress Yes or No to go back.";
                break;
            case "Repair":
                FixText.text = _type + towerOperationReuqst;
                break;
            case "Remove":
                FixText.text = _type + towerOperationReuqst;
                break;
            case "Sell":
                FixText.text = _type + towerOperationReuqst;
                break;


            // TODO: stiwch case for block cases
        }
    }


    public void DisAppear()
    {
        ThisPanel.SetActive(false);
    }


    public void SetNotificationType(string type)
    {
        _type = type;
    }
}
