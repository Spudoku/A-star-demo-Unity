using System.Linq;
using Unity.Mathematics;
using UnityEngine;

public class FollowWaypoint : MonoBehaviour
{

    Transform goal;

    float speed = 5.0f;
    float accuracy = 5.0f;
    float rotSpeed = 2.0f;

    public GameObject wpManager;
    GameObject[] wps;
    GameObject currentNode;
    int curWP = 0;      // tracks index in a path, not in overall waypoint list
    Graph g;


    public float lookAhead = 10f;

    void Start()
    {
        wps = wpManager.GetComponent<WPManager>().wayPoints;
        g = wpManager.GetComponent<WPManager>().graph;

        currentNode = wps[0];

        Invoke(nameof(GoTo6), 2);
    }


    public void GoTo6()
    {
        GoToPoint(5);
    }

    public void GoToPoint(int index)
    {
        g.AStar(currentNode, wps[index]);
        curWP = 0;
    }

    void LateUpdate()
    {
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
}
