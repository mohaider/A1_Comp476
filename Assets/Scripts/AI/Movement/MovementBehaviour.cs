using UnityEngine;
using System;
using System.Collections;
using System.Runtime.InteropServices;
using Assets.Script.Tools;

public class MovementBehaviour : MonoBehaviour
{
    public GameObject _targetGameObject;
    private GameObject character;
    private Vector3 CharacterVelocity;
    private Vector3 targetVelocity;
    private Vector3 characterAcceleration;
    public float maxVelocity;
    public float maxAngularVelocity;
    public float maxAcceleration;
    public float maxAngularAcceleration;
    public float ArrivalRadius; //inner satisfaction radius
    public float SlowDownRadius;
    public float slowDownRotationRadius;
    private bool hasArrived = false;
    private float resetTimer = 3f;
    private float timer = 3f;
    public float TurnSmoothing;
    public float maxPredictionTime; //max prediction time for pursue, and evade
    public float satisfactionRotation;
    public float slowDownOrientation;
    public float timeToTarget; //time to target
    public float angularVelocity;
    public float characterAngularVelocity;

    public delegate Vector3 SeekTargetDelegate(GameObject target, float timeStep);

    public SeekTargetDelegate seekTargetDelegate;


    #region constructors and setters

    public MovementBehaviour(GameObject character, float maxspeed, float maxAngularvelocity, float maxAcceleration,
        float maxAngularAcceleration, float turnSmoothing)
    {

        this.character = character;
        this.maxVelocity = maxspeed;
        this.maxAngularAcceleration = maxAngularAcceleration;
        this.maxAngularVelocity = maxAngularvelocity;
        this.maxAcceleration = maxAcceleration;
        seekTargetDelegate += SteeringSeek;
        this.TurnSmoothing = turnSmoothing;
    }

    public GameObject TargetGameObject
    {
        get { return _targetGameObject; }
        set { _targetGameObject = value; }
    }

    public void InstatiateMovementBehaviour(GameObject character, float maxspeed, float maxAngularvelocity, float maxAcceleration,
        float maxAngularAcceleration, float turnSmoothing, float timeToTarget)
    {

        this.character = character;
        this.maxVelocity = maxspeed;
        this.maxAngularAcceleration = maxAngularAcceleration;
        this.maxAngularVelocity = maxAngularvelocity;
        this.maxAcceleration = maxAcceleration;
        seekTargetDelegate += SteeringSeek;
        this.TurnSmoothing = turnSmoothing;
        this.timeToTarget = timeToTarget;
        resetTimer = 3.5f;
    }

    #endregion
    #region steering

    /// <summary>
    /// this will return a velocity, will always set the velocity.y to 0
    /// </summary>
    /// <param name="target"></param>
    /// <param name="timeStep"></param>
    /// <returns></returns>
    public Vector3 SteeringSeek(GameObject target, float timeStep)
    {
        if (!hasArrived)
        {

            characterAcceleration = (target.transform.position - character.transform.position);

            characterAcceleration.y = 0;
            characterAcceleration = Vector3.Normalize(characterAcceleration) * maxAcceleration;
            CharacterVelocity = CharacterVelocity + (characterAcceleration * timeStep);
            //current velocity + desiredAcceleration* time
            CharacterVelocity = AdditionalVector3Tools.Limit(CharacterVelocity, maxVelocity);
            //FixRotation();
        }
        return CharacterVelocity;


    }

    public Vector3 SteeringSeek(Vector3 targetPos, float timeStep)
    {
        if (!hasArrived)
        {
         //   FixRotation();
            characterAcceleration = (targetPos - character.transform.position);
            characterAcceleration.y = 0;
            characterAcceleration = Vector3.Normalize(characterAcceleration) * maxAcceleration;
            CharacterVelocity = CharacterVelocity + (characterAcceleration * timeStep);
            //current velocity + desiredAcceleration* time
            CharacterVelocity = AdditionalVector3Tools.Limit(CharacterVelocity, maxVelocity);

        }
        return CharacterVelocity;


    }

    /// <summary>
    /// this will return a velocity that is trying to steer away from the target. 
    /// will always set velocity.y to 0
    /// </summary>
    /// <param name="target"></param>
    /// <param name="timeStep"></param>
    /// <returns></returns>
    public Vector3 SteeringFlee(GameObject target, float timeStep)
    {
       // FixRotation();
        characterAcceleration = (character.transform.position - target.transform.position);

        characterAcceleration.y = 0;
        characterAcceleration = Vector3.Normalize(characterAcceleration) * maxAcceleration;
        CharacterVelocity = CharacterVelocity + (characterAcceleration * timeStep);
        CharacterVelocity = AdditionalVector3Tools.Limit(CharacterVelocity, maxVelocity);
        return CharacterVelocity;
    }

