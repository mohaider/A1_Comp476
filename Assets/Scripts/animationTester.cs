using UnityEngine;
using System.Collections;

public class animationTester : MonoBehaviour
{

//    private AnimationClip[] clips;
    private ArrayList clips;
    private ArrayList clipsName = new ArrayList();
    private int index = 0;
	// Use this for initialization
	void Start ()
	{
	    foreach (AnimationState clip in animation)
	    {

            clipsName.Add(clip.name);
	    } 

	}
	
	// Update is called once per frame
	void Update ()
	{
	    if (Input.GetKeyDown(KeyCode.F1))
	    {
	       
            animation.Play((string)clipsName[index]);
            print("now playing " + (string)clipsName[index]);
	        index++;
	        if (index == clipsName.Count )
                index = 0;
	    }
        if (Input.GetKeyDown(KeyCode.F2))
        {

            animation.Play((string)clipsName[index]);
            print("now playing " + (string)clipsName[index]);
            index--;
            if (index < 0)
                index = clipsName.Count -1;
        }
	    if (Input.GetKeyDown(KeyCode.R))
	        index = 0;
	    if (Input.GetKeyDown(KeyCode.A))
	        animation.Play("worry");
	}
}
