using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Character.CharacterWrapper
{
    class CharacterStateMachine: MonoBehaviour
    {
        #region class variables

        private bool useKinematic = true;
        


        public bool UseKinematic
        {
            get { return useKinematic; }
            set { useKinematic = value; }
        }

        #endregion

        #region state machine functions

        //statemachine in accordance with the assignement's requirements
        private void KinematicMovement()
        {
           // if(rigidbody.velocity)
        }

        private void SteeringMovement()
        {
            
        }
       

        #endregion

        #region unity functions


        private void Update()
        {
            if (UseKinematic)
                KinematicMovement();
            else
            {
                SteeringMovement();
            }

        }

        #endregion
    }
}
