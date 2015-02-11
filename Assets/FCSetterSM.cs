using System;
using UnityEngine;
using System.Collections;
using Assets.Scripts.Character;

public class FCSetterSM : MonoBehaviour
{
    #region class variables and properties
    [SerializeField]
    private GameObject enemyFlag;
    [SerializeField]
    private GameObject homeBase;
    [SerializeField]
    private GameObject flagCarrier;

    [SerializeField]
    private float timeBeforeDistCheck;
    [SerializeField]
    private float timerReset;
    [SerializeField]
    private float maxDistance;
    [SerializeField]
    private float SpeedUpFactor;

    private float originalSpeed;
    private ArrayList teamPool;
    private ArrayList enemyPool;

    public GameObject FlagCarrier
    {
        get { return flagCarrier; }
        set { flagCarrier = value; }
    }

    #endregion


    #region unity functions
    // Use this for initialization
    void Start()
    {
        teamPool = gameObject.GetComponent<TeamManager>().TeamPool;
        enemyPool = gameObject.GetComponent<TeamManager>().EnemyTeamPool;
    }

    // Update is called once per frame
    void Update()
    {

        if (flagCarrier == null)
        {
            for (int i = 0; i < teamPool.Count; i++)
            {
                GameObject player = teamPool[i] as GameObject;
                if (player.GetComponent<PlayerStateListener>().CurrentState != PlayerStateController.PlayerState.idle)
                {
                    continue;
                }
                if (player.GetComponent<CharacterBoundsController>().CurrentLocation !=
                    CharacterBoundsController.state.inHomeBase)
                {
                    continue;
                }
                else //the player is the flag carrier
                {
                    flagCarrier = player;
                    flagCarrier.GetComponent<FlagCarrierSM>().IsAnFc = true;
                    player.GetComponent<PlayerStateListener>().TargetAgent = enemyFlag;
                    player.GetComponent<PlayerStateController>().ChangeState(PlayerStateController.PlayerState.chasing);
                    originalSpeed = flagCarrier.GetComponent<MovementBehaviour>().MaxSpeed;                        //set the original speed
                    break;

                }

            }
        }
        if (flagCarrier != null)
        {
            if (flagCarrier.GetComponent<FlagCarrierSM>().HasFlag)
            {

                if (timeBeforeDistCheck <= 0f) //are there any enemies close by? 
                {

                    if (flagCarrier.GetComponent<CharacterBoundsController>().CurrentLocation ==
                         CharacterBoundsController.state.inEnemyBase)  //is he in the enemy location?
                    {
                        GameObject enemy = FindClosestEnemy();
                        if (enemy != null) //yes, found a nearby enemy
                        {
                            if (flagCarrier.GetComponent<PlayerStateListener>().CurrentState !=
                                //is his state set to escaping already?
                                PlayerStateController.PlayerState.EscapingState)
                            {
                                if (flagCarrier.GetComponent<MovementBehaviour>() == null)
                                    Debug.Log("no movement behaviour is set to the flagcarrier " + flagCarrier.name);
                                else //set it to escape from the player
                                {
                                    /*
                                                                    float desiredSpeed = flagCarrier.GetComponent<MovementBehaviour>().MaxSpeed*
                                                                                         SpeedUpFactor;*/

                                    float desiredSpeed = originalSpeed * SpeedUpFactor;
                                    flagCarrier.GetComponent<MovementBehaviour>().MaxSpeed = desiredSpeed;
                                    //increase max speed
                                    flagCarrier.GetComponent<PlayerStateListener>().TargetAgent = enemy;
                                    flagCarrier.GetComponent<PlayerStateController>()
                                        .ChangeState(PlayerStateController.PlayerState.EscapingState);
                                    //change the state to escape
                                }
                            }
                        }
                        else
                        //if (flagCarrier.GetComponent<FlagCarrierSM>().HasFlag ) //no enemies close by and he has the flag
                        {
                            // float desiredSpeed = flagCarrier.GetComponent<MovementBehaviour>().MaxSpeed / SpeedUpFactor;
                            float desiredSpeed = originalSpeed;
                            flagCarrier.GetComponent<MovementBehaviour>().MaxSpeed = desiredSpeed; //increase max speed
                            flagCarrier.GetComponent<PlayerStateListener>().TargetAgent = homeBase;
                            flagCarrier.GetComponent<PlayerStateController>()
                                .ChangeState(PlayerStateController.PlayerState.chasing);

                        }

                        timeBeforeDistCheck = timerReset;
                    }
                }

                if (flagCarrier.GetComponent<CharacterBoundsController>().CurrentLocation !=
                    CharacterBoundsController.state.inEnemyBase) //is he not in the enemy location?
                {
                    float desiredSpeed = originalSpeed;
                    flagCarrier.GetComponent<MovementBehaviour>().MaxSpeed = desiredSpeed; //increase max speed
                    flagCarrier.GetComponent<PlayerStateListener>().TargetAgent = homeBase;
                    flagCarrier.GetComponent<PlayerStateController>()
                        .ChangeState(PlayerStateController.PlayerState.chasing);
                }

            }
        }

        timeBeforeDistCheck -= Time.fixedDeltaTime;
    }
    #endregion

    #region class functions
    /// <summary>
    /// find the closest enemy within the predefined max distance 
    /// </summary>
    private GameObject FindClosestEnemy()
    {
        Debug.Log("THIS ISNT SORTING DIDLY!");
        float[] sortedDistances = new float[enemyPool.Count];
        for (int i = 0; i < enemyPool.Count; i++)
        {
            
            GameObject enemy = (GameObject)enemyPool[i];
            Vector3 direction = enemy.transform.position - flagCarrier.transform.position;
            direction.y = 0;//flatten y
            float squareMag = direction.sqrMagnitude;
            sortedDistances[i] = squareMag;

        }
        Array.Sort(sortedDistances);

        if (sortedDistances[0] < maxDistance*maxDistance)
            return (GameObject) enemyPool[0];
        
        return null;
    }

    #endregion
}
