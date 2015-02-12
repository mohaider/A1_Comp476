using UnityEngine;
using System.Collections;

public class ScorePoint : MonoBehaviour
{
    #region class variables

    [SerializeField] private GameObject enemyFlag;
    [SerializeField] private ScoreKeeping scoreKeeper;
    
    #endregion


    #region unity functions
    // Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

    }

    void OnTriggerEnter(Collider col)
    {
        if (tag == "TeamOrangeFlagArea")
        {
            if (col.gameObject.tag == "TeamOrange")
            {
                if (col.gameObject.GetComponent<FlagCarrierSM>().HasFlag) //has the enemy flag
                {
                    col.gameObject.GetComponent<FlagCarrierSM>().Reset();
                    enemyFlag.GetComponent<FlagScript>().ResetToHomePosition();
                    scoreKeeper.OrangeTeamScore++;
                }
            }

        }
        if (tag == "TeamBananaFlagArea")
        {
            if (col.gameObject.tag == "TeamBanana")
            {
                if (col.gameObject.GetComponent<FlagCarrierSM>().HasFlag) //has the enemy flag
                {
                    col.gameObject.GetComponent<FlagCarrierSM>().Reset();
                    enemyFlag.GetComponent<FlagScript>().ResetToHomePosition();
                    scoreKeeper.BananaTeamScore++;
                }
            }
        }
    }
    #endregion
}
