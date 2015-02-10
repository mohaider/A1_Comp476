using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TeamManager : MonoBehaviour
{
    #region class variables and properties
    [SerializeField] GameObject _flagCarrier;
    [SerializeField] GameObject _chaser;
    [SerializeField] private ArrayList teamPool;
    [SerializeField] private GameObject flag;
    private LinkedList<GameObject> taggedPlayers;

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
