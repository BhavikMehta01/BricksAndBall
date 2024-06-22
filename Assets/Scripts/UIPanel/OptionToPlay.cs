using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionToPlay : MonoBehaviour
{
    [SerializeField] Text amount;
    [SerializeField] Text avilableTicket;

    private void OnEnable()
    {
        amount.text = ApiValueStore.instance.wallet.ToString();
        avilableTicket.text = ApiValueStore.instance.remain_ticket.ToString();
    }
}