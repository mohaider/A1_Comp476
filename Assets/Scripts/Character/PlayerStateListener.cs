using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Schema;
using UnityEngine;
using Assets.Scripts.Character;
using Assets.Scripts.Character.CharacterWrapper;

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

        public GameObject _targetAgent;
        private PlayerStateController stateController;
        public CharacterBehaviourWrapper _characterBehaviourWrapper;
        private ShowTextBubble textBubble;
        public GameObject TextBubbleGameObject;
        private UntaggerSetterSM untaggerManager;


        //default the states to idle
        private PlayerStateController.PlayerState previousState = PlayerStateController.PlayerState.idle;
        //previous state

        [SerializeField]
        private PlayerStateController.PlayerState currentState = PlayerStateController.PlayerState.idle;
        //current state

        [SerializeField]
        private string outputinfo = "";


        public CharacterBehaviourWrapper characterBehaviourWrapper
        {
            get { return _characterBehaviourWrapper; }
            set { _characterBehaviourWrapper = value; }
        }

        public ShowTextBubble TextBubble
        {
            get { return textBubble; }
            set { textBubble = value; }
        }

        public PlayerStateController.PlayerState CurrentState
        {
            get { return currentState; }
            set { currentState = value; }
        }

        public string Outputinfo
        {
            get { return outputinfo; }
            set { outputinfo = value; }
        }

        public GameObject TargetAgent
        {
            get { return _targetAgent; }
            set
            {
                _characterBehaviourWrapper.TargetAgent = value;
                _targetAgent = value;
            }
        }

        #endregion

        #region class functions

        public string GetCurrentState()
        {
            return name + "'s current state: " + currentState.ToString();
        }


        /// <summary>
        /// At every cycle, update the character behaviour according to its current state
        /// </summary>
        private void OnStateCycle()
        {
            Vector3 directionVector3;
            float dist = 0f;
            if (TargetAgent != null)
            {
                directionVector3 = TargetAgent.transform.position - transform.position;
                dist = directionVector3.magnitude;
            }



            switch (currentState)
            {
                case PlayerStateController.PlayerState.idle:
                    _characterBehaviourWrapper.Wander();


                    break;
                case PlayerStateController.PlayerState.chasing:

                    Chase();

                    break;

                case PlayerStateController.PlayerState.EscapingState:

                    Escape();
                    break;

                case PlayerStateController.PlayerState.IsTagged:
                    if (dist > 20f)
                        OnStateChange(PlayerStateController.PlayerState.IsTaggedRunningHome);
                    else
                    {

                        Tagged();
                    }
                    break;

                case PlayerStateController.PlayerState.IsTaggedRunningHome:

                    if (dist > 33f)
                        Chase();
                    else
                    {
                        OnStateChange(PlayerStateController.PlayerState.IsTagged);
                    }
                    break;
                case PlayerStateController.PlayerState.untagger:

                    Chase();
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

            /*     if (newPlayerState == currentState)
                 {
                     return;
                 }
            
                 //check if any special conditions that would cause this new state to abort
                 if (CheckIfAbortState(newPlayerState))
                 {
                     return;
                 }*/
            /*
             //check if the current is allowed to transition into this new state. if not, abort.
             if (!CheckValidStatePairing(newPlayerState))
             {
                 return;
             }

             //if we're here, then state change is allowed
*/
            switch (newPlayerState) //for all cases, you must activate the text speech bubble
            {
                case PlayerStateController.PlayerState.chasing:
                    TextBubbleGameObject.SetActive(true);

                    switch (TargetAgent.tag)
                    {
                        case "RunToOrangeHome":
                            textBubble.ApplyNewImage(ShowTextBubble.imagetype.goHome, "");
                            break;
                        case "RunToBananaHome":
                            textBubble.ApplyNewImage(ShowTextBubble.imagetype.goHome, "");
                            break;
                        case "TeamOrangeFlagArea":
                            textBubble.ApplyNewImage(ShowTextBubble.imagetype.goHome, "");
                            break;
                        case "TeamBananaFlagArea":
                            textBubble.ApplyNewImage(ShowTextBubble.imagetype.goHome, "");
                            break;
                        case "TeamOrange":
                            if (tag == "TeamOrange")
                                textBubble.ApplyNewImage(ShowTextBubble.imagetype.untag, "");
                            else
                                textBubble.ApplyNewImage(ShowTextBubble.imagetype.stop, "");
                            break;
                        case "TeamBanana":
                            if (tag == "TeamBanana")
                                textBubble.ApplyNewImage(ShowTextBubble.imagetype.untag, "");
                            else
                                textBubble.ApplyNewImage(ShowTextBubble.imagetype.stop, "");
                            break;
                        case "Orange":
                            textBubble.ApplyNewImage(ShowTextBubble.imagetype.chaseFlag, "orange");
                            break;
                        case "Banana":
                            textBubble.ApplyNewImage(ShowTextBubble.imagetype.chaseFlag, "");
                            break;

                    }
                   
                    break;
                case PlayerStateController.PlayerState.idle:
                    TextBubbleGameObject.SetActive(true);
                    textBubble.ApplyNewImage(ShowTextBubble.imagetype.wandering, "");
                    break;
                case PlayerStateController.PlayerState.holdingFlag:
                    TextBubbleGameObject.SetActive(true);
                    textBubble.ApplyNewImage(ShowTextBubble.imagetype.goHome, "");

                    // textBubble.ApplyNewText("I'm holding the flag!");
                    break;
                case PlayerStateController.PlayerState.IsTaggedRunningHome:
                    TextBubbleGameObject.SetActive(true);
                    textBubble.ApplyNewImage(ShowTextBubble.imagetype.taggedRunningHome, "");
                    break;

                case PlayerStateController.PlayerState.IsTagged:
                    TextBubbleGameObject.SetActive(true);
                    textBubble.ApplyNewImage(ShowTextBubble.imagetype.tagged, "");
                    break;

                case PlayerStateController.PlayerState.untagger:
                    TextBubbleGameObject.SetActive(true);
                    textBubble.ApplyNewImage(ShowTextBubble.imagetype.untag, "");
                    break;
           



            }

            //store the current state as the previous state
            previousState = currentState;

            //store new state as the current state
            currentState = newPlayerState;


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








        /// <summary>
        /// implemented according to the requirements set by the assigment
        /// </summary>
        private void Chase()
        {
            Vector3 directionalVector3 = _characterBehaviourWrapper.Target.transform.position - transform.position;
            directionalVector3.y = 0; //flatten y;
            Vector3 characterForward = transform.forward;
            characterForward.y = 0;
            float angleBetweenDirAndTar = Vector3.Angle(directionalVector3, transform.forward);
            float distance = directionalVector3.magnitude;
            float currentSpeed = rigidbody.velocity.magnitude;

            if (currentSpeed <= 5f)//A
            {
                outputinfo = "In A";

                //if the character is really close to the target(A.1)
                if (distance < _characterBehaviourWrapper.MovementBehaviour1.ArrivalRadius * 0.75f)
                {
                    outputinfo = "In A.i";
                    _characterBehaviourWrapper.Hop();
                    print(name + "Hopped over to target");
                }
                else
                {

                    //need to stop and rotate:

                    float difInAngles = Vector3.Angle(characterForward, directionalVector3.normalized);


                    float targetAngle = Mathf.Atan2(directionalVector3.x, directionalVector3.z) * Mathf.Rad2Deg;
                    targetAngle %= 360f;
                    float currentAngle = transform.rotation.eulerAngles.y % 360;
                    float differenceInAngles = targetAngle - currentAngle;

                    outputinfo = "In A.ii, difference between angles is " + difInAngles;

                    // float diferen = curangle - angere;

                    if (difInAngles > 9f)
                    {
                        outputinfo = "In A.ii difference between angles is " + difInAngles;
                        _characterBehaviourWrapper.Rotate(CharacterBehaviourWrapper.HeuristicType.A2);
                    }
                    else
                    {
                        _characterBehaviourWrapper.Move(CharacterBehaviourWrapper.HeuristicType.A2);
                    }

                    ///   print("chase state 2");
                }
            }
            else
            {
                outputinfo = "In B";
                if (Mathf.Abs(angleBetweenDirAndTar) < 30f)
                {
                    _characterBehaviourWrapper.Rotate(CharacterBehaviourWrapper.HeuristicType.B1);
                    _characterBehaviourWrapper.Move(CharacterBehaviourWrapper.HeuristicType.B2);
                    //  print("chase state 3");
                    outputinfo = "In B.i";
                }
                else
                {
                    //  rigidbody.velocity = Vector3.zero;
                    //stop the character
                    rigidbody.velocity = Vector3.zero;
                    _characterBehaviourWrapper.Rotate(CharacterBehaviourWrapper.HeuristicType.B2);
                    outputinfo = "In B.ii";
                    //  print("chase state 4");

                }
            }

        }/// <summary>
        /// implemented according to the requirements set by the assigment
        /// </summary>
        private void Escape()
        {
            Vector3 directionalVector3 = _characterBehaviourWrapper.Target.transform.position - transform.position;
            float distance = directionalVector3.magnitude;
            //teleport
            if (distance < _characterBehaviourWrapper.MovementBehaviour1.ArrivalRadius * 0.75f)
            {
                //transform.position = (transform.forward.normalized*3f) + transform.forward; //move 3 steps forward
                transform.position -= directionalVector3.normalized * 3f;
                outputinfo = "in c.i";
            }
            else
            {
                outputinfo = "in c.ii";
                _characterBehaviourWrapper.Rotate(CharacterBehaviourWrapper.HeuristicType.C2);
                _characterBehaviourWrapper.Move(CharacterBehaviourWrapper.HeuristicType.C2);
            }



        }

        private void Tagged()
        {

            untaggerManager.AddNewTaggedPlayer(gameObject);
            _characterBehaviourWrapper.Tagged();
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



        #region unity functions

        private void Awake()
        {
            playerAnimation = gameObject.GetComponent<Animation>();


        }

        private void OnEnable()
        {
            stateController = gameObject.GetComponent<PlayerStateController>();
            //PlayerStateController.OnStateChange += OnStateChange;
            stateController.OnStateChange += OnStateChange;
        }

        private void OnDisable()
        {
            //PlayerStateController.OnStateChange -= OnStateChange;
            stateController.OnStateChange -= OnStateChange;
        }



        private void Start()
        {
            //TeamOrangeManager
            //TeamBananaManager
            GameObject manager;
            if (tag == "TeamOrange")
            {
                manager = GameObject.FindGameObjectWithTag("TeamOrangeManager");

            }
            else
            {
                manager = GameObject.FindGameObjectWithTag("TeamBananaManager");
            }
            untaggerManager = manager.GetComponent<UntaggerSetterSM>();
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