    public Vector3 SteeringFlee(Vector3 target, float timeStep)
    {
       // FixRotation();
        characterAcceleration = (character.transform.position - target);

        characterAcceleration.y = 0;
        characterAcceleration = Vector3.Normalize(characterAcceleration) * maxAcceleration;
        CharacterVelocity = CharacterVelocity + (characterAcceleration * timeStep);
        CharacterVelocity = AdditionalVector3Tools.Limit(CharacterVelocity, maxVelocity);
        return CharacterVelocity;
    }

    public Vector3 SteeringArrive(GameObject target, float timeStep)
    {

        float dist = (target.transform.position - character.transform.position).magnitude;

        //in the inner radius, stop
        if (dist < ArrivalRadius)
        {
            CharacterVelocity = Vector3.zero;
            hasArrived = true;

        }
        else if (dist < SlowDownRadius)
        {
            Vector3 desiredVel = target.transform.position - character.transform.position;
            float distance = desiredVel.magnitude;
            float mag = AdditionalVector3Tools.map(distance, 0, 50, 0, maxVelocity);
            //CharacterVelocity = mag*desiredVel*timeStep;
            CharacterVelocity = Vector3.Lerp(CharacterVelocity, Vector3.zero, timeStep);

            hasArrived = true;
        }
        else if ((target.transform.position - character.transform.position).magnitude > SlowDownRadius)
        {
            hasArrived = false;
        }
        return CharacterVelocity;


    }



    public void ReynoldsWander(float dist, float timeStep)
    {
        //decrement timer by one time frame
        timer -= Time.fixedDeltaTime;
        //  StartCoroutine(RandomizeDirection(TimeBetweenWanders,dist));
        if (timer < 0)
        {

            CharacterVelocity = RandomDirection(6f, timeStep);
            timer = resetTimer;
        }
        //smoothen rotation
        SmoothRotation(CharacterVelocity, timeStep);
        CharacterVelocity.y = 0;
        character.rigidbody.velocity = CharacterVelocity;

    }
    void SmoothRotation(Vector3 tarDirection, float timeStep)
    {
        Vector3 targetDirection = tarDirection;

        // Rotation based on this new vector assuming that up is the global y axis.
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection, Vector3.up);

        // Incremental rotation towards target rotation from the player's rotation.
        Quaternion newRotation = Quaternion.Lerp(character.rigidbody.rotation, targetRotation, TurnSmoothing * timeStep);

