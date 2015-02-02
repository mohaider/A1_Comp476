using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
/**
 * Code written by Mohammed Haider
 * If you have any questions about this code, feel free to email me
 * mrhaider@gmail.com 
 */
namespace Assets.Scripts.Character
{
    class PlayerStateController : MonoBehaviour
    {

        #region class variables and properties
        public enum PlayerState
        {
            idle = 0, left, right, up, down, jump, landing, fall, resurrect, crawling, climbing,
            attacking, throwItem, usePotion, poisoned, confused, slowed,
            healthy, run, walk //todo might need to remove this if we decide that the player needs to move through keyboard inputs. Also change the array instatiation below
        }

        public static float[] stateDelayTimer = new float[(int)PlayerState.walk]; //in the case we need to add various timers to various states

        public delegate void playerStateHandler(PlayerStateController.PlayerState newState);

        public static event playerStateHandler OnStateChange;


        #endregion


        #region class functions


        #endregion

        #region unity functions

        //not sure which one is appropriate

        private void Update()
        {


            //detect the current input of the horizontal axis, then broadcast a state update for the player
            if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
            {
                if (OnStateChange != null)
                {
                    //check if the character is running
                    if (Input.GetKey(KeyCode.LeftCommand) || Input.GetKey(KeyCode.LeftControl))
                        OnStateChange(PlayerStateController.PlayerState.run);
                    else if (Input.GetKey(KeyCode.LeftShift)) //crawling
                    {
                        
                            OnStateChange(PlayerStateController.PlayerState.crawling);
                          
                    }
                    else
                        OnStateChange(PlayerStateController.PlayerState.walk);
                }
            }
            else
            {
                OnStateChange(PlayerStateController.PlayerState.idle);


            }
            if (Input.GetAxis("Jump") != 0)
            {
                if (OnStateChange != null)
                {
                    OnStateChange(PlayerStateController.PlayerState.jump);
                }
            }


        }


    }
        #endregion


}

