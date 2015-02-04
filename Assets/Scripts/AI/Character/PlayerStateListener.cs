using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Assets.Scripts.Character;
/**
 * Code written by Mohammed Haider
 * If you have any questions about this code, feel free to email me
 * mrhaider@gmail.com 
 */

namespace Assets.Scripts.Character
{
    public class PlayerStateListener : MonoBehaviour
    {
        #region class variables

        public bool onGround; //is the player on the ground?
        public GameObject respawnPoint; //incase we need a respawn point
        public GameObject projectileSpawnPoint;
        public float playerSpeed; //what is the players speed?
        public float turnSmoothing = 15.0f;
        public float playerJumpForceVertical; //in case you need to jump, what is the vertical force?
        public float playerJumpForceHorizontal; //in case you need to jumo, what is the horizontal force?
        private Animation playerAnimation;
        private MovementBehaviour _movementBehaviour;



        //default the states to idle
        private PlayerStateController.PlayerState previousState = PlayerStateController.PlayerState.idle;
        //previous state

        private PlayerStateController.PlayerState currentState = PlayerStateController.PlayerState.idle;
        //current state

        [SerializeField]



        #endregion

        #region class functions

        public string GetCurrentState()
        {
            return currentState.ToString();
        }


        /// <summary>
        /// At every cycle, update the character behaviour according to its current state
        /// </summary>
        private void OnStateCycle()
        {
            //cache the input axis values 
            // Cache the inputs.
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");
            //todo fill this switch case after discussing the controls with the team. might need to change the player state enumerator depending on the decision
            switch (currentState)
            {
                case PlayerStateController.PlayerState.idle:
                    playerAnimation.Play("walk");
                        _movementBehaviour.ReynoldsWander(3f, Time.fixedDeltaTime);

                    break;
                /*  
                                case PlayerStateController.PlayerState.left:


                                    break;

                                case PlayerStateController.PlayerState.walk:
                                    playerAnimation.Play("walk");
                                    Rotating(h, v);
                                    rigidbody.MovePosition(transform.position + (transform.forward / 20.0f));

                                    break;

                                case PlayerStateController.PlayerState.run:
                                    playerAnimation.Play("run");
                                    Rotating(h, v);
                                    rigidbody.MovePosition(transform.position + (transform.forward / 10.0f));

                                    //MovementManagement(h, v);
                                    break;

                                //start crawling
                                case PlayerStateController.PlayerState.crawling:
                                    playerAnimation.Play("walk");
                                    Rotating(h, v);
                                    rigidbody.MovePosition(transform.position + (transform.forward / 30.0f));

                                    break;

                                case PlayerStateController.PlayerState.jump:


                                    break;

                                case PlayerStateController.PlayerState.attacking:


                                    break;

                                case PlayerStateController.PlayerState.climbing:


                                    break;

                                case PlayerStateController.PlayerState.confused:


                                    break;

                                case PlayerStateController.PlayerState.throwItem:

                                    break;


                                case PlayerStateController.PlayerState.usePotion:

                                    break;
                */
            }
        }

        /// <summary>
        /// OnStateChange is called whenever we make a change tot he player's state from
        /// anywhere in the game's code
        /// </summary>
        /// <param name="playerState"></param>
        private void OnStateChange(PlayerStateController.PlayerState newPlayerState)
        {
            //if the current state are the same, abort. No need to change the state we're in

            /* if (newPlayerState == currentState)
             {
                 return;
             }

             //check if any special conditions that would cause this new state to abort
             if (CheckIfAbortState(newPlayerState))
             {
                 return;
             }

             //check if the current is allowed to transition into this new state. if not, abort.
             if (!CheckValidStatePairing(newPlayerState))
             {
                 return;
             }

             //if we're here, then state change is allowed

             switch (newPlayerState)
             {
                 case PlayerStateController.PlayerState.walk:
                     playerAnimation.Play("walk");
                     break;
                 case PlayerStateController.PlayerState.run:
                     playerAnimation.Play("run");
                     break;
                 case PlayerStateController.PlayerState.crawling:
                     playerAnimation.Play("walk");
                     break;


             }

             //store the current state as the previous state
             previousState = currentState;

             //store new state as the current state
             currentState = newPlayerState;
 */

        }

