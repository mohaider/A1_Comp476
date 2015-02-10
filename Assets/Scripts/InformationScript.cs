using UnityEngine;
using System.Collections;

public class InformationScript : MonoBehaviour
{
    public Vector3 forward;
	// Use this for initialization
	void Start ()
	{
	    forward = transform.forward;
	}
	
	// Update is called once per frame
	void Update () {
        forward = transform.forward;
	}
}
