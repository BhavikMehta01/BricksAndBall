using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/PurchaseTicketSO")]
public class PurchaseTicketsSO : ScriptableObject
{
    public int user_id;
    public int tournament_id;
    public int total_ticket;
    public int used_ticket;
    public int remain_ticket;
    public string created_at;
    public string updated_at;

    public int User_id { get => user_id; set => user_id = value; }
    public int Tournament_id { get => tournament_id; set => tournament_id = value; }
    public int Total_ticket { get => total_ticket; set => total_ticket = value; }
    public int Used_ticket { get => used_ticket; set => used_ticket = value; }
    public int Remain_ticket { get => remain_ticket; set => remain_ticket = value; }
    public string Created_at { get => created_at; set => created_at = value; }
    public string Updated_at { get => updated_at; set => updated_at = value; }

    public PurchaseTicketsSO(int user_id, int tournament_id, int total_ticket, int used_ticket, int remain_ticket, string created_at, string updated_at)
    {
        this.user_id = user_id;
        this.tournament_id = tournament_id;
        this.total_ticket = total_ticket;
        this.used_ticket = used_ticket;
        this.remain_ticket = remain_ticket;
        this.created_at = created_at;
        this.updated_at = updated_at;
    }
}
