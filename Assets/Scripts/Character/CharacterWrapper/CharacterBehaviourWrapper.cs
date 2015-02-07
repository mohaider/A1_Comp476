using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Character.CharacterWrapper
{

    /// <summary>
    /// The following class wraps the movement behaviour of an agent. 
    /// This calls on a static event from input controller 
    /// in order to find what the current state of the  movmement type is
    /// </summary>
    class CharacterBehaviourWrapper: MonoBehaviour
    {
        #region

        //what is the current state?
        private InputController.MovementTypeState currentState;

        #endregion

        #region class functions
        public void OnStateChange(InputController.MovementTypeState newState )
        {
        }
        #endregion

        #region unity functions
        private void OnEnable()
        {
            //hook onstatechange
            InputController.OnStateChange += OnStateChange;
        }

        private void OnDisable()
        {
            //unhook onstatechange
            InputController.OnStateChange -= OnStateChange;
        }

        private void Update()
        {
        }

        #endregion


    }
}
