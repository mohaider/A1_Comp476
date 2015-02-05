using UnityEngine;
using System.Collections;

public class collisiontester : MonoBehaviour
{


    public float speed;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	    if (Input.GetKeyDown(KeyCode.A))
	        gameObject.GetComponent<Animation>().Play("pick_up");
     //   rigidbody.velocity = Vector3.forward * speed;
        

	}

     void OnTriggerEnter(Collider col)
    {
       
    }
}