        /// <summary>
        /// compare the current state against the new desired playerstate
        /// </summary>
        /// <param name="playerState"></param>
        /// <returns></returns>
        private bool CheckValidStatePairing(PlayerStateController.PlayerState newPlayerState)
        {
            bool returner = false;

            /*   switch (currentState)
               {
                   //any state can talk over from idle
                   case PlayerStateController.PlayerState.idle:
                       returner = true;
                       break;
                   //any state can take over from walking, crawling and running
                   case PlayerStateController.PlayerState.run:
                       returner = true;
                       break;
                   case PlayerStateController.PlayerState.walk:
                       returner = true;
                       break;
                   case PlayerStateController.PlayerState.crawling:
                       returner = true;
                       break;

                   //only state that can take over from a jump is a landing or killing

                   case PlayerStateController.PlayerState.jump:
                       if (newPlayerState == PlayerStateController.PlayerState.landing ||
                           newPlayerState == PlayerStateController.PlayerState.throwItem ||
                           newPlayerState == PlayerStateController.PlayerState.usePotion)
                           returner = true;
                       else
                           returner = false;
                       break;
                   //only state that can take over from a fall  is a landing or killing

                   case PlayerStateController.PlayerState.fall:
                       if (newPlayerState == PlayerStateController.PlayerState.landing ||
                           newPlayerState == PlayerStateController.PlayerState.throwItem ||
                           newPlayerState == PlayerStateController.PlayerState.usePotion)
                           returner = true;
                       else
                           returner = false;
                       break;
                   //only state that can take over from landing is movement or idleness 
                   case PlayerStateController.PlayerState.landing:
                       if (newPlayerState == PlayerStateController.PlayerState.walk ||
                           newPlayerState == PlayerStateController.PlayerState.run ||
                           newPlayerState == PlayerStateController.PlayerState.crawling ||
                           newPlayerState == PlayerStateController.PlayerState.idle)
                           returner = true;
                       else
                           returner = false;
                       break;

                   //only state that can take over from resurecction is idle
                   case PlayerStateController.PlayerState.resurrect:
                       if (newPlayerState == PlayerStateController.PlayerState.idle)
                           returner = true;
                       else
                           returner = false;
                       break;
                   case PlayerStateController.PlayerState.throwItem:
                       returner = true;
                       break;
                   case PlayerStateController.PlayerState.usePotion:
                       returner = true;
                       break;


               }*/
            return returner;
        }

        /// <summary>
        ///  This would be used to check if a current state change will have to be cancelled. 
        /// For example, suppose the character is attacking and the next state is attack.
        /// We just want him him to attack every 0.25seconds, we return true to cancel this change of states. 
        /// </summary>
        /// <param name="playerState"></param>
        /// <returns></returns>
        private bool CheckIfAbortState(PlayerStateController.PlayerState newPlayerState)
        {
            //todo: doublecheck other states if they need to do something as well
            bool returner = false;
            /*    switch (newPlayerState)
                {
                    case PlayerStateController.PlayerState.idle:

                        break;
                    case PlayerStateController.PlayerState.walk:

                        break;
                    case PlayerStateController.PlayerState.run:

                        break;
                    case PlayerStateController.PlayerState.crawling:

                        break;

                    case PlayerStateController.PlayerState.jump:
                        float nextAllowedJump =
                            PlayerStateController.stateDelayTimer[(int)PlayerStateController.PlayerState.jump];

                        if (nextAllowedJump < 0.1f || nextAllowedJump > Time.time)
                            returner = true;
                        else
                            returner = false;
                        break;

                    case PlayerStateController.PlayerState.throwItem:
                        float nextAllowedThrowItem =
                            PlayerStateController.stateDelayTimer[(int)PlayerStateController.PlayerState.throwItem];
                        if (nextAllowedThrowItem > Time.time)
                            returner = true;
                        else
                            returner = false;
                        break;

                }*/
            return returner;


        }


        private void MovementManagement(float horizontal, float vertical)
        {

            Rotating(horizontal, vertical);
            Move();

        }

        private void Move()
        {

            // TODO: Change speeds if needed
            // Crawl
            if (Input.GetKey(KeyCode.LeftShift))
            {
                rigidbody.MovePosition(transform.position + (transform.forward / 30.0f));

            }
            // Sprint
            else if (Input.GetKey(KeyCode.LeftCommand) || Input.GetKey(KeyCode.LeftControl))
            {
                rigidbody.MovePosition(transform.position + (transform.forward / 10.0f));

            }
            // Walk
            else
            {
                rigidbody.MovePosition(transform.position + (transform.forward / 20.0f));

            }


        }

        private void Walk()
        {

        }

        private void Crawl()
        {

        }

        private void Running()
        {

        }

        private void Rotating(float horizontal, float vertical)
        {
            Vector3 targetDirection = new Vector3(horizontal, 0f, vertical);

            // Rotation based on this new vector assuming that up is the global y axis.
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection, Vector3.up);

            // Incremental rotation towards target rotation from the player's rotation.
            Quaternion newRotation = Quaternion.Lerp(rigidbody.rotation, targetRotation, turnSmoothing * Time.deltaTime);
            rigidbody.MoveRotation(newRotation);
        }

        #endregion


        #region class functions implemented from ICharacter

        public Vector3 PosVector3
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public Vector3 VelVector3
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public Vector3 AccelVector3
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public float orientation
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public float maxAcceleration
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public float maxVelocity
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public MovementBehaviour movementBehaviour
        {
            get { return _movementBehaviour; }
            set { _movementBehaviour = value; }
        }

        #endregion

        #region unity functions

        private void Awake()
        {
            playerAnimation = gameObject.GetComponent<Animation>();

        }

        private void OnEnable()
        {
            PlayerStateController.OnStateChange += OnStateChange;
        }

        private void OnDisable()
        {
            PlayerStateController.OnStateChange -= OnStateChange;
        }



        private void State()
        {
            // PlayerStateController.stateDelayTimer[(int)PlayerStateController.PlayerState.jump] = 1.0f;//can jump every 1.0 seconds
            //PlayerStateController.stateDelayTimer[(int)PlayerStateController.PlayerState.jump] = 1.0f;//can jump every 1.0 seconds
        }

        private void Update()
        {
            OnStateCycle();
        }


        #endregion

    }

}