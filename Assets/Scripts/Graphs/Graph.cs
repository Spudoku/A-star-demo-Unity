using System.Collections.Generic;
using UnityEngine;

// This class represents a Graph data structure,
// and implements the A* algorithm for navigation
public class Graph
{
    List<Edge> edges = new List<Edge>();
    List<Node> nodes = new List<Node>();

    public List<Node> pathList = new List<Node>();

    public Graph() { }

    // add node to graph
    public void AddNode(GameObject id)
    {
        Node node = new Node(id);
        nodes.Add(node);
    }

    // connect two nodes
    public void AddEdge(GameObject fromNode, GameObject toNode)
    {
        Node from = FindNode(fromNode);
        Node to = FindNode(toNode);

        if (from != null && to != null)
        {
            Edge edge = new(from, to);
            edges.Add(edge);
            from.edges.Add(edge);
        }

    }

    // find a Node with a GameObject 'ID'
    Node FindNode(GameObject id)
    {
        foreach (Node n in nodes)
        {
            if (n.getID() == id)
            {
                return n;
            }
        }
        return null;
    }


    // A* algorithm
    // uses hueristics to find the best path from start to end
    public bool AStar(GameObject startID, GameObject endID)
    {
        // retrieve nodes
        Node start = FindNode(startID);
        Node end = FindNode(endID);

        if (start == null || end == null) return false;


        List<Node> open = new();            // Nodes that can be traveresed
        List<Node> closed = new();          // Nodes that have already been traversed
        bool tentativeIsBetter;

        start.g = 0;
        start.h = Distance(start, end);
        start.f = start.h;

        open.Add(start);

        while (open.Count > 0)
        {
            // access the lowest-f score node in
            // the Open list
            int i = lowestF(open);

            Node thisNode = open[i];

            // if true, Done!
            if (thisNode.getID() == endID)
            {
                // reconstruct path
                ReconstructPath(start, end);
                return true;
            }

            // make sure the current node can't be traversed again
            open.RemoveAt(i);
            closed.Add(thisNode);


            Node neighbor;

            // search all nodes adjacent to current
            foreach (Edge e in thisNode.edges)
            {
                // neighbor refers to the node pointed to by Edge e
                neighbor = e.endNode;

                if (closed.IndexOf(neighbor) > -1)
                {
                    continue;
                }

                // 
                float tentativeGScore = thisNode.g + Distance(thisNode, neighbor);
                if (open.IndexOf(neighbor) == -1)
                {
                    // add neighbor as the next in line
                    open.Add(neighbor);
                    tentativeIsBetter = true;
                }
                else if (tentativeGScore < neighbor.g)
                {
                    tentativeIsBetter = true;
                }
                else
                {
                    tentativeIsBetter = false;
                }

                // if the tentative score of the given Neighbor node is 
                // the best one, update its information
                if (tentativeIsBetter)
                {
                    neighbor.cameFrom = thisNode;
                    neighbor.g = tentativeGScore;
                    neighbor.h = Distance(thisNode, end);
                    neighbor.f = neighbor.g + neighbor.h;
                }
            }

        }
        return false;
    }

    // build a path using the cameFrom field in each
    // Node object
    public void ReconstructPath(Node startID, Node endID)
    {
        pathList.Clear();
        pathList.Add(endID);
        var p = endID.cameFrom;
        // backtrack from endID to startID
        // using cameFrom field
        while (p != startID && p != null)
        {
            pathList.Insert(0, p);
            p = p.cameFrom;
        }

        pathList.Insert(0, startID);
    }

    // Hueristic for node comparison
    float Distance(Node a, Node b)
    {
        return Vector3.SqrMagnitude(a.getID().transform.position - b.getID().transform.position);
    }

    // return the index of the node with the lowest
    // value of f(n)
    int lowestF(List<Node> l)
    {
        float lowestF = 0;
        int count = 0;

        int interatorCount = 0;
        lowestF = l[0].f;

        for (int i = 1; i < l.Count; i++)
        {
            if (l[i].f <= lowestF)
            {
                lowestF = l[i].f;
                interatorCount = count;
            }
            count++;
        }

        return interatorCount;
    }


}
