using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum paid_from { wallet, paypal, applestore }
public class ApiValueStore : MonoBehaviour
{
    public static ApiValueStore instance;

    public BestScoreGetData bestScoreGetData;
    public RunningTournamentData runningTournamentData;

    [Header("Runtime add data")]
    // public int currantScore;
    public string token;
    public string userName;
    public int runningTournament_id;
    public int prize_pool;
    public GetTicketsSO getTicketsSO;
    public int is_eligible_for_prize;
    public paid_from paid_From = paid_from.wallet;
    public int selected_ticket_id = 3;
    public float Selected_ticket_Price = 0;
    public float Selected_total_ticket = 0;
    public int remain_ticket;
    public float wallet;
    public bool isPurshceTicket;



    private void Awake()
    {
        if (instance == null) instance = this;
    }
}
