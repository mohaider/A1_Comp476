using UnityEngine;
using System.Collections;

public class CharacterBoundsController : MonoBehaviour
{
    #region class variables and functions

    [SerializeField] private GameObject homebase;
    [SerializeField] private GameObject enemybase;
    public enum state
    {
        inHomeBase,
        inEnemyBase,
        neither
    }

    [SerializeField]
    private state currentLocation;

    public state CurrentLocation
    {
        get { return currentLocation; }
        set { currentLocation = value; }
    }

    #endregion 
    #region unity functions
    // Use this for initialization

    void Awake()
    {
        currentLocation = state.inHomeBase;
        if (tag == "TeamOrange")
        {
            homebase = GameObject.FindGameObjectWithTag("OrangeAreaTag");
            enemybase = GameObject.FindGameObjectWithTag("BananaAreaTag");
        }
        if (tag == "TeamBanana")
        {
            homebase = GameObject.FindGameObjectWithTag("BananaAreaTag");
            enemybase = GameObject.FindGameObjectWithTag("OrangeAreaTag");
        }

    }

    void Update()
    {
        if (transform.position.z >= 0 && transform.position.z <= 1000
            && transform.position.x >= 0 && transform.position.x <= 600)
        {
            if (tag == "TeamOrange")
            {
                currentLocation = state.inHomeBase;
            }
            if (tag == "TeamBanana")
            {
                currentLocation = state.inEnemyBase;
            }

        }
        else if (transform.position.z >= 0 && transform.position.z <= 1000
          && transform.position.x >= 1000 && transform.position.x <= 1600)
        {
            if (tag == "TeamOrange")
            {
                currentLocation = state.inEnemyBase;
            }
            if (tag == "TeamBanana")
            {
                currentLocation = state.inHomeBase;
            }

        }
        else
        {
            currentLocation = state.neither;

        }
    }
   /* private void OnTriggerStay(Collider col)
    {
        if (col.tag == homebase.tag)
        {
            currentLocation = state.inHomeBase;
            print(name + " is now in home base");
        }
        else if (col.tag == enemybase.tag)
        {
            currentLocation = state.inEnemyBase;
            print(name + " is now in enemy base");
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == homebase.tag)
        {
            currentLocation = state.inHomeBase;
            print(name + " is now in home base");
        }
        else if (col.tag == enemybase.tag)
        {
            currentLocation = state.inEnemyBase;
            print(name+" is now in enemy base");
        }
        
    }
    private void OnTriggerExit(Collider col)
    {
        if (col.tag == homebase.tag || col.tag == enemybase.tag)
        {
            currentLocation = state.neither;
        }
      
        
    }*/

    #endregion
}
