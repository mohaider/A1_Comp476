using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TeamBuilder : MonoBehaviour
{

    #region class variables and properties

    [SerializeField]  int playerPerTeam;
    [SerializeField] private GameObject flag1;
    [SerializeField] private GameObject flag2;
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
    private ArrayList teamOnePool;
    private ArrayList teamTwoPool;

    public ArrayList TeamOnePool
    {
        get { return teamOnePool; }
    }

    public ArrayList TeamTwoPool
    {
        get { return teamTwoPool; }
    }

    public GameObject Flag1
    {
        get { return flag1; }
    }

    public GameObject Flag2
    {
        get { return flag2; }
    }

    public GameObject[] TeamAgents
    {
        get { return teamAgents; }
    }

    //  private List<GameObject> teamOnePool;
    //private List<GameObject> teamTwoPool;
    
  /*  public List<GameObject> TeamOnePool
    {
        get { return teamOnePool; }
    }

    public List<GameObject> TeamTwoPool
    {
        get { return teamTwoPool; }
    }*/

    #endregion

    #region unity functions
    // Use this for initialization
	void Awake () {
        print("Teambuilder");
        teamAgents = new GameObject[playerPerTeam * 2]; 
        teamOnePool = new ArrayList();
        teamTwoPool = new ArrayList();
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
            
	        GameObject tm1 = Instantiate(teamOneMember,teamOnePos,teamOneMember.transform.rotation) as GameObject;//Team Member one
            tm1.name = "Agent A" +i;
	        teamAgents[i] = tm1;
	        teamOnePool.Add(tm1);

            GameObject tm2 = Instantiate(teamTwoMember, teamTwoPos, teamTwoMember.transform.rotation) as GameObject;//Team Member one
            teamAgents[i] = tm2;
            teamTwoPool.Add(tm2);
            tm2.name = "Agent B" + i;

	        switchSides = !switchSides;

	    }
	}
	
	// Update is called once per frame
	void Update () {

    }
    #endregion
}
