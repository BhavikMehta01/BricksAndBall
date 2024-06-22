using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TicketPurchasedPanel : MonoBehaviour
{
    [SerializeField] Text msg;
    // Start is called before the first frame update
    void Start()
    {
        msg.text = "You've purchased " + ApiValueStore.instance.Selected_total_ticket + " tickets";
    }

    public void ClickOkBtn()
    {
        ApiValueStore.instance.isPurshceTicket = true;
        CanvasManager.instance.leaderBoardUI.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }
}
