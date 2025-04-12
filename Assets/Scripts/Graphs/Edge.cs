// Represents a connection between
// two Node objects
public class Edge
{
    public Node startNode;
    public Node endNode;


    public Edge(Node from, Node to)
    {
        startNode = from;
        endNode = to;
    }


}
