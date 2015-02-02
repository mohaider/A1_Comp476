using UnityEngine;
using System.Collections;

public class TerrainWrapping : MonoBehaviour
{
    #region class variables and properties
    private Terrain terrain;
    private Vector3 terrainSize;
    public LayerMask layerMask;
    #endregion
    // Use this for initialization
    #region unity functions
    void Start()
    {
        //get terain data to attach colliders to the sides of the terrain in order to start terrain wrapping
        terrain = gameObject.GetComponent<Terrain>();
        terrainSize = terrain.terrainData.size;
        Vector3 terrainPos = transform.position;
        BoxCollider[] boxColliders = new BoxCollider[4];
        for (int i = 0; i < boxColliders.Length; i++)
        {
            boxColliders[i] = gameObject.AddComponent<BoxCollider>();
        }

        // width, length
        float w = terrainSize.x;
        float l = terrainSize.z;


        //set boxcollider 1 paramaters
        boxColliders[0].center = new Vector3(terrainPos.x, terrainPos.y, terrainPos.z + l / 2);
        boxColliders[0].size = new Vector3(5f, 400f, l);
        //set boxcollider 2 parameters
        boxColliders[1].center = new Vector3(terrainPos.x + w / 2, terrainPos.y, terrainPos.z + l);
        boxColliders[1].size = new Vector3(w, 400f, 5f);
        //set boxcollider 3 parameters
        boxColliders[2].center = new Vector3(terrainPos.x + w, terrainPos.y, terrainPos.z + l / 2);
        boxColliders[2].size = new Vector3(5f, 400f, l);
        //set boxcollider 4 parameters
        boxColliders[3].center = new Vector3(terrainPos.x + w / 2, terrainPos.y, terrainPos.z);
        boxColliders[3].size = new Vector3(w, 400f, 5f);

        //side 2

        //side 3

        //side 4


    }

    // Update is called once per frame
    void Update()
    {

        print(terrainSize);
    }

    #region collision detection
    void OnTriggerEnter(Collider col)
    {
        if ((col.tag == "NPC_Agent" || col.tag == "Player" ))
        {
            RaycastHit hit;
            if (Physics.Raycast(col.transform.position, -col.transform.position.normalized, out hit, Mathf.Infinity, layerMask))
            {
                Vector3 prevPos = col.transform.position;
                Vector3 currPos = hit.point;
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
