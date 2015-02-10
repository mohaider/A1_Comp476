using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// The team manager manages various state machines
/// 1. FCSetterSM(flag carrier setter state machine)
/// 2. FlagSaverSetterSM(notify a player to go chase after the flag
/// 3. UntaggerSetterSM(explained below)
/// Furthermore it gets notified by players that they are IsTagged. Once they get IsTagged they get placed into 
/// a linked list data structure. The manager then calls on the UntaggerSM to free them
/// It gets notified by the flag that it has been captured.
/// </summary>
public class TeamManager : MonoBehaviour
{
    #region class variables and properties
    [SerializeField] GameObject _flagCarrier;
    [SerializeField] private GameObject TeamArea;
   
    [SerializeField] GameObject _chaser;
    [SerializeField] private ArrayList teamPool;
    [SerializeField]
    private ArrayList _enemyTeamPool;
    [SerializeField] private GameObject flag;
    private LinkedList<GameObject> taggedPlayers;

    //set handlers and event

    public delegate void FlagCarrierEventHandler(GameObject flag); //the manager gets notified that the flag has been captured

    public static event FlagCarrierEventHandler FlagCarrierEvent;

    public ArrayList TeamPool
    {
        get { return teamPool; }
        set { teamPool = value; }
    }

    public GameObject FlagCarrier
    {
        get { return _flagCarrier; }
        set { _flagCarrier = value; }
    }

    public GameObject Chaser
    {
        get { return _chaser; }
        set { _chaser = value; }
    }

    public GameObject Flag
    {
        get { return flag; }
        set { flag = value; }
    }

    public ArrayList EnemyTeamPool
    {
        get { return _enemyTeamPool; }
        set { _enemyTeamPool = value; }
    }

    #endregion
    # region class functions

    #endregion
    #region unity functions
    // Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
       
    }
    #endregion
}
