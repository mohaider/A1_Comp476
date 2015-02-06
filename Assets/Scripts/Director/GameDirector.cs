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
        private GameObject[] playerPool;
      //  private List<GameObject> teamOnePool = new List<GameObject>();
      //  private List<GameObject> teamTwoPool = new List<GameObject>();
        private TeamBuilder teamBuilder;
        #endregion

        #region unity function

        private void Awake()
        {

            teamBuilder = GetComponent<TeamBuilder>();

            if (teamBuilder == null)
                Debug.Log("teambuilder isn't found on the director!");

            playerPool = teamBuilder.TeamAgents;

            teamOnePool = teamBuilder.TeamOnePool;
            teamTwoPool = teamBuilder.TeamTwoPool;

            RandomizeFirstFlagRunners();//randomize first two flag runners from opposing teams            
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
            GameObject flagBearerTwo = teamTwoPool[flagRunnerTwoIndex] as GameObject;
            


            flagBearerOne.GetComponent<PlayerStateListener>().TargetAgent = teamBuilder.Flag2;
            flagBearerTwo.GetComponent<PlayerStateListener>().TargetAgent = teamBuilder.Flag1;


            flagBearerOne.GetComponent<PlayerStateController>().ChangeState(PlayerStateController.PlayerState.chasing);
            flagBearerTwo.GetComponent<PlayerStateController>().ChangeState(PlayerStateController.PlayerState.chasing);
        }

        /// <summary>
        /// This class will change the movement behaviour of all characters in the playerpool movement 
        /// behaviour type in accordance with the assignment requirements
        /// </summary>
        public  void PlrMovementBehaviourChange()
        {

        }

        #endregion
    }
}
