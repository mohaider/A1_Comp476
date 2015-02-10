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

    [SerializeField] private float timeBeforeDistCheck;
    [SerializeField] private float timerReset;
    [SerializeField]
    private float maxDistance;
    [SerializeField]
    private float SpeedUpFactor;
    private ArrayList teamPool;
    private ArrayList enemyPool;
    
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
                    break;

                }

            }
        }
        if (flagCarrier != null)
        {
            if (flagCarrier.GetComponent<PlayerStateListener>().CurrentState !=
                PlayerStateController.PlayerState.holdingFlag)
            {
                if (timeBeforeDistCheck <= 0f)
                {
                    GameObject enemy = FindClosestEnemy();
                    if (enemy != null) //found enemy!
                    {
                        if (flagCarrier.GetComponent<PlayerStateListener>().CurrentState !=
                            PlayerStateController.PlayerState.EscapingState)
                        {
                            if (flagCarrier.GetComponent<MovementBehaviour>() == null)
                                Debug.Log("no movement behaviour is set to the flagcarrier " + flagCarrier.name);
                            else
                            {
                                float desiredSpeed = flagCarrier.GetComponent<MovementBehaviour>().MaxSpeed*
                                                     SpeedUpFactor;
                                flagCarrier.GetComponent<MovementBehaviour>().MaxSpeed = desiredSpeed; //increase max speed
                                flagCarrier.GetComponent<PlayerStateListener>().TargetAgent = enemy;
                                flagCarrier.GetComponent<PlayerStateController>().ChangeState(PlayerStateController.PlayerState.EscapingState); //change the state to escape
                            }
                        }
                    }
                    else if (flagCarrier.GetComponent<FlagCarrierSM>().HasFlag ) //no enemies close by and he has the flag
                    {
                        float desiredSpeed = flagCarrier.GetComponent<MovementBehaviour>().MaxSpeed / SpeedUpFactor;
                        flagCarrier.GetComponent<MovementBehaviour>().MaxSpeed = desiredSpeed; //increase max speed
                        flagCarrier.GetComponent<PlayerStateListener>().TargetAgent = homeBase;
                        flagCarrier.GetComponent<PlayerStateController>().ChangeState(PlayerStateController.PlayerState.chasing);

                    }

                    timeBeforeDistCheck = timerReset;
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
        for (int i = 0; i < enemyPool.Count; i++)
        {
            GameObject enemy = (GameObject) enemyPool[i];
            Vector3 direction = enemy.transform.position - flagCarrier.transform.position;
            direction.y = 0;//flatten y
            float squareMag = direction.sqrMagnitude;
            if (squareMag < maxDistance*maxDistance)
            {
                return enemy;
                
            }

        }
        return null;
    }

    #endregion
}
