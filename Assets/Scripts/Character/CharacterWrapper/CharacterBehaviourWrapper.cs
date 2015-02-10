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
    public class CharacterBehaviourWrapper : MonoBehaviour
    {
        #region

        //what is the current state?
        private InputController.MovementTypeState currentState;
        private MovementBehaviour _movementBehaviour;
        private GameObject _targetAgent;
        private Animation playerAnimation;

        public MovementBehaviour MovementBehaviour1
        {
            get { return _movementBehaviour; }
            set { _movementBehaviour = value; }
        }

        public GameObject Target
        {
            get { return _targetAgent; }
            set { _targetAgent = value; }
        }
        public GameObject TargetAgent
        {
            get { return _targetAgent; }
            set
            {

                if (_movementBehaviour == null)
                    Debug.Log("there is no assigned movement behaviour");
                else
                {
                    _movementBehaviour.TargetGameObject = value;
                    _targetAgent = value;
                }

            }

        }

        #endregion

        #region class functions

        private void OnStateChange(InputController.MovementTypeState newInputState)
        {
            //if the current state are the same, abort. No need to change the state we're in

            if (newInputState == currentState)
            {
                return;
            }

            currentState = newInputState;

            print("current state has changed to " + newInputState);
        }

        public void Arrive()
        {
            if (!CheckStop())
            {
                if (currentState == InputController.MovementTypeState.kinematic)
                    MovementBehaviour1.KinematicArrive();
                else
                {
                    MovementBehaviour1.SteeringArrive();
                }
                playerAnimation.Play("walk");


            }
        }

        public void Rotate()
        {
            if (currentState == InputController.MovementTypeState.kinematic)
                MovementBehaviour1.InterpolateRotate();

        }

        public void Flee()
        {
        }

        public void Wander()
        {
            if (!CheckStop())
                playerAnimation.Play("walk");
            _movementBehaviour.ReynoldsWander(3f, Time.fixedDeltaTime);
        }

        public bool CheckStop()
        {
            if (rigidbody.velocity.magnitude < 1f)
            {
                playerAnimation.Play("idle");
                return true;
            }

            return false;
        }


        public void Hop()
        {
            playerAnimation.Play("idle");
            Vector3 directionVector3 = TargetAgent.transform.position - transform.position;
            directionVector3.y = 0;//flatten y

            float distanceForRay = directionVector3.magnitude + 10f;
            int layermask = 1 << TargetAgent.layer;


            // transform.position = TargetAgent.transform.position + TargetAgent.transform.position.normalized * MovementBehaviour1.ArrivalRadius;
            //get the bound of the object
            Collider targetCollider = TargetAgent.GetComponent<Collider>();
            if (targetCollider == null)
                Debug.Log("there is no collider attached to the target object  " + TargetAgent.name);

            RaycastHit hit;

            if (Physics.Raycast(transform.position, directionVector3.normalized, out hit, distanceForRay, layermask))
            {
                Vector3 desiredPos = hit.point;
                desiredPos.y = 0; //flatten 0
                transform.position = desiredPos;

            }


            //TODO Fix hopping
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
        private void Awake()
        {
            playerAnimation = gameObject.GetComponent<Animation>();


        }

        #endregion


    }
}
