using UnityEngine;
using System.Collections;

public class rotationtest : MonoBehaviour {
   public float  rotspeed= 5.0f;
    public GameObject target;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
/*

        Vector3 aimingDirection = target.transform.position - transform.position;
   
        var newHeading = target.transform.position - transform.position;
        newHeading = new Vector3(newHeading.x, 0, newHeading.z).normalized; // flattend, as before
 
        transform.forward = Vector3.Lerp(transform.forward, newHeading, Time.deltaTime * rotspeed);*/
        Vector3 aimingDirection = target.transform.position - transform.position;

        var heading = target.transform.position - transform.position;
        var newHeading = new Vector2(heading.x, heading.z).normalized;
        var currentHeading = new Vector2(transform.forward.x, transform.forward.z).normalized;
	    float angle = Mathf.Atan2(newHeading.x, newHeading.y)*Mathf.Rad2Deg;
	    Quaternion rot= Quaternion.Euler(new Vector3(0, angle, 0));
	    transform.rotation = Quaternion.Lerp(transform.rotation, rot, Time.deltaTime*rotspeed);
	}
    Quaternion RotateAroundUp(float degrees)
    {
        var radians = degrees * Mathf.Deg2Rad; // Radians in a degree.
        return RotateAroundAxis((float)radians, Vector3.up);
    }
    Quaternion RotateAroundAxis(float radians, Vector3 axis)
    {
        var vec = axis * Mathf.Sin(radians);
        return new Quaternion(vec.x, vec.y, vec.z, Mathf.Cos(radians));

    }

}
