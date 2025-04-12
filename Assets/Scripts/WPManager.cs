using UnityEngine;


[System.Serializable]

// Manages a collection of Nodes, Edges and Links

// represents a Uni- or billateral connection between
// Nodes
public class Link
{
    public enum direction { UNI, BI }

    public GameObject node1;
    public GameObject node2;

    public direction dir;
}

public class WPManager : MonoBehaviour
{

    public GameObject[] wayPoints;
    public Link[] links;

    public Graph graph = new Graph();


    void Start()
    {
        if (wayPoints.Length > 0)
        {
            foreach (GameObject wp in wayPoints)
            {
                graph.AddNode(wp);
            }
            foreach (Link l in links)
            {
                graph.AddEdge(l.node1, l.node2);

                // if the direction is BI, connect 
                // nodes in both directions
                if (l.dir == Link.direction.BI)
                {
                    graph.AddEdge(l.node2, l.node1);
                }
            }
        }
    }

}
