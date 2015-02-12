using UnityEngine;
using System.Collections;

public class Seperation : MonoBehaviour
{

    private ArrayList teamPool;

    [SerializeField] private float threshold;
    [SerializeField] private float decayCoefficient;
    [SerializeField] private float maxAcceleration;

    public float Threshold
    {
        get { return threshold; }
        set { threshold = value; }
    }

    public float DecayCoefficient
    {
        get { return decayCoefficient; }
        set { decayCoefficient = value; }
    }

    public float MaxAcceleration
    {
        get { return maxAcceleration; }
        set { maxAcceleration = value; }
    }

    // Use this for initialization
	void Start () {
        teamPool = gameObject.GetComponent<TeamManager>().TeamPool;
	}
	
	// Update is called once per frame
	void Update () {
	    for (int i = 0; i < teamPool.Count; i++)
	    {
	        
	    }
	
	}
}
