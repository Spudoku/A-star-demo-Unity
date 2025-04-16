using System.Collections.Generic;
using UnityEngine;

// This class stores a location (in the form of a GameObject), as well
// as factors to calcuate f(n).
public class Node
{
    public List<Edge> edges = new();


    public Node path = null;

    readonly GameObject id;

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
