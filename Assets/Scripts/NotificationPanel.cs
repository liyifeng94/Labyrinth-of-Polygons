using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NotificationPanel : MonoBehaviour {

    public static NotificationPanel Instance;
    public GameObject ThisPanel;
    public Text FixText;
    //public Text AutoDisappearText;
    private string _type;
    private string _towerOperationReuqst;
    private string _rejectedCase;
    //public float StartTime, EndTime;

    void Awake()
    {
        Instance = this;
    }


    void Start()
    {
        ThisPanel.SetActive(false);
        _type = "Nop";
        _towerOperationReuqst = " request received.\nPress Yes to confirm.\nPress No to refuse.";
        _rejectedCase = "Request refused. Click another tile or EnterBattle to continue.";
    }

    /*
    void update()
    {
        //if ()
    }
    */

    public void Appear()
    {
        //StartTime = Time.time;
        ThisPanel.SetActive(true);
        switch (_type)
        {
            case "Nop":
                break;
            case "Tank":
                FixText.text = "Tank Tower creation" + _towerOperationReuqst;
                break;
            case "Range":
                FixText.text = "Range Tower creation" + _towerOperationReuqst;
                break;
            case "Slow":
                FixText.text = "Slow Tower creation" + _towerOperationReuqst;
                break;
            case "Heal":
                FixText.text = "Heal Tower creation" + _towerOperationReuqst;
                break;
            case "Money":
                FixText.text = "Gold Tower creation" + _towerOperationReuqst;
                break;
            case "NotEnoughMoney":
                FixText.text = "Not enough gold to build.\n" + _rejectedCase;
                break;
            case "Block":
                FixText.text = "You were tried to build a tower that blocks the last path.\n" + _rejectedCase;
                break;
            case "Upgrade":
                FixText.text = _type + _towerOperationReuqst;
                break;
            case "UpgradeWhenDestroied":
                FixText.text = "The selected tower is destroyed. You can only repair or sell it.\n" + _rejectedCase;
                break;
            case "MaxLevel":
                FixText.text = "Max level reached.\nPress Yes or No to go back.";
                break;
            case "Repair":
                FixText.text = _type + _towerOperationReuqst;
                break;
            case "RepairWithFullHp":
                FixText.text = "You were tried to repair a full HP tower.\n" + _rejectedCase;
                break;
            case "Remove":
                FixText.text = _type + _towerOperationReuqst;
                break;
            case "Sell":
                FixText.text = _type + _towerOperationReuqst;
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
