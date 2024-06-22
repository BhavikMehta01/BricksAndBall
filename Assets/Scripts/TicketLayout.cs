using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TicketLayout : MonoBehaviour
{
    public Button spriteBtn;
    public Text amount;
    public Text numberofTicket;
    public GameObject checkBox;
    public GameObject bestValue;

    public void SetTicketValues(int id, Sprite _sprite, float price, int numberOfTicket,int isbest)
    {
        spriteBtn.GetComponent<Image>().sprite = _sprite;
        amount.text = "$." + price.ToString();
        numberofTicket.text = numberOfTicket.ToString();
        if (isbest == 1) bestValue.SetActive(true);
        spriteBtn.onClick.AddListener(() => { SaveSelectedTicketValue(id, price, numberOfTicket);});

    }

    public void SaveSelectedTicketValue(int _id, float _price,int _numberOfTicket)
    {
        ApiValueStore.instance.selected_ticket_id = _id;
        ApiValueStore.instance.Selected_ticket_Price = _price;
        ApiValueStore.instance.Selected_total_ticket = _numberOfTicket;
        transform.GetComponentInParent<PurchaseTicket>().SetCheckBoxPosition(transform);
       // CanvasManager.instance.balancePanel.SetActive(true);
       // transform.parent.gameObject.SetActive(false);
    }
}
