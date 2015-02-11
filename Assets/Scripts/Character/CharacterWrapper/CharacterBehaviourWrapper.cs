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
        private float originalMaxSpeed;

        public enum HeuristicType
        {
            A1,
            A2,
            B1,
            B2,
            C1,
            C2
        }
        public MovementBehaviour MovementBehaviour1
        {
            get { return _movementBehaviour; }
            set
            {
                originalMaxSpeed = value.MaxSpeed;
                _movementBehaviour = value;
            }
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

            //            print("current state has changed to " + newInputState);
        }

        public void Move(HeuristicType heuristicType)
        {

            //set maxspeed to original value
            MovementBehaviour1.MaxSpeed = originalMaxSpeed;

            //check if the player's speed is close to zero
            if (_movementBehaviour.CharacterVelocity.magnitude < 1f)
                playerAnimation.Play("idle");
            //check if current speed is half,set animation to walk
            else if (rigidbody.velocity.magnitude < originalMaxSpeed * 0.5f)
                playerAnimation.Play("walk");
            else
            {
                playerAnimation.Play("run");
            }
            if (currentState == InputController.MovementTypeState.kinematic)
            {
                KinematicMovement(heuristicType);
                //print(name + " is moving kinematically");
                // MovementBehaviour1.KinematicArrive();
            }
            else
            {
                SteeringMove(heuristicType);
                //  print(name + " is moving by steering");
                //MovementBehaviour1.SteeringArrive();
            }




        }

        public void Rotate(CharacterBehaviourWrapper.HeuristicType heuristicType)
        {
            if (currentState == InputController.MovementTypeState.kinematic)
                KinematicRotate(heuristicType);
            // MovementBehaviour1.InterpolateRotate();
            if (currentState == InputController.MovementTypeState.steering)
                SteeringRotate(heuristicType);

        }

        private void KinematicRotate(HeuristicType heuristicType)
        {
            switch (heuristicType)
            {
                case HeuristicType.A1:
                    break;

                case HeuristicType.A2:
                    MovementBehaviour1.InterpolateRotate();
                    break;

                case HeuristicType.B1:
                    MovementBehaviour1.InterpolateRotate();
                    break;

                case HeuristicType.B2:
                    MovementBehaviour1.InterpolateRotate();
                    break;

                case HeuristicType.C1:
                   // MovementBehaviour1.InterpolateRotate();
               
                    break;

                case HeuristicType.C2:

                    MovementBehaviour1.InterpolateRotateWithEnemy();
                    break;


            }
        }

        private void SteeringRotate(CharacterBehaviourWrapper.HeuristicType heuristicType)
        {
            switch (heuristicType)
            {
                case CharacterBehaviourWrapper.HeuristicType.A1:
                    break;

                case CharacterBehaviourWrapper.HeuristicType.A2:
                    MovementBehaviour1.Face(MovementBehaviour1.TargetGameObject.transform);
                    break;

                case CharacterBehaviourWrapper.HeuristicType.B1:
                    MovementBehaviour1.LookWhereYoureGoing();
                    break;

                case CharacterBehaviourWrapper.HeuristicType.B2:
                    MovementBehaviour1.Face(MovementBehaviour1.TargetGameObject.transform);
                    break;

                case CharacterBehaviourWrapper.HeuristicType.C1:
                    MovementBehaviour1.LookWhereYoureGoing();
                    break;

                case CharacterBehaviourWrapper.HeuristicType.C2:

                    MovementBehaviour1.FaceAway(MovementBehaviour1.TargetGameObject.transform);
                    break;


            }
        }

        private void KinematicMovement(HeuristicType heuristicType)
        {
            switch (heuristicType)
            {
                case HeuristicType.A1:
                    MovementBehaviour1.KinematicArrive();
                    break;

                case HeuristicType.A2:
                    MovementBehaviour1.KinematicArrive();
                    break;

                case HeuristicType.B1:
                    MovementBehaviour1.KinematicArrive();
                    break;

                case HeuristicType.B2:
                    MovementBehaviour1.KinematicArrive();
                    break;

                case HeuristicType.C1:
                    MovementBehaviour1.KinematicFlee();
                    break;

                case HeuristicType.C2:
                    
                    MovementBehaviour1.KinematicFlee();
                    break;


            }
        }

        private void SteeringMove(HeuristicType heuristicType)
        {
            switch (heuristicType)
            {
                case HeuristicType.A1:
                    MovementBehaviour1.SteeringArrive();
                    break;

                case HeuristicType.A2:
                    MovementBehaviour1.KinematicArrive();
                    break;

                case HeuristicType.B1:
                    MovementBehaviour1.KinematicArrive();
                    break;

                case HeuristicType.B2:
                    MovementBehaviour1.SteeringArrive();
                    break;

                case HeuristicType.C1:
                    MovementBehaviour1.SteeringFlee(); 
                    break;

                case HeuristicType.C2:
                    MovementBehaviour1.Evade(); 
                    break;


            }
        }
        public void Flee()
        {
        }

        public void Wander()
        {
            if (_movementBehaviour.CharacterVelocity.magnitude < 1f)
                playerAnimation.Play("idle");
            else
                playerAnimation.Play("walk");
            _movementBehaviour.ReynoldsWander(3f, Time.fixedDeltaTime);
            _movementBehaviour.MaxSpeed = originalMaxSpeed / 2f; //walk slower
        }

        public bool CheckStop()
        {
            if (_movementBehaviour.CharacterVelocity.magnitude < 1f)
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


        public void Tagged()
        {
            animation.Play("worry");
            MovementBehaviour1.Stop();
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
