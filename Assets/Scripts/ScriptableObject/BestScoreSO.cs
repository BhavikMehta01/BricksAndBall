using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/BestScoreOS")]
public class BestScoreSO : ScriptableObject
{
    public int is_my_best;
    public int is_eligible_for_prize;
    public int is_best;
    public int best_score;

    public List<Best3Score> best3Score;
    public List<Top4Score> top4Score;

    public int Is_my_best { get => is_my_best; set => is_my_best = value; }
    public int Is_eligible_for_prize { get => is_eligible_for_prize; set => is_eligible_for_prize = value; }
    public int Is_best { get => is_best; set => is_best = value; }
    public int Best_score { get => best_score; set => best_score = value; }
    public List<Best3Score> Best3Score { get => best3Score; set => best3Score = value; }
    public List<Top4Score> Top4Score { get => top4Score; set => top4Score = value; }

    public BestScoreSO(int is_my_best, int is_eligible_for_prize, int is_best, int best_score, List<Best3Score> best3Score, List<Top4Score> top4Score)
    {
        this.is_my_best = is_my_best;
        this.is_eligible_for_prize = is_eligible_for_prize;
        this.is_best = is_best;
        this.best_score = best_score;
        this.best3Score = best3Score;
        this.top4Score = top4Score;
    }
}

/// <summary>
/// Best 3 score
/// </summary>
[System.Serializable]
public class Best3Score
{
    [SerializeField] string score;
    [SerializeField] string timetaken;
    [SerializeField] int tournament_id;
    [SerializeField] int user_id;
    [SerializeField] string username;
    [SerializeField] int present;
    [SerializeField] int rank;
    [SerializeField] int prize;

    public Best3Score(string score, string timetaken, int tournament_id, int user_id, string username, int present, int rank, int prize)
    {
        this.score = score;
        this.timetaken = timetaken;
        this.tournament_id = tournament_id;
        this.user_id = user_id;
        this.username = username;
        this.present = present;
        this.rank = rank;
        this.prize = prize;
    }

    public string Score { get => score; set => score = value; }
    public string Timetaken { get => timetaken; set => timetaken = value; }
    public int Tournament_id { get => tournament_id; set => tournament_id = value; }
    public int User_id { get => user_id; set => user_id = value; }
    public string Username { get => username; set => username = value; }
    public int Present { get => present; set => present = value; }
    public int Rank { get => rank; set => rank = value; }
    public int Prize { get => prize; set => prize = value; }
}

/// <summary>
/// Top 4 score
/// </summary>

[System.Serializable]
public class Top4Score
{
    [SerializeField] string score;
    [SerializeField] string timetaken;
    [SerializeField] int tournament_id;
    [SerializeField] int user_id;
    [SerializeField] string username;
    [SerializeField] int present;
    [SerializeField] int rank;
    [SerializeField] int prize;

    public Top4Score(string score, string timetaken, int tournament_id, int user_id, string username, int present, int rank, int prize)
    {
        this.score = score;
        this.timetaken = timetaken;
        this.tournament_id = tournament_id;
        this.user_id = user_id;
        this.username = username;
        this.present = present;
        this.rank = rank;
        this.prize = prize;
    }

    public string Score { get => score; set => score = value; }
    public string Timetaken { get => timetaken; set => timetaken = value; }
    public int Tournament_id { get => tournament_id; set => tournament_id = value; }
    public int User_id { get => user_id; set => user_id = value; }
    public string Username { get => username; set => username = value; }
    public int Present { get => present; set => present = value; }
    public int Rank { get => rank; set => rank = value; }
    public int Prize { get => prize; set => prize = value; }
}