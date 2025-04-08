using System.Collections.Generic;
using System.Xml;
using Unity.VisualScripting;
using UnityEngine;

public class Graph
{
    List<Edge> edges = new List<Edge>();
    List<Node> nodes = new List<Node>();

    List<Node> pathList = new List<Node>();

    public Graph() { }

    public void AddNode(GameObject id)
    {
        Node node = new Node(id);
        nodes.Add(node);
    }

    public void AddEdge(GameObject fromNode, GameObject toNode)
    {
        Node from = FindNode(fromNode);
        Node to = FindNode(toNode);

        if (from != null && to != null)
        {
            Edge edge = new Edge(from, to);
            edges.Add(edge);
            from.edges.Add(edge);
        }

    }

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

    public bool AStar(GameObject startID, GameObject endID)
    {
        Node start = FindNode(startID);
        Node end = FindNode(endID);

        if (start == null || end == null) return false;

        List<Node> open = new List<Node>();
        List<Node> closed = new List<Node>();

        float tentativeGScore = 0;

        bool tentativeIsBetter;

        start.g = 0;
        start.h = Distance(start, end);
        start.f = start.h;

        open.Add(start);

        while (open.Count > 0)
        {
            int i = lowestF(open);
            Node thisNode = open[i];
            if (thisNode.getID() == endID)
            {
                // reconstruct path
                ReconstructPath(start, end);
                return true;
            }

            open.RemoveAt(i);
            closed.Add(thisNode);

            Node neighbor;

            foreach (Edge e in thisNode.edges)
            {
                neighbor = e.endNode;

                if (closed.IndexOf(neighbor) > -1)
                {
                    continue;
                }

                tentativeGScore = thisNode.g + Distance(thisNode, neighbor);
                if (open.IndexOf(neighbor) == -1)
                {
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

    public void ReconstructPath(Node startID, Node endID)
    {
        pathList.Clear();
        pathList.Add(endID);
        var p = endID.cameFrom;
        while (p != startID && p != null)
        {
            pathList.Insert(0, p);
            p = p.cameFrom;
        }

        pathList.Insert(0, startID);
    }

    // Hueristic for node comparison (?)
    float Distance(Node a, Node b)
    {
        return Vector3.SqrMagnitude(a.getID().transform.position - b.getID().transform.position);
    }

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
