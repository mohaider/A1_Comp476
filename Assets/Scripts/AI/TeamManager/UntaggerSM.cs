using UnityEngine;
using System.Collections;
using Assets.Scripts.Character;

public class UntaggerSM : MonoBehaviour
{
    #region class variables and functions
    [SerializeField] private GameObject playerToUntag;
    [SerializeField]
    private bool _untagging = false;

    private UntaggerSetterSM untagManager; 

    #endregion


    #region unity functions
    public GameObject PlayerToUntag
    {
        get { return playerToUntag; }
        set { playerToUntag = value; }
    }

    public bool Untagging
    {
        get { return _untagging; }
        set { _untagging = value; }
    }

    // Use this for initialization
	void Start () {
        GameObject manager;
        if (tag == "TeamOrange")
        {
            manager = GameObject.FindGameObjectWithTag("TeamOrangeManager");
        }
        else
        {
            manager = GameObject.FindGameObjectWithTag("TeamBananaManager");
        }
        untagManager = manager.GetComponent<UntaggerSetterSM>();
	}
	
	// Update is called once per frame
	void Update () {

    }

    void OnCollisionEnter(Collision col)
    {
        if (Untagging)
        {
            if (col.gameObject == playerToUntag)
            {
                playerToUntag.GetComponent<PlayerStateController>().ChangeState(PlayerStateController.PlayerState.idle);
                untagManager.RemoveTaggedPlayer(playerToUntag);
                playerToUntag = null; //unset this player
                Untagging = false;
                gameObject.GetComponent<PlayerStateController>().ChangeState(PlayerStateController.PlayerState.idle);

            }
        }
    }

    #endregion

    #region class functions
    /// <summary>
    /// sets the primary target to save
    /// </summary>
    /// <param name="target"></param>
    public void Set(GameObject target)
    {
        PlayerToUntag = target;
        gameObject.GetComponent<PlayerStateListener>().TargetAgent = PlayerToUntag;
        gameObject.GetComponent<PlayerStateController>().ChangeState(PlayerStateController.PlayerState.untagger);
        _untagging = true;

    }
    /// <summary>
    /// unsets the target 
    /// </summary>
    public void Unset()
    {
        PlayerToUntag = null;
        gameObject.GetComponent<PlayerStateListener>().TargetAgent = null; //unset
        gameObject.GetComponent<PlayerStateController>().ChangeState(PlayerStateController.PlayerState.idle); //wander
        _untagging = false;
    }

    #endregion
}
