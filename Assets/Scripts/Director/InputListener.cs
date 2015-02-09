using UnityEngine;
using System.Collections;


public class InputListener : MonoBehaviour
{


    #region class variables and properties

    private bool isKinematicMovement = true;
    public UnityEngine.UI.Text text;
    private InputController.MovementTypeState currentState = InputController.MovementTypeState.kinematic;

    #endregion
    #region class funtions

    private void OnStateChange(InputController.MovementTypeState newInputState)
    {
        //if the current state are the same, abort. No need to change the state we're in

        if (newInputState == currentState)
        {
            return;
        }

        currentState = newInputState;

        text.text = "Mode: " + currentState;

    }

    #endregion

    #region unity functions
    // Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetButtonDown("Change_movement_type"))
            print("changing movement type");
       
    }
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

    void ChangeState()
    { }

    #endregion
}
