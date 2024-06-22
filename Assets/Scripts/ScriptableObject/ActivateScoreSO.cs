using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/ActivateScoreSO")]
public class ActivateScoreSO : ScriptableObject
{
    public int status;
    public bool success;
    public string message;

    public int Status { get => status; set => status = value; }
    public bool Success { get => success; set => success = value; }
    public string Message { get => message; set => message = value; }

    public ActivateScoreSO(int status, bool success, string message)
    {
        this.status = status;
        this.success = success;
        this.message = message;
    }
}
