using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaymentProgress : MonoBehaviour
{

    private void OnEnable()
    { 
        StartCoroutine(WaitForProcess());
    }


        IEnumerator WaitForProcess()
    {
        yield return new WaitForSecondsRealtime(1.5f);
        CanvasManager.instance.Panel_TicketPurchasePanel.SetActive(true);
        gameObject.SetActive(false);
    }
}
