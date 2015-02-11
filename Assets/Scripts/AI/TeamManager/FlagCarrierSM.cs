using UnityEngine;
using System.Collections;
using Assets.Scripts.Character;

/// <summary>
/// this class will check to see if the current gameobject is the flag carrier
/// </summary>
public class FlagCarrierSM : MonoBehaviour
{

    #region class variables and properties

    [SerializeField] private bool hasFlag =false;
    [SerializeField] private bool isAnFC =false;
    [SerializeField] private bool touchingFlag = false;
    [SerializeField] private GameObject flagPlacement;
    [SerializeField] private GameObject homeBase;
    private FCSetterSM _FCSetterManager;

    public bool IsAnFc
    {
        get { return isAnFC; }
        set { isAnFC = value; }
    }

    public bool HasFlag
    {
        get { return hasFlag; }
        set { hasFlag = value; }
    }

    #endregion

    #region class functions

    #endregion 

    #region unity functions
    // Use this for initialization
	void Start () {
	
	}

    void Awake()
    {
        if (tag == "TeamOrange")
        {
            GameObject manager = GameObject.FindGameObjectWithTag("TeamOrangeManager");
            _FCSetterManager = manager.GetComponent<FCSetterSM>();
           
        }
        else
        {
            GameObject manager = GameObject.FindGameObjectWithTag("TeamBananaManager");
            _FCSetterManager = manager.GetComponent<FCSetterSM>();
        }
    }
	// Update is called once per frame
	void Update () {
	    if (isAnFC) //is a flag carrier
	    {
	        if (!hasFlag)
	        {

	        }
	    }

	}

    void OnTriggerEnter(Collider col)
    {
        if (isAnFC && !hasFlag)
        {
            if (tag == "TeamBanana")
            {
                if (col.tag == "Orange")
                {
                    
                    //take the flag
                    FlagScript flagScript = col.GetComponent<FlagScript>();
                    if (flagScript ==null)
                        Debug.Log("the flag doesn't have a flag script!");
                    flagScript.PickupFlag(flagPlacement);
                    
                    //notify the team manager.

                        hasFlag = true;
                }

            }
            if (tag == "TeamOrange")
            {
                if (col.tag == "Banana")
                {
                    //take the flag
                    FlagScript flagScript = col.GetComponent<FlagScript>();
                    if (flagScript == null)
                        Debug.Log("the flag doesn't have a flag script!");
                    flagScript.PickupFlag(flagPlacement);
                    
                    hasFlag = true;
                 
                    //notify the team manager
                }

            }
        }
    }

    public void TagThisFC()
    {
        hasFlag = false;
        isAnFC = false;
        gameObject.GetComponent<PlayerStateListener>().TargetAgent = null;
        gameObject.GetComponent<PlayerStateController>().ChangeState(PlayerStateController.PlayerState.IsTagged);
        //tell the FCSetterSM that we no longer have an FC
        _FCSetterManager.UnsetFC();
    }

    #endregion
}
