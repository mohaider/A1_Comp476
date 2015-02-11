using UnityEngine;
using System.Collections;
using Assets.Scripts.Character;

public class UntaggerSetterSM : MonoBehaviour
{
    #region class variables and properties

    [SerializeField] private GameObject untagger;
    private Queue taggedPlayers;
    private ArrayList taggedPlayersList;
    private ArrayList teamPool;
    private ArrayList UntaggerList;

    public GameObject Untagger
    {
        get { return untagger; }
        set { untagger = value; }
    }

    public ArrayList TaggedPlayersList
    {
        get { return taggedPlayersList; }
        set { taggedPlayersList = value; }
    }

    
    #endregion
    #region unity functions
    // Use this for initialization
    private void Start()
    {
        taggedPlayers = new Queue();
        taggedPlayersList = new ArrayList();
        teamPool = gameObject.GetComponent<TeamManager>().TeamPool;
        UntaggerList = new ArrayList();
    }

    // Update is called once per frame
    void Update()
    {
        if (taggedPlayersList.Count != 0) //there are players that are still tagged
        {
 


        }
    }
    #endregion
    #region class functions

    /// <summary>
    /// adds a tagged player to the list
    /// </summary>
    /// <param name="taggedPlayer"></param>
    public void AddNewTaggedPlayer(GameObject taggedPlayer)
    {
        /*taggedPlayers.Enqueue(taggedPlayer);
        taggedPlayers.Dequeue();*/
        bool playerExists = taggedPlayersList.Contains(taggedPlayer);
        if (playerExists)
            return;

        else
        {
            
            if (CanAssignUntagger(taggedPlayer)) //if we can assign an untagger, then add one and assign tagged player an untagger
                taggedPlayersList.Add(taggedPlayer);
        }

    }

    public void RemoveTaggedPlayer(GameObject taggedPlayer)
    {
        bool playerExists = taggedPlayersList.Contains(taggedPlayer);
        if (!playerExists)
        {
            Debug.Log("for some reason this tagged player was never assinged. This should not happen");
            return;
        }
        else
        {
            taggedPlayersList.Remove(taggedPlayer);
        }
    }
    /// <summary>
    /// If possible, assign an untagger to this player
    /// </summary>
    /// <param name="taggedPlayer"></param>
    private bool CanAssignUntagger(GameObject taggedPlayer)
    {
        GameObject untagger = GetAnUntagger(taggedPlayer);
        if (!untagger) //if none are found exit this 
            return false;
        else
        {
            untagger.GetComponent<UntaggerSM>().Set(taggedPlayer);
            // untagger.GetComponent<PlayerStateListener>().TargetAgent = taggedPlayer;
            //untagger.GetComponent<PlayerStateController>().ChangeState(PlayerStateController.PlayerState.untagger);
            return true;
        }
    }

    public void AddNewUntagger(GameObject untagger)
    {
        /*taggedPlayers.Enqueue(taggedPlayer);
      taggedPlayers.Dequeue();*/
        bool playerExists = UntaggerList.Contains(untagger);
        if (playerExists)
            return;
        else
        {
            UntaggerList.Add(untagger);
        }
    }

  
    //private ArrayList GetListOfUntagPlay
    /// <summary>
    /// create a list of idle players, then find the closest one to the current player
    /// </summary>
    /// <returns></returns>
    private GameObject GetAnUntagger(GameObject taggedObject)
    {
        ArrayList idlePlayers = new ArrayList();
        
        for (int i = 0; i < teamPool.Count; i++) //find idle players
        {
            GameObject player = teamPool[i] as GameObject;
            if (player.GetComponent<PlayerStateListener>().CurrentState != PlayerStateController.PlayerState.idle)
                continue;
            else
                idlePlayers.Add(player);
        }
        if (idlePlayers.Count == 0) //no idle players
            return null;
        else
        {
            float minDist = Mathf.Infinity;
            int indexOfSmallest = -1;
            for (int i = 0; i < idlePlayers.Count; i++)
            {

                GameObject player = (GameObject)idlePlayers[i];

                Vector3 direction = taggedObject.transform.position - player.transform.position;
                direction.y = 0;//flatten y
                float tempDist = direction.magnitude;
                if (tempDist < minDist)
                {
                    indexOfSmallest = i;
                    minDist = tempDist;
                }
            }
            return idlePlayers[indexOfSmallest] as GameObject;
        }

        return null;
    }

    #endregion

  
}
