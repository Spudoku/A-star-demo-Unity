using System.Linq;
using Unity.Mathematics;
using UnityEngine;

public class FollowWaypoint : MonoBehaviour
{

    public GameObject[] waypoints;

    GameObject tracker;
    int currentWayPoint = 0;

    public float rotSpeed = 30f;
    public float speed = 10.0f;

    public float lookAhead = 10f;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tracker = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        DestroyImmediate(tracker.GetComponent<Collider>());
        tracker.GetComponent<MeshRenderer>().enabled = false;
        tracker.transform.SetPositionAndRotation(transform.position, transform.rotation);
    }

    void ProgressTracker()
    {
        if (Vector3.Distance(tracker.transform.position, transform.position) >= lookAhead) return;

        Vector3 currentWPPos = waypoints[currentWayPoint].transform.position;
        if (Vector3.Distance(tracker.transform.position, currentWPPos) < 3.0f)
        {
            currentWayPoint++;
            if (currentWayPoint >= waypoints.Length)
            {
                currentWayPoint = 0;
            }
        }
        tracker.transform.LookAt(currentWPPos);
        tracker.transform.Translate(0, 0, speed * 5.1f * Time.deltaTime);
    }

    void Update()
    {
        Vector3 currentWPPos = waypoints[currentWayPoint].transform.position;

        ProgressTracker();

        //transform.LookAt(waypoints[currentWayPoint].transform.position);

        // SLERP (so thirsty)
        Quaternion lookatWP = quaternion.LookRotation(tracker.transform.position - transform.position, Vector3.up);

        transform.rotation = Quaternion.Slerp(transform.rotation, lookatWP, Time.deltaTime * rotSpeed);
        transform.Translate(0, 0, speed * Time.deltaTime);


    }
}
