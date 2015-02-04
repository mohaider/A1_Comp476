using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TeamBuilder : MonoBehaviour
{

    #region class variables and properties

    [SerializeField]  int playerPerTeam;
    [SerializeField] private GameObject teamOneMember;
    [SerializeField] private GameObject teamTwoMember;
    [SerializeField]
    private GameObject TeamOneSpawnAreaA;
    [SerializeField]
    private GameObject TeamTWoSpawningAreaA;
    [SerializeField]
    private GameObject TeamOneSpawnAreaB;
    [SerializeField]
    private GameObject TeamTWoSpawningAreaB;
    [SerializeField] private int positionalRadius; //this will be used for randomizing team members positons around the spawning points
    private GameObject[] teamAgents;
    private List<GameObject> teamOnePool;
    private List<GameObject> teamTwoPool;
    
    public List<GameObject> TeamOnePool
    {
        get { return teamOnePool; }
    }

    public List<GameObject> TeamTwoPool
    {
        get { return teamTwoPool; }
    }

    #endregion

    #region unity functions
    // Use this for initialization
	void Start () {
	   
        teamAgents = new GameObject[playerPerTeam * 2]; 
        teamOnePool = new List<GameObject>();
	    teamTwoPool = new List<GameObject>();
	    bool switchSides = false;
        //instantiate players in a random area around the spawning point
	    for (int i = 0; i < playerPerTeam; i++)
	    {
            //get random position
	        Vector3 teamOnePos = Vector3.zero;
            Vector3 teamTwoPos = Vector3.zero; 

	        if (!switchSides)
	        {
                teamOnePos = UnityEngine.Random.insideUnitSphere * positionalRadius +
               TeamOneSpawnAreaA.transform.position;
                teamTwoPos = UnityEngine.Random.insideUnitSphere * positionalRadius +
                   TeamTWoSpawningAreaA.transform.position;
	        }
            else
            {
                teamOnePos = UnityEngine.Random.insideUnitSphere * positionalRadius +
                    TeamOneSpawnAreaB.transform.position;
                teamTwoPos = UnityEngine.Random.insideUnitSphere * positionalRadius +
                   TeamTWoSpawningAreaB.transform.position;
            }
	         
            
	        teamOnePos.y = 0;
	        teamTwoPos.y = 0;
            
	        GameObject tm1 = Instantiate(teamOneMember,teamOnePos,Quaternion.identity) as GameObject;//Team Member one
            tm1.name = "A team #+" +i;
	        teamAgents[i] = tm1;
	        teamOnePool.Add(tm1);

            GameObject tm2 = Instantiate(teamTwoMember, teamTwoPos, Quaternion.identity) as GameObject;//Team Member one
            teamAgents[i] = tm2;
            teamTwoPool.Add(tm2);
            tm2.name = "B team #+" + i;

	        switchSides = !switchSides;

	    }
	}
	
	// Update is called once per frame
	void Update () {

    }
    #endregion
}
