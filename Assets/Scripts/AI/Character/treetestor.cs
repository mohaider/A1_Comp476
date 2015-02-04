using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.Script.Tools;
using Assets.Scripts.Tools.ADT;
/*
 * Please disregard the following class as it was used for testing purposes. 
 * 
 * 
 * 
 */
public class treetestor : MonoBehaviour
{

    private Tree<string> tree;
    private Tree<string> t1;
    // Use this for initialization
    void Start()
    {
       
        tree = new Tree<string>();
        tree.AddRoot("test");

        t1=  new Tree<string>();
        t1.AddRoot("t2");

        List<string> ab = new Node<string>();
        ab.Add("first");
        for (int i  =0 ; i < 10; i++)
            ab.Add(""+i);
        t1.AddChild(ab,"what");
       
    }

    // Update is called once per frame
    void Update()
    {

    }
}
