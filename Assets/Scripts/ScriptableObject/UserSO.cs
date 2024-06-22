using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/UserOS")]
public class UserSO : ScriptableObject
{
    [SerializeField] int user_id;
    [SerializeField] string username;
    [SerializeField] string email;
    [SerializeField] string wallet;
    [SerializeField] string sound;
    [SerializeField] tickets tickets;
    [SerializeField] string message;
    [SerializeField] string token;

    public int User_id { get => user_id; set => user_id = value; }
    public string Username { get => username; set => username = value; }
    public string Email { get => email; set => email = value; }
    public string Wallet { get => wallet; set => wallet = value; }
    public string Sound { get => sound; set => sound = value; }
    public tickets Tickets { get => tickets; set => tickets = value; }
    public string Message { get => message; set => message = value; }
    public string Token { get => token; set => token = value; }

    public UserSO(int user_id, string username, string email, string wallet, string sound, tickets tickets, string message, string token = null)
    {
        this.user_id = user_id;
        this.username = username;
        this.email = email;
        this.wallet = wallet;
        this.sound = sound;
        this.tickets = tickets;
        this.message = message;
        this.token = token;
    }
}

[System.Serializable]
public class tickets
{
    [SerializeField] int total_ticket;
    [SerializeField] int used_ticket;
    [SerializeField] int remain_ticket;

    public int Total_ticket { get => total_ticket; set => total_ticket = value; }
    public int Used_ticket { get => used_ticket; set => used_ticket = value; }
    public int Remain_ticket { get => remain_ticket; set => remain_ticket = value; }

    public tickets(int total_ticket, int used_ticket, int remain_ticket)
    {
        this.total_ticket = total_ticket;
        this.used_ticket = used_ticket;
        this.remain_ticket = remain_ticket;
    }
}