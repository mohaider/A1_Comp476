using UnityEngine;
using System.Collections;

public class WallBoundsCreation : MonoBehaviour {
    #region class variables and properties
    public Terrain terrain;
    private Vector3 terrainSize;
    [SerializeField] private GameObject wallbounds;
    #endregion


	// Use this for initialization
	void Start () {
        //get terain data to attach colliders to the sides of the terrain in order to start terrain wrapping
       // terrain = gameObject.GetComponent<Terrain>();
        terrainSize = terrain.terrainData.size;
        Vector3 terrainPos = transform.position;
        BoxCollider[] boxColliders = new BoxCollider[4];
        for (int i = 0; i < boxColliders.Length; i++)
        {
            boxColliders[i] = wallbounds.AddComponent<BoxCollider>();
            boxColliders[i].isTrigger = true;
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


	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
