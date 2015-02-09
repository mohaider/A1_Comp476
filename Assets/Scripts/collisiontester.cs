using UnityEngine;
using System.Collections;

public class collisiontester : MonoBehaviour
{
    public GameObject target;
    public float rotspeed;
    public float speed;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	    //if (Input.GetKeyDown(KeyCode.A))
	      //      gameObject.GetComponent<Animation>().Play("pick_up");
     /*
        Vector3 currentOrientation = transform.rotation.eulerAngles;
        Vector3 directionVector3 =(target.transform.position) - transform.position;
        print("InverseTransformDirection" + transform.InverseTransformDirection(directionVector3));
        float angle = Mathf.Atan2(directionVector3.z, directionVector3.x) * Mathf.Rad2Deg;
	    Quaternion rotationQuaternion = Quaternion.Euler(new Vector3(0, angle, 0));
       // currentOrientation.y =angle;
       //  currentOrientation.y = Mathf.Lerp(currentOrientation.y,angle, Time.deltaTime * 10f);
       // transform.rotation = Quaternion.Euler(currentOrientation);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotationQuaternion, Time.deltaTime * rotspeed);
       // transform.LookAt(target.transform);
        print("direction vector  " + directionVector3.normalized);
        print(rigidbody.velocity.normalized);*/
	    rigidbody.velocity = Vector3.forward*speed;
	}

     void OnTriggerEnter(Collider col)
    {
       
    }
}
