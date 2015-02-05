using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Character;
using UnityEngine;

namespace Assets.Scripts.Director
{


    class GameDirector : MonoBehaviour
    {

        #region class variables and properties

        private ArrayList teamOnePool = new ArrayList();
        private ArrayList teamTwoPool = new ArrayList();
      //  private List<GameObject> teamOnePool = new List<GameObject>();
      //  private List<GameObject> teamTwoPool = new List<GameObject>();
        private TeamBuilder teamBuilder;
        #endregion

        #region unity function

        private void Awake()
        {
            print("game director");
            teamBuilder = GetComponent<TeamBuilder>();

            if (teamBuilder == null)
                Debug.Log("teambuilder isn't found on the director!");

            teamOnePool = teamBuilder.TeamOnePool;
            teamTwoPool = teamBuilder.TeamTwoPool;
            RandomizeFirstFlagRunners();
        }

        private void Start()
        {

        }


        #endregion

        #region class functions
        /// <summary>
        /// This function will randomize the flag runner 
        /// </summary>
        private void RandomizeFirstFlagRunners()
        {
            int flagRunnerOneIndex = UnityEngine.Random.Range(0,teamOnePool.Count);
            int flagRunnerTwoIndex = UnityEngine.Random.Range(0, teamTwoPool.Count);

            GameObject flagBearerOne = teamOnePool[flagRunnerOneIndex] as GameObject;
            GameObject flagBearerTwo = teamOnePool[flagRunnerTwoIndex] as GameObject;
            
            print(flagBearerOne.name + " is the flag runner");
            print(flagBearerTwo.name + " is the flag runner");

            flagBearerOne.GetComponent<PlayerStateListener>().TargetAgent = teamBuilder.Flag1;
            flagBearerTwo.GetComponent<PlayerStateListener>().TargetAgent = teamBuilder.Flag2;

            flagBearerOne.GetComponent<PlayerStateController>().ChangeState(PlayerStateController.PlayerState.chasing);
            flagBearerTwo.GetComponent<PlayerStateController>().ChangeState(PlayerStateController.PlayerState.chasing);
        }

        #endregion
    }
}
