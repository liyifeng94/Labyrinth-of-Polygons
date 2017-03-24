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
    //private string _rejectedCase;
    private float _endTime;
    private bool _autoDisappearSet;
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
        //_rejectedCase = "Request refused. Click another tile or EnterBattle to continue.";
        _autoDisappearSet = false;
    }

    
    void Update()
    {
        if (_autoDisappearSet)
        {
            if (_endTime - Time.time < 0)
            {
                DisAppear();
            }
        }
    }
    

    public void Appear()
    {
        _endTime = Time.time + 3; // auto disappear countdown
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
                FixText.text = "Not enough gold to perform.\nRequest refused.";
                _autoDisappearSet = true;
                break;
            case "Block":
                FixText.text = "Cannot block the last path.\nRequest refused.";
                _autoDisappearSet = true;
                break;
            case "Upgrade":
                FixText.text = _type + _towerOperationReuqst;
                break;
            case "UpgradeWhenDestroied":
                FixText.text = "The selected tower is destroyed. Repair or sell it.\nRequest refused.";
                _autoDisappearSet = true;
                break;
            /*case "MaxLevel":
                FixText.text = "Max level reached.\nPress Yes or No to go back.";
                break;*/
            case "Repair":
                FixText.text = _type + _towerOperationReuqst;
                break;
            case "RepairWithFullHp":
                FixText.text = "Cannot repair a full HP tower.\nRequest refused.";
                _autoDisappearSet = true;
                break;
            case "Remove":
                FixText.text = _type + _towerOperationReuqst;
                break;
            case "Sell":
                FixText.text = _type + _towerOperationReuqst;
                break;
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
