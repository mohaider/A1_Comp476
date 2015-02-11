using UnityEngine;
using System.Collections;

public class AreaBound : MonoBehaviour
{
    #region class variables and properties

    private Rect rect;

    public Rect Rect1
    {
        get { return rect; }
        set { rect = value; }
    }

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
