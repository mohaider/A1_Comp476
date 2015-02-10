using UnityEngine;
using System.Collections;
using Assets.Scripts.Character;

public class GrabberController : MonoBehaviour
{
    public Vector3 dragVector;
    public GameObject cursorObject;
    public Vector3 cursorPosition;
    public Vector3 prevCursorPosition;
    public UnityEngine.UI.Text info;
    private MovementBehaviour movementBehaviourinfo;
    private PlayerStateListener playerlisterStateListener;

    private static GrabberController instance;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {


        // track cursor position
        prevCursorPosition = cursorPosition;
        cursorPosition = GetMousePositionYZero();

        // reset drag vector
        dragVector = Vector3.zero;

        if (Input.GetMouseButtonDown(0))
        {
            if (cursorObject != null)
                if (cursorObject.tag != "NPC_Agent") //only grab the character
                {
                    cursorObject = null;
                    return;
                }

            // attempt to find cursor object
            cursorObject = GetCursorObject();

        }
        // listen for mouse down events
        else if (Input.GetMouseButton(0))
        {
            // holding
            dragVector = cursorPosition - prevCursorPosition;
        }

        DisplayInfo();
    }

    void DisplayInfo()
    {
        if (cursorObject != null)
        {

            /* if (cursorObject.GetComponent<MovementBehaviour>() != null)
                 movementBehaviourinfo = cursorObject.GetComponent<MovementBehaviour>();
             if (movementBehaviourinfo != null && info != null)
                 info.text = movementBehaviourinfo.outputInfo;*/
            //cursorObject.name + "'s velocity: " + movementBehaviourinfo.CharacterVelocity;
            if (cursorObject.GetComponent<PlayerStateListener>() != null)
            {
                playerlisterStateListener = cursorObject.GetComponent<PlayerStateListener>();
                if (playerlisterStateListener != null && info != null)
                    info.text = playerlisterStateListener.Outputinfo;
                //info.text = playerlisterStateListener.GetCurrentState();
            }
        }
    }

    /// <summary>
    /// Singleton instance
    /// </summary>
    public static GrabberController Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject container = new GameObject();
                container.name = "GrabberController";
                instance = container.AddComponent<GrabberController>();
            }

            return instance;
        }
    }

    private GameObject GetCursorObject()
    {
        // get mouse position data
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // get the object (if any) immediately under the cursor
        RaycastHit hit;
        Physics.Raycast(ray, out hit);

        return hit.collider != null ? hit.collider.gameObject : null;
    }

    public static Vector3 GetMousePositionYZero()
    {
        if (Camera.main != null)
        {
            Vector3 pos = Camera.main.ScreenToWorldPoint(new Vector3(
                                                                        Input.mousePosition.x,
                                                                        Input.mousePosition.y,
                                                                        Camera.main.transform.position.y
                                                                    ));
            return new Vector3(pos.x, 0f, pos.z);
        }
        else
        {
            return Vector3.zero;
        }
    }
}
