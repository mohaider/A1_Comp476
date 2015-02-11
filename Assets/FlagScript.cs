using UnityEngine;
using System.Collections;

public class FlagScript : MonoBehaviour
{
    public enum FlagState
    {
        Taken,
        AtHome,
        BeingReturned
    }

    public GameObject newParent;
    public GameObject home;

    [SerializeField]
    private GameObject enemyCarrier;
    FlagState _flagState = FlagState.AtHome;

    public FlagState flagState
    {
        get { return _flagState; }
        set { _flagState = value; }
    }

    public GameObject EnemyCarrier
    {
        get { return enemyCarrier; }
        set { enemyCarrier = value; }
    }

    #region class function


    /// <summary>
    /// activate the flag's collider
    /// </summary>
    public void ActivateCollider()
    {
        SphereCollider collider = this.GetComponent<SphereCollider>();
        collider.enabled = true;
        //need to notify the manager that the flag is returned
    }
    /// <summary>
    /// deactivate the flag's collider
    /// </summary>
    public void DeactivateCollider()
    {
        SphereCollider collider = this.GetComponent<SphereCollider>();
        collider.enabled = false;
        //need to notify the manager that the flag is taken
    }


    /// <summary>
    /// this method deactivates the collider of this game object and sets the flag position according to its new parent
    /// </summary>
    /// <param name="newParent"></param>
    public void PickupFlag(GameObject newParent)
    {
        DeactivateCollider();
        this.newParent = newParent;
        this.gameObject.transform.position = newParent.transform.position; //set the position to that of the parent's position
        this.gameObject.transform.rotation= newParent.transform.rotation;  //set the rotation to that of the parent's rotation
        this.gameObject.transform.parent = newParent.transform;            //set the transforms parent to the new parent's
        enemyCarrier = newParent.transform.parent.gameObject;
        _flagState = FlagState.Taken;
        //notify the tagger

    }

    public void PlaceFlagDown(GameObject newParent)
    {
        
    }

    public void ResetToHomePosition()
    {
        this.newParent = home;
        this.gameObject.transform.position = newParent.transform.position; //set the position to that of the parent's position
        this.gameObject.transform.rotation = newParent.transform.rotation;  //set the rotation to that of the parent's rotation
        this.gameObject.transform.parent = newParent.transform;            //set the transforms parent to the new parent's
        enemyCarrier = null;
        _flagState = FlagState.AtHome;
        ActivateCollider();
    }



    #endregion
}
