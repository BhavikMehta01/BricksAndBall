using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerObjectManager : MonoBehaviour
{
    public static PlayerObjectManager instance;

    public GameObject powerBox;
    public ShootManager shootManager;
    public bool isPowerActive;
    public bool isPlayerPowerOn;
    public bool isPlayerSpeedUp;
    public bool isPlayerDouble;

    public bool isBoxSpawn;
    public List<GameObject> boxManagers = new List<GameObject>();
    public GameObject bullateBlastPrefab;

    [Header("Power Canvas")]
    public GameObject powerCanavs;
    public TextMeshProUGUI redBoxtext;
    public TextMeshProUGUI speedtext;
    public TextMeshProUGUI doubletext; 

    private void Awake()
    {
        if (instance == null) instance = this;
    }
}
