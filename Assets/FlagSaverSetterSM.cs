using System;
using UnityEngine;
using System.Collections;
using Assets.Scripts.Character;

public class FlagSaverSetterSM : MonoBehaviour
{
    #region class variables and properties
    [SerializeField]
    private GameObject flag;
    [SerializeField]
    private GameObject tagger;
    [SerializeField]
    private float maxDistance;
    private ArrayList teamPool;
    private FlagScript flagscript;
    public GameObject Tagger
    {
        get { return tagger; }
        set { tagger = value; }
    }

    #endregion

    // Use this for initialization
    void Start()
    {
        flagscript = flag.GetComponent<FlagScript>();
        teamPool = gameObject.GetComponent<TeamManager>().TeamPool;
    }

    // Update is called once per frame
    void Update()
    {
        bool ourFlagIsTaken = flagscript.flagState == FlagScript.FlagState.Taken;



        if (ourFlagIsTaken)
        {
            GameObject enemy = flagscript.EnemyCarrier; //if the flag is taken then it must be that the enemy has attached it
            if (tagger) //there is a tagger
            {
                if (enemy.GetComponent<CharacterBoundsController>().CurrentLocation !=
                    CharacterBoundsController.state.inEnemyBase) //enemy is outside our base
                {
                    //unset tagger parameters
                    tagger.GetComponent<TagSM>().IsTagger = false;
                    tagger.GetComponent<PlayerStateController>().ChangeState(PlayerStateController.PlayerState.idle);
                    tagger.GetComponent<PlayerStateListener>().TargetAgent = null; //unset the target agent
                    tagger.GetComponent<TagSM>().Enemy = null;
                    tagger = null;
                }
            }
            if (!tagger) //there is no tagger
            {
                //the enemy is in our base!
                if (enemy.GetComponent<CharacterBoundsController>().CurrentLocation ==
                    CharacterBoundsController.state.inEnemyBase)
                {
                    GameObject obj = FindClosetPlayerInHomeBase(enemy);
                    if (obj)
                    {
                        tagger = obj;
                        tagger.GetComponent<PlayerStateListener>().TargetAgent = enemy;
                        tagger.GetComponent<PlayerStateController>().ChangeState(PlayerStateController.PlayerState.chasing);
                        tagger.GetComponent<TagSM>().IsTagger = true;
                        tagger.GetComponent<TagSM>().Enemy = enemy;
                    }
                }
            }
        }


    }

    #region class functions

    private GameObject FindClosetPlayerInHomeBase(GameObject enemyGameObject)
    {

        float minDist = Mathf.Infinity;
        int indexOfSmallest = -1;
        for (int i = 0; i < teamPool.Count; i++)
        {

            GameObject player = (GameObject)teamPool[i];
            bool trueCondition = !player.GetComponent<FlagCarrierSM>().HasFlag &&
                             !player.GetComponent<FlagCarrierSM>().IsAnFc
                             &&
                             player.GetComponent<PlayerStateListener>().CurrentState !=
                             PlayerStateController.PlayerState.IsTagged;

            if (!trueCondition)
                continue;
            Vector3 direction = enemyGameObject.transform.position - player.transform.position;
            direction.y = 0;//flatten y
            float tempDist = direction.magnitude;
            if (tempDist < minDist)
            {
                indexOfSmallest = i;
                minDist = tempDist;
            }
        }
        //Array.Sort(sortedDistances);
        if (indexOfSmallest != -1 && minDist < maxDistance)
        {
            GameObject returner = (GameObject)teamPool[indexOfSmallest];
            return returner;
        }


        return null;
    }

    public void ResetTaggers()
    {
        tagger = null;
    }
    #endregion


}
