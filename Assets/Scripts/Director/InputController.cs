using UnityEngine;
using System.Collections;

public class InputController : MonoBehaviour {
     #region class variables and properties

    private bool isKinematicMovement = true;
    public bool shome;
    #region class variables and properties
    public  delegate void changeMovementTypeHandler(InputController.MovementTypeState newState);

    public static event changeMovementTypeHandler OnStateChange;
    #endregion
    public enum MovementTypeState
    {
        kinematic,
        steering
    }

    #endregion

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	    if (Input.GetButtonDown("Change_movement_type"))
	    {
	        isKinematicMovement = !isKinematicMovement;
            if(isKinematicMovement)
                OnStateChange(InputController.MovementTypeState.kinematic);
            else
                OnStateChange(InputController.MovementTypeState.steering);
	    }
	}

#region class functions

    public void Toggle()
    {
        isKinematicMovement = !isKinematicMovement;
        if (isKinematicMovement)
            OnStateChange(InputController.MovementTypeState.kinematic);
        else
            OnStateChange(InputController.MovementTypeState.steering);
    }

    private void ChangeState(InputController.MovementTypeState newState)
    {
        if (OnStateChange != null)
            OnStateChange(newState);
    }

    #endregion
}
