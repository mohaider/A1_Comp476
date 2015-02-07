using UnityEngine;
using System.Collections;

public class InputListener : MonoBehaviour
{


    #region class variables and properties

    private bool isKinematicMovement = true;
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
    #endregion
}
