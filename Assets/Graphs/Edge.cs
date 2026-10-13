using UnityEngine;
using System.Collection.Generic;
using System.Collection;

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

   