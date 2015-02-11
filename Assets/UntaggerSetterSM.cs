using UnityEngine;
using System.Collections;

public class UntaggerSetterSM : MonoBehaviour
{
    #region class variables and properties

    [SerializeField] private GameObject untagger;

    public GameObject Untagger
    {
        get { return untagger; }
        set { untagger = value; }
    }

    #endregion

    #region class functions

    #endregion

    #region unity functions
    // Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

    }
    #endregion
}
