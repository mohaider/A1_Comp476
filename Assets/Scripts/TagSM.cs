using UnityEngine;
using System.Collections;
using Assets.Scripts.Character;

public class TagSM : MonoBehaviour
{

    [SerializeField] private bool isTagger=false;
    [SerializeField] private GameObject enemy;
    [SerializeField] private GameObject ourFlag;
    private FlagSaverSetterSM _taggingManager;

    public bool IsTagger
    {
        get { return isTagger; }
        set { isTagger = value; }
    }

    public GameObject Enemy
    {
        get { return enemy; }
        set { enemy = value; }
    }
    #region unity functions
    // Use this for initialization
	void Awake ()
	{
	    if (tag == "TeamOrange")
	    {
	        GameObject manager = GameObject.FindGameObjectWithTag("TeamOrangeManager");
            _taggingManager = manager.GetComponent<FlagSaverSetterSM>();
	        ourFlag = GameObject.FindGameObjectWithTag("Orange");
	    }
        else
        {
            GameObject manager = GameObject.FindGameObjectWithTag("TeamBananaManager");
            _taggingManager = manager.GetComponent<FlagSaverSetterSM>();
            ourFlag = GameObject.FindGameObjectWithTag("Banana");
        }
	}
	
	// Update is called once per frame
	void Update () {

    }

    void OnCollisionEnter(Collision collision)
    {
        if (isTagger)
        {
            if (collision.gameObject.GetInstanceID() == enemy.GetInstanceID()) //we're colliding with the enemy
            {
                collision.gameObject.GetComponent<FlagCarrierSM>().TagThisFC(); //tell the game object it is tagged and unset it.
                //collision.gameObject.GetComponent<PlayerStateController>().ChangeState(PlayerStateController.PlayerState.IsTagged);
                ourFlag.GetComponent<FlagScript>().ResetToHomePosition(); //reset flag to home positiion
                _taggingManager.UnsetTagger();

            }
        }
    }
    #endregion
}
