using UnityEngine;
using System.Collections;

public class testerScript : MonoBehaviour
{

    public GameObject target;
    public float maxSpeed;
    public float maxAngularVelocity;
    public float maxAcceleration;
    public float maxAngularAcceleration;
    public float ArrivalRadius; //inner satisfaction radius
    public float SlowDownRadius;
    public float slowDownRotationRadius;

    public float turnSmoothing;
    public float maxPredictionTime; //max prediction time for pursue, and evade
    public float satisfactionRotation;
    public float slowDownOrientation;
    public float timeToTarget = 5f;
    public float angularVelocity;
    public float characterAngularVelocity;
    public GameObject textbubble;
    public float maxRotation;
    private MovementBehaviour movementBehaviour;
    public GameObject HolderGameObject;

    public enum movementType
    {
        evade,
        pursuit,
        seek,
        FaceawayAndEvade
    }

    public movementType movementype;
    /* gameObject.AddComponent<MovementBehaviour>();
             gameObject.GetComponent<MovementBehaviour>().InstatiateMovementBehaviour(gameObject, maxSpeed, maxAngularVelocity, maxAcceleration, maxAngularAcceleration, turnSmoothing,5f);
             gameObject.GetComponent<MovementBehaviour>().ArrivalRadius = this.ArrivalRadius;
             gameObject.GetComponent<MovementBehaviour>().SlowDownRadius = this.SlowDownRadius;
             gameObject.GetComponent<MovementBehaviour>().slowDownRotationRadius = this.slowDownRotationRadius;
             gameObject.GetComponent<MovementBehaviour>().maxPredictionTime = this.maxPredictionTime;
             gameObject.GetComponent<MovementBehaviour>().satisfactionRotation = this.satisfactionRotation;
             gameObject.GetComponent<MovementBehaviour>().slowDownOrientation = this.slowDownOrientation;
             gameObject.GetComponent<MovementBehaviour>().angularVelocity = this.angularVelocity;
             gameObject.GetComponent<MovementBehaviour>().characterAngularVelocity = this.characterAngularVelocity;*/

    // Use this for initialization
    void Start()
    {
        gameObject.AddComponent<MovementBehaviour>();
        movementBehaviour = gameObject.GetComponent<MovementBehaviour>();
        gameObject.GetComponent<MovementBehaviour>().InstatiateMovementBehaviour(gameObject, maxSpeed, maxAngularVelocity, maxAcceleration, maxAngularAcceleration, turnSmoothing, 5f);
        gameObject.GetComponent<MovementBehaviour>().ArrivalRadius = this.ArrivalRadius;
        gameObject.GetComponent<MovementBehaviour>().SlowDownRadius = this.SlowDownRadius;
        gameObject.GetComponent<MovementBehaviour>().slowDownRotationRadius = this.slowDownRotationRadius;
        gameObject.GetComponent<MovementBehaviour>().maxPredictionTime = this.maxPredictionTime;
        gameObject.GetComponent<MovementBehaviour>().satisfactionRotation = this.satisfactionRotation;
        gameObject.GetComponent<MovementBehaviour>().slowDownOrientation = this.slowDownOrientation;
        gameObject.GetComponent<MovementBehaviour>().angularVelocity = this.angularVelocity;
        gameObject.GetComponent<MovementBehaviour>().characterAngularVelocity = this.characterAngularVelocity;
        gameObject.GetComponent<MovementBehaviour>().maxRotation = this.maxRotation;
        gameObject.GetComponent<MovementBehaviour>().holder = this.HolderGameObject;
        gameObject.GetComponent<MovementBehaviour>()._targetGameObject = this.target;
    }

    // Update is called once per frame
    void Update()
    {
        //movementBehaviour.Align(target.transform);
        // rigidbody.velocity = Vector3.forward*maxSpeed;

        //  movementBehaviour.SteeringFlee();
        //movementBehaviour.LookWhereYoureGoing();
        switch (movementype)
        {
            case testerScript.movementType.evade:
                movementBehaviour.Evade();
                break;
            case testerScript.movementType.pursuit:
                movementBehaviour.Pursuit();
                break;
            case testerScript.movementType.seek:
                movementBehaviour.SteeringSeek(movementBehaviour._targetGameObject.transform);
                break;
            case testerScript.movementType.FaceawayAndEvade:
                movementBehaviour.FaceAway(movementBehaviour._targetGameObject.transform);
                movementBehaviour.Evade();
                break;
        }


    }
}
