using UnityEngine;
using System.Collections;
/// <summary>
/// this class will check to see if the current gameobject is the flag carrier
/// </summary>
public class FlagCarrierSM : MonoBehaviour
{

    #region class variables and properties

    [SerializeField] private bool hasFlag =false;
    [SerializeField] private bool isAnFC =false;
    [SerializeField] private bool touchingFlag = false;

    public bool IsAnFc
    {
        get { return isAnFC; }
        set { isAnFC = value; }
    }

    #endregion

    #region class functions

    #endregion 

    #region unity functions
    // Use this for initialization
	void Start () {
	
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
                    hasFlag = true;
                    //notify the team manager
                }
            }
            if (tag == "TeamOrange")
            {
                if (col.tag == "Banana")
                {
                    hasFlag = true;
                    //notify the team manager
                }

            }
        }
    }

    #endregion
}
