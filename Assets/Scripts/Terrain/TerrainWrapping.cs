﻿using UnityEngine;
using System.Collections;

public class TerrainWrapping : MonoBehaviour
{
    #region class variables and properties
   
    public LayerMask layerMask;
    #endregion

    #region unity functions
    void Start()
    {
       


    }

    // Update is called once per frame
    void Update()
    {


    }

    #region collision detection
    void OnTriggerEnter(Collider col)
    {
        if ((col.tag == "NPC_Agent" || col.tag == "Player" || col.tag == "TeamOrange" || col.tag == "TeamBanana"))
        {
            RaycastHit hit;
            if (Physics.Raycast(col.transform.position, -col.rigidbody.velocity.normalized, out hit, Mathf.Infinity, layerMask))
            {
                Vector3 currPos = hit.point ;
                col.transform.position = currPos;
            }

        }


    }
    void OnTriggerStay(Collider col)
    {
        //Debug.DrawLine (col.transform.position, Vector3.zero,Color.black);
    }

    private void OnTriggerExit(Collider col)
    {
    }

    #endregion


    #endregion
}
