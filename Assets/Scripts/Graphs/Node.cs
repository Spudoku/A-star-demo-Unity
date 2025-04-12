using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public List<Edge> edges = new List<Edge>();


    public Node path = null;

    GameObject id;

    public float f, g, h;   // f(n), g(n) and h(n) respectively
    public Node cameFrom;

    public Node(GameObject i)
    {
        id = i;
        path = null;

    }

    public GameObject getID()
    {
        return id;
    }



}
