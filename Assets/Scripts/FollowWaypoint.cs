using UnityEngine;

public class FollowWaypoint : MonoBehaviour
{
    Transform goal;

    [SerializeField] float speed = 10.0f;
    [SerializeField] float accuracy = 5.0f;     // how close the gameObject must be in order to 'reach' 
                                                // a node
    [SerializeField] float rotSpeed = 2.0f;     // how quickly this object can rotate, in degrees per frame

    public GameObject wpManager;
    GameObject[] wps;
    GameObject currentNode;
    int curWP = 0;      // tracks index in a path, not in overall waypoint list
    Graph g;

    GUIStyle debugStyle;
    public float lookAhead = 10f;

    void Start()
    {
        wps = wpManager.GetComponent<WPManager>().wayPoints;
        g = wpManager.GetComponent<WPManager>().graph;


        // initialize debug style


        // wait for waypoints to load
        //Invoke(nameof(GoTo6), 2);

    }


    public void GoTo6()
    {
        GoToPoint(5);
    }

    public void GoToPoint(int index)
    {
        currentNode = GetClosestWP();
        if (g.AStar(currentNode, wps[index]))
        {
            Debug.Log($"Found path to waypoint " + index);
        }
        else
        {
            Debug.Log($"No path found to {index}");
        }
        curWP = 0;
    }

    void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log("Going to point 1");
            GoToPoint(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Debug.Log("Going to point 2");
            GoToPoint(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Debug.Log("Going to point 3");
            GoToPoint(2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Debug.Log("Going to point 4");
            GoToPoint(3);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            Debug.Log("Going to point 5");
            GoToPoint(4);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            Debug.Log("Going to point 6");
            GoToPoint(5);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            Debug.Log("Going to point 7");
            GoToPoint(6);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            Debug.Log("Going to point 8");
            GoToPoint(7);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            Debug.Log("Going to point 9");
            GoToPoint(8);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            Debug.Log("Going to point 10");
            GoToPoint(9);
        }

        if (g.pathList.Count <= 0 || curWP >= g.pathList.Count) return;

        if (Vector3.Distance(transform.position, g.pathList[curWP].getID().transform.position) < accuracy)
        {
            currentNode = g.pathList[curWP].getID();
            curWP++;
        }

        if (curWP < g.pathList.Count)
        {
            goal = g.pathList[curWP].getID().transform;
            Vector3 lookAtGoal = new Vector3(goal.position.x, transform.position.y, goal.transform.position.z);

            Vector3 dir = lookAtGoal - transform.position;

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * rotSpeed);

            transform.Translate(0, 0, speed * Time.deltaTime);
        }


    }


    // display some information on the screen
    void OnGUI()
    {
        debugStyle = new GUIStyle(GUI.skin.label)
        {
            fontSize = 24
        };
        // Current Waypoint
        if (currentNode != null)
        {
            GUI.Label(new Rect(10, 10, 500, 50), $"Current waypoint: {currentNode.name}", debugStyle);
        }
        else
        {
            GUI.Label(new Rect(10, 10, 500, 50), $"Current waypoint: NULL", debugStyle);
        }
        // path list
        GUI.Label(new Rect(10, 60, 500, 100), PrintPathList(), debugStyle);

    }

    // return the nearest WayPoint
    GameObject GetClosestWP()
    {
        if (wps.Length <= 0)
        {
            return null;
        }
        GameObject nearest = wps[0];
        for (int i = 1; i < wps.Length; i++)
        {
            if (Vector3.Distance(wps[i].transform.position, transform.position) < Vector3.Distance(nearest.transform.position, transform.position))
            {
                nearest = wps[i];
            }
        }
        Debug.Log($"Nearest Waypoint is {nearest.name}");
        return nearest;
    }

    string PrintPathList()
    {
        string print = "Pathlist: ";
        foreach (Node p in g.pathList)
        {
            print += p.getID().name + " ";
        }
        return print;
    }
}
