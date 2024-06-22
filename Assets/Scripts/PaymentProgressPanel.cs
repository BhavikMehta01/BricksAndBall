using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaymentProgressPanel : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadTicketValueAfterPurchased());
    }


    IEnumerator LoadTicketValueAfterPurchased()
    {
        yield return new WaitForSeconds(1.5f);
        CanvasManager.instance.Panel_TicketPurchasePanel.SetActive(true);
        gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
