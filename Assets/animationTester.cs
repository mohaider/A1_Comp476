using UnityEngine;
using System.Collections;


public class animationTester : MonoBehaviour
{
    #region class variables and properties
    private Animation animation;

    #endregion

    #region unity functions
    // Use this for initialization
	void Start ()
	{
	    animation = gameObject.GetComponent<Animation>();
        
	}
	
	// Update is called once per frame
	void Update ()
	{

        if (Input.GetKeyDown(KeyCode.Alpha0))
            animation.Play("attack 1");

        if (Input.GetKeyDown(KeyCode.Alpha1))
            animation.Play("attack 2");

        if (Input.GetKeyDown(KeyCode.Alpha2))
            animation.Play("attack 3");

        if (Input.GetKeyDown(KeyCode.Alpha3))
            animation.Play("climb");

        if (Input.GetKeyDown(KeyCode.Alpha5))
            animation.Play("walk");

        if (Input.GetKeyDown(KeyCode.Alpha6))
            animation.Play("run");

        if (Input.GetKeyDown(KeyCode.Alpha7))
            animation.Play("ready");



	}
    #endregion
}
