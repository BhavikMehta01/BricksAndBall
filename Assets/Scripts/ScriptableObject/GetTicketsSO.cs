using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/GetTicketsSO")]
public class GetTicketsSO : ScriptableObject
{
    public int status;
    public List<Data> data;

    public int Status { get => status; set => status = value; }
    public List<Data> Data { get => data; set => data = value; }

    public GetTicketsSO(int status, List<Data> data)
    {
        this.status = status;
        this.data = data;
    }
}

/// <summary>
/// Best 3 score
/// </summary>
[System.Serializable]
public class Data
{
    [SerializeField] int id;
    [SerializeField] int total_ticket;
    [SerializeField] int total_price;
    [SerializeField] string created_at;
    [SerializeField] int is_best;

    public Data(int id, int total_ticket, int total_price, string created_at, int is_best)
    {
        this.id = id;
        this.total_ticket = total_ticket;
        this.total_price = total_price;
        this.created_at = created_at;
        this.is_best = is_best;
    }

    public int Id { get => id; set => id = value; }
    public int Total_ticket { get => total_ticket; set => total_ticket = value; }
    public int Total_price { get => total_price; set => total_price = value; }
    public string Created_at { get => created_at; set => created_at = value; }
    public int Is_best { get => is_best; set => is_best = value; }
}