        character.rigidbody.MoveRotation(newRotation);
    }

    /// <summary>
    /// function helps with reynolds wander. This function will randomize the direction 
    /// </summary>
    /// <param name="dist"></param>
    /// <returns></returns>
    public Vector3 RandomDirection(float dist, float timeStep)
    {
        //get the orientation of the current character
        float _z = Mathf.Cos(character.transform.rotation.eulerAngles.y * Mathf.Deg2Rad);
        float _x = Mathf.Sin(character.transform.rotation.eulerAngles.y * Mathf.Deg2Rad);

        //place the target position in a front line in front of the character
        Vector3 target = character.transform.position;
        target.x += _x;
        target.z += _z;
        target = Vector3.Normalize(target) * dist;


        //get a random point inside a unit circle and scale it
        Vector2 randCircle = UnityEngine.Random.insideUnitCircle * 10f;
        //place a circle at the target's point, which will be the center of the velocity vector
        randCircle.x += target.x;
        randCircle.y += target.z;


        //calculate the new velocity, that is find the velocity from the middle of the circle to the chosen point. 
        CharacterVelocity.x = randCircle.x - target.x;
        CharacterVelocity.z = randCircle.y - target.z;
        CharacterVelocity = Vector3.Normalize(CharacterVelocity) * maxVelocity / 2;

        return CharacterVelocity;


    }

    IEnumerator RandomizeDirection(float waitTime, float dist)
    {
        yield return new WaitForSeconds(waitTime);
        Vector2 randCircle = UnityEngine.Random.insideUnitCircle * 5f;
        Vector3 target = character.transform.position.normalized * dist;
        randCircle.x += target.x;
        randCircle.y += target.z;
        target = Vector3.Normalize(target) * maxVelocity;
        CharacterVelocity = character.transform.position - target;
        CharacterVelocity = Vector3.Normalize(CharacterVelocity) * maxVelocity;

       // FixRotation();
    }



    private void DrawCircle(Vector3 target, float r)
    {

        float theta_scale = 0.1f; //set lower to add more points
        int size = (int)((2.0 * Mathf.PI) / theta_scale);

        LineRenderer lineRenderer = character.AddComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Particles/Additive"));
        lineRenderer.SetColors(Color.black, Color.black);
        lineRenderer.SetWidth(0.2f, 0.2f);
        lineRenderer.SetVertexCount(size);

        int i = 0;
        for (float theta = 0; theta < 2 * Mathf.PI; theta += 0.1f)
        {
            float _x = r * Mathf.Cos(theta);
            float _z = r * Mathf.Sin(theta);
            Vector3 pos = new Vector3(_x, 0, _z);
            lineRenderer.SetPosition(i, pos);
            i += 1;
        }

    }

    /// <summary>
    /// helper function that fixes the rotation of the object to face in the right direction
    /// </summary>
    private void FixRotation()
    {
        if (CharacterVelocity.sqrMagnitude > 0f)
            character.transform.rotation = Quaternion.LookRotation(CharacterVelocity.normalized, Vector3.up);
        //
    }

    private void drawCircle(float r, int segments, LineRenderer line)
    {
        float _x = 0;
        float _y = transform.position.y + 3f;
        float _z = 0;

        float theta = 0f;

        for (int i = 0; i < segments + 1; i++)
        {
            _x = r * Mathf.Sin(Mathf.Deg2Rad * theta) + transform.position.x;
            _z = r * Mathf.Cos(Mathf.Deg2Rad * theta) + transform.position.z;
            theta += (360f / segments);
            line.SetPosition(i, new Vector3(_x, _y, _z));
        }
    }

    public Vector3 Persuit(MovementBehaviour target, float timeStep, float timeInterval)
    {
        Vector3 directionVector3 = target.character.transform.position - character.transform.position;
        float distance = Vector3.Magnitude(directionVector3);
        float predictionTime; //prediction time
        if (maxVelocity < distance)
            predictionTime = maxPredictionTime;
        else
        {
            predictionTime = distance / maxVelocity;
        }
        //delegate to seek
        return SteeringSeek(target.character, timeStep);

    }

    public float Align(Transform target)
    {
        if (target.rotation.eulerAngles.y != character.transform.rotation.eulerAngles.y)
        {
            float rotation = target.transform.rotation.eulerAngles.y - transform.rotation.eulerAngles.y;
            //if we are within the range of satisfaction, stop rotating and return the targets rotation
            if (Mathf.Abs(target.rotation.eulerAngles.y - transform.rotation.eulerAngles.y) < satisfactionRotation)
                return target.rotation.eulerAngles.y;
            rotation = AdditionalVector3Tools.mapAngleToRange(rotation);

            //rotation sign

            float sign = Mathf.Sign(rotation);
            //now we compute the goal angular velocity

            float goalVelocity = (sign * maxAngularVelocity) * rotation / (sign * slowDownOrientation);

            //current character velocity

            float currCharVelocity = Vector3.Magnitude(rigidbody.velocity);

            //compute the angular acceleration

            float angularAcceleration = (goalVelocity - currCharVelocity) / timeToTarget;


            if (Mathf.Abs(angularAcceleration) > Mathf.Abs(maxAngularAcceleration))
                angularAcceleration = maxAngularAcceleration * sign;

            //ensure angular velocity sign is the same as rotation
            if (Mathf.Sign(maxAngularVelocity) != sign)
                angularVelocity *= sign;
            angularVelocity += angularAcceleration * Time.deltaTime;

            if (Mathf.Abs(angularVelocity) > maxAngularVelocity)
                angularVelocity = Mathf.Abs(maxAngularVelocity) * sign;




            float newAngle = character.transform.rotation.eulerAngles.y + Time.deltaTime * angularVelocity;
            return newAngle;
        }
        else return character.transform.rotation.eulerAngles.y;

    }
    float Align3(GameObject target)
    {

        if (target.transform.rotation.eulerAngles.y != transform.rotation.eulerAngles.y)
        {
            float rotation = target.transform.rotation.eulerAngles.y - transform.rotation.eulerAngles.y;
            rotation = AdditionalVector3Tools.mapAngleToRange(rotation);
            //if we are within the range of satisfaction, stop rotating and return the targets rotation
            if (Mathf.Abs(target.transform.rotation.eulerAngles.y - transform.rotation.eulerAngles.y) < satisfactionRotation)
                return target.transform.rotation.eulerAngles.y;


            //rotation sign

            float sign = Mathf.Sign(rotation);
            //now we compute the goal angular velocity

            float goalVelocity = (sign * maxAngularVelocity) * rotation / (sign * slowDownOrientation);

            //current character velocity

            float currCharVelocity = Vector3.Magnitude(rigidbody.velocity);

            //compute the angular acceleration

            float angularAcceleration = (goalVelocity - currCharVelocity) / timeToTarget;


            if (Mathf.Abs(angularAcceleration) > Mathf.Abs(maxAngularAcceleration))
                angularAcceleration = maxAngularAcceleration * sign;

            //ensure angular velocity sign is the same as rotation
            if (Mathf.Sign(maxAngularVelocity) != sign)
                angularVelocity *= sign;
            angularVelocity += angularAcceleration * Time.deltaTime;

            if (Mathf.Abs(angularVelocity) > maxAngularVelocity)
                angularVelocity = Mathf.Abs(maxAngularVelocity) * sign;




            float newAngle = character.transform.rotation.eulerAngles.y + Time.deltaTime * angularVelocity;
            return newAngle;
        }
        else return character.transform.rotation.eulerAngles.y;


    }


    /// <summary>
    /// faces the character towards the target
    /// </summary>
    public void Face(Transform target, float timeStep)
    {
        Vector3 direction = target.position - character.transform.position;
        if (direction.magnitude < 0.01f && direction.magnitude > 0.01f)
            return;


        float targetOrientation = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        Align2(targetOrientation, timeStep);

    }
    /// <summary>
    /// faces the character away from the target
    /// </summary>
    /// <param name="target"></param>
    /// <param name="timeStep"></param>
    public void FaceAway(Transform target, float timeStep)
    {
        Vector3 direction = target.position - character.transform.position;
        if (direction.magnitude < 0.01f && direction.magnitude > 0.01f)
            return;


        float targetOrientation = Mathf.Atan2(-direction.x, direction.z) * Mathf.Rad2Deg;
        Align2(targetOrientation, timeStep);

    }

    public void LookWhereYoureGoing(float timeStep)
    {
        if (character.rigidbody.velocity.magnitude < 0.01f && character.rigidbody.velocity.magnitude > -0.01f)
            return;
        else
        {
            float orientation = Mathf.Atan2(-character.rigidbody.velocity.x, character.rigidbody.velocity.z);
            Align2(orientation,timeStep);
        }

    }

    public void FaceAway(float timeStep)
    {
    }

    void Align(float targetOrientation, float timeStep)
    {



        float rotation = targetOrientation - character.transform.rotation.eulerAngles.y;
        rotation = AdditionalVector3Tools.mapAngleToRange(rotation);
        Vector3 charRotation = character.transform.rotation.eulerAngles;

        //if we are within the range of satisfaction, stop rotating and return the targets rotation
        if (Mathf.Abs(Mathf.Abs(targetOrientation) - Mathf.Abs(transform.rotation.eulerAngles.y)) < satisfactionRotation)
        {
            charRotation.y = targetOrientation;
            character.transform.rotation = Quaternion.Euler(charRotation);
            return;
        }


        //rotation sign

        float sign = Mathf.Sign(rotation);
        //now we compute the goal angular velocity

        float goalVelocity = (sign * maxAngularVelocity) * rotation / (sign * slowDownOrientation);

        //current character velocity

        float currCharVelocity = Vector3.Magnitude(character.rigidbody.velocity);

        //compute the angular acceleration

        float angularAcceleration = (goalVelocity - currCharVelocity) / timeToTarget;


        if (Mathf.Abs(angularAcceleration) > Mathf.Abs(maxAngularAcceleration))
        {

            angularAcceleration = maxAngularAcceleration * sign;
        }

        //ensure angular velocity sign is the same as rotation
        if (Mathf.Sign(maxAngularVelocity) != sign)
        {

            angularVelocity *= sign;
        }
        angularVelocity += angularAcceleration * timeStep;

        if (Mathf.Abs(angularVelocity) > maxAngularVelocity)
        {

            angularVelocity = Mathf.Abs(maxAngularVelocity) * sign;
        }




        charRotation.y = character.transform.rotation.eulerAngles.y + angularVelocity * timeStep;
        character.transform.rotation = Quaternion.Euler(charRotation);




    }

    public void Align2(float targetOrientation, float timeStep)
    {
        float rotation = targetOrientation - character.transform.rotation.eulerAngles.y;
        rotation = AdditionalVector3Tools.mapAngleToRange(rotation);
        Vector3 charAngles = character.transform.rotation.eulerAngles;
        float rotationSize = Mathf.Abs(Mathf.Abs(targetOrientation) - Mathf.Abs(transform.rotation.eulerAngles.y));
        //if we are within the range of satisfaction, stop rotating and return the targets rotation
        if (rotation < slowDownOrientation)
        {
            charAngles.y = targetOrientation;
            character.transform.rotation = Quaternion.Euler(charAngles);
            return;
        }
        float sign = Mathf.Sign(rotation);
        float goalVelocity = (sign * maxAngularVelocity) * (rotation * sign) / (sign * slowDownOrientation);
        float angularAcceleration = (goalVelocity - characterAngularVelocity) / timeToTarget;
        float angle = charAngles.y;
        if (Mathf.Abs(angularAcceleration) < Mathf.Abs(maxAngularAcceleration))
        {
            characterAngularVelocity = characterAngularVelocity + angularAcceleration*timeStep;
        }
        else
        {
            characterAngularVelocity = (sign) *angularAcceleration;
        }
        if (characterAngularVelocity < maxAngularVelocity)
        {
            charAngles.y = angle + characterAngularVelocity*timeStep;
            character.transform.rotation = Quaternion.Euler(charAngles);
            return;
        }

    }
    public void LookWhereYoureGoing()
    { }



    public Vector3 Evade(MovementBehaviour target)
    {

        return Vector3.one;

    }

    #endregion


    #region kinematic 

    /// <summary>
    /// implmenetation of the kinematic arrive algorithm.
    /// move towards the target at max velocity and stop immediately at a satisfaction radius
    /// </summary>
    public void KinematicArrive()
    {
        CharacterVelocity = TargetGameObject.transform.position - character.transform.position;
        float mag = CharacterVelocity.magnitude;
        float currentVelocity = 0;
        if (mag > ArrivalRadius)
        {

            currentVelocity = Mathf.Min(maxVelocity, mag/timeToTarget);
        }
        else
        {
            currentVelocity = 0; 
            print(character.rigidbody.velocity);
        }
           

        character.rigidbody.velocity = CharacterVelocity.normalized*currentVelocity;
        // KinematicSeek();
    }

    /// <summary>
    /// 
    /// </summary>
    public void KinematicSeek()
    {
        CharacterVelocity = TargetGameObject.transform.position - character.transform.position;
        character.rigidbody.velocity = CharacterVelocity.normalized * maxVelocity;

    }

    public void KinematicFlee()
    {
        CharacterVelocity = character.transform.position - TargetGameObject.transform.position;
        character.rigidbody.velocity = CharacterVelocity.normalized * maxVelocity;
    }
    /// <summary>
    /// linearly interpolate the rotation to face the target
    /// </summary>
    public void InterpolateRotate()
    {
       /* Vector3 currentOrientation = character.transform.rotation.eulerAngles;
        Vector3 directionVector3 = (TargetGameObject.transform.position - character.transform.position).normalized;
       // print(gameObject.name + " is trying to point to " + TargetGameObject.transform.position + " and its direction is  " + directionVector3.x);
        float angle = Mathf.Atan2(directionVector3.z, directionVector3.x)* Mathf.Rad2Deg;
      // currentOrientation.y = Mathf.Lerp(currentOrientation.y, angle, Time.deltaTime * 5f);
        currentOrientation.y = angle;
      //  print(TargetGameObject.name + " and the angle " + angle);
        character.transform.rotation = Quaternion.Euler(currentOrientation);*/
        Vector3 aimingDirection = _targetGameObject.transform.position - transform.position;

        var heading = _targetGameObject.transform.position - transform.position;
        var newHeading = new Vector2(heading.x, heading.z).normalized;
        var currentHeading = new Vector2(transform.forward.x, transform.forward.z).normalized;
        float angle = Mathf.Atan2(newHeading.x, newHeading.y) * Mathf.Rad2Deg;
        Quaternion rot = Quaternion.Euler(new Vector3(0, angle, 0));
        transform.rotation = Quaternion.Lerp(transform.rotation, rot, Time.deltaTime * 2.0f);
    }

    #endregion
}
