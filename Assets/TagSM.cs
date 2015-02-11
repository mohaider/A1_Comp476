using UnityEngine;
using System.Collections;

public class TagSM : MonoBehaviour
{

    [SerializeField] private bool isTagger=false;
    [SerializeField] private GameObject enemy;
    [SerializeField] private GameObject ourFlag;

    public bool IsTagger
    {
        get { return isTagger; }
        set { isTagger = value; }
    }

    public GameObject Enemy
    {
        get { return enemy; }
        set { enemy = value; }
    }
    #region unity functions
    // Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

    }

    void OnCollisionEnter(Collision collision)
    {
            
    }
    #endregion
}
