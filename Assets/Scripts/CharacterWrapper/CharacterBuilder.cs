using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Character;
using UnityEngine;

namespace Assets.Scripts.CharacterWrapper

{
    /// <summary>
    /// The following class main function is to attach character listener, character controller 
    /// instantiate a new movement behaviour and assign it to the character listener.
    /// </summary>
    class CharacterBuilder: MonoBehaviour
    {
        #region class variables
        public GameObject TargetAgent;
        public float maxSpeed;
        public float maxAngularVelocity;
        public float maxAcceleration;
        public float maxAngularAcceleration;
        public float ArrivalRadius; //inner satisfaction radius
        public float SlowDownRadius;
        public float slowDownRotationRadius;
        
        public float turnSmoothing;
        public float maxPredictionTime; //max prediction time for pursue, and evade
        public float satisfactionRotation;
        public float slowDownOrientation;
        public float timeToTarget = 0.09f;
        public float angularVelocity;
        public float characterAngularVelocity;
   
        private MovementBehaviour movementBehaviour;

        #endregion


        #region
        void Start()
        {
            movementBehaviour = new MovementBehaviour(gameObject, maxSpeed, maxAngularVelocity, maxAcceleration, maxAngularAcceleration, turnSmoothing);
            gameObject.AddComponent<PlayerStateListener>();
            gameObject.GetComponent<PlayerStateListener>().movementBehaviour = movementBehaviour;


            gameObject.AddComponent<PlayerStateController>();
            
        }
        #endregion

    }
}
