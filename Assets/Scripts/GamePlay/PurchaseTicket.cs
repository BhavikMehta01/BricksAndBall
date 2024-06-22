using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurchaseTicket : MonoBehaviour
{
    [SerializeField] GameObject _ticketPrefab;
    [SerializeField] Transform parents;
    [SerializeField] Sprite[] sprites;
    [SerializeField] Transform checkBox;

    private void OnEnable()
    {
        TicketGanaret();
    }

    public void TicketGanaret()
    {
        if(parents.childCount > 0)
        {
            for (int i = 0; i < parents.childCount; i++)
            {
                Destroy(parents.GetChild(i).gameObject);
            }
        }
        for (int i = 0; i < ApiValueStore.instance.getTicketsSO.data.Count; i++)
        {
            GameObject _ticket = Instantiate(_ticketPrefab, parents);
            TicketLayout ticketLayout = _ticket.GetComponent<TicketLayout>();
            ticketLayout.SetTicketValues(ApiValueStore.instance.getTicketsSO.data[i].Id,sprites[i], 
                (float)ApiValueStore.instance.getTicketsSO.data[i].Total_price,
                ApiValueStore.instance.getTicketsSO.data[i].Total_ticket,
                ApiValueStore.instance.getTicketsSO.data[i].Is_best);
           /* ticketLayout.spriteImage.sprite = sprites[i];
            ticketLayout.amount.text = "$." + ApiValueStore.instance.getTicketsSO.data[i].Total_price.ToString();
            ticketLayout.numberofTicket.text = ApiValueStore.instance.getTicketsSO.data[i].Total_ticket.ToString();*/
        }
    }

    public void SetCheckBoxPosition(Transform btnTransfrom)
    {
        checkBox.gameObject.SetActive(true);
        checkBox.localPosition = new Vector3(btnTransfrom.localPosition.x, checkBox.localPosition.y, 0);
    }
}