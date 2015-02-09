using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Character;
using Assets.Scripts.Character.CharacterWrapper;
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
        public float timeToTarget = 5f;
        public float angularVelocity;
        public float characterAngularVelocity;
        public GameObject textbubble;
        public GameObject holder;
        private MovementBehaviour movementBehaviour;

        #endregion


        #region
        void Awake()
        {
           // _characterBehaviourWrapper = new MovementBehaviour(gameObject, maxSpeed, maxAngularVelocity, maxAcceleration, maxAngularAcceleration, TurnSmoothing);
           
            gameObject.AddComponent<MovementBehaviour>();
            gameObject.GetComponent<MovementBehaviour>().InstatiateMovementBehaviour(gameObject, maxSpeed, maxAngularVelocity, maxAcceleration, maxAngularAcceleration, turnSmoothing,5f);
            gameObject.GetComponent<MovementBehaviour>().ArrivalRadius = this.ArrivalRadius;
            gameObject.GetComponent<MovementBehaviour>().SlowDownRadius = this.SlowDownRadius;
            gameObject.GetComponent<MovementBehaviour>().slowDownRotationRadius = this.slowDownRotationRadius;
            gameObject.GetComponent<MovementBehaviour>().maxPredictionTime = this.maxPredictionTime;
            gameObject.GetComponent<MovementBehaviour>().satisfactionRotation = this.satisfactionRotation;
            gameObject.GetComponent<MovementBehaviour>().slowDownOrientation = this.slowDownOrientation;
            gameObject.GetComponent<MovementBehaviour>().angularVelocity = this.angularVelocity;
            gameObject.GetComponent<MovementBehaviour>().characterAngularVelocity = this.characterAngularVelocity;
            GameObject tempHolder =(GameObject) Instantiate(holder);
            gameObject.GetComponent<MovementBehaviour>().holder = tempHolder;
            
            gameObject.AddComponent<PlayerStateController>();
            gameObject.AddComponent<PlayerStateListener>();
            //gameObject.GetComponent<PlayerStateListener>()._characterBehaviourWrapper =gameObject.GetComponent<MovementBehaviour>();
            gameObject.AddComponent<CharacterBehaviourWrapper>();
            gameObject.GetComponent<CharacterBehaviourWrapper>().MovementBehaviour1 = gameObject.GetComponent<MovementBehaviour>();

            gameObject.GetComponent<PlayerStateListener>().characterBehaviourWrapper = gameObject.GetComponent<CharacterBehaviourWrapper>();
            gameObject.GetComponent<PlayerStateListener>().TextBubble = GetComponentInChildren<ShowTextBubble>();
            gameObject.GetComponent<PlayerStateListener>().TextBubbleGameObject = this.textbubble;
             gameObject.GetComponent<PlayerStateListener>().TextBubbleGameObject.SetActive(false);
            Destroy(this);

            
        }
        #endregion

    }
}
