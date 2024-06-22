using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BalancePanelLayout : MonoBehaviour
{
    public Text balance;
    public Text numberOfTicket;
    public Text ticketPrice;
    public Text newBalance;

    float countNewBalance;
    // Start is called before the first frame update
    void Start()
    {
        countNewBalance =  ApiValueStore.instance.wallet - ApiValueStore.instance.Selected_ticket_Price;
        balance.text = "$ " + ApiValueStore.instance.wallet.ToString();
        numberOfTicket.text = ApiValueStore.instance.Selected_total_ticket.ToString() + " Tickets";
        ticketPrice.text = "$ " + ApiValueStore.instance.Selected_ticket_Price.ToString();
        newBalance.text = "$ " + countNewBalance.ToString();
    }

 }
