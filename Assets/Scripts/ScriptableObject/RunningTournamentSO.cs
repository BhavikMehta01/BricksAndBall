using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/RunningTournamentSO")]
public class RunningTournamentSO : ScriptableObject
{
    public int id;
    public string name;
    public string start_time;
    public string end_time;
    public string total_duration;
    public int ticket_used;
    private int prize_pool;
    // public List<Prizepoll> prizepoll;

    public int Id { get => id; set => id = value; }
    public string Name { get => name; set => name = value; }
    public string Start_time { get => start_time; set => start_time = value; }
    public string End_time { get => end_time; set => end_time = value; }
    public string Total_duration { get => total_duration; set => total_duration = value; }
    public int Ticket_used { get => ticket_used; set => ticket_used = value; }
    //public List<Prizepoll> PrizeOoll { get => prizepoll; set => prizepoll = value; }
    public int Prize_pool { get => prize_pool; set => prize_pool = value; }

    public RunningTournamentSO(int id, string name, string start_time, string end_time, string total_duration, int ticket_used, int prizePoll)
    {
        this.id = id;
        this.name = name;
        this.start_time = start_time;
        this.end_time = end_time;
        this.total_duration = total_duration;
        this.ticket_used = ticket_used;
        this.prize_pool = prizePoll;
    }
}

/// <summary>
/// Best 3 score
/// </summary>
[System.Serializable]
public class Prizepoll
{
    [SerializeField] int id;
    [SerializeField] int tournament_id;
    [SerializeField] string rank;
    [SerializeField] string prize;
    [SerializeField] string created_at;
    [SerializeField] string updated_at;

    public Prizepoll(int id, int tournament_id, string rank, string prize, string created_at, string updated_at)
    {
        this.id = id;
        this.tournament_id = tournament_id;
        this.rank = rank;
        this.prize = prize;
        this.created_at = created_at;
        this.updated_at = updated_at;
    }

    public int Id { get => id; set => id = value; }
    public int Tournament_id { get => tournament_id; set => tournament_id = value; }
    public string Rank { get => rank; set => rank = value; }
    public string Prize { get => prize; set => prize = value; }
    public string Created_at { get => created_at; set => created_at = value; }
    public string Updated_at { get => updated_at; set => updated_at = value; }
}