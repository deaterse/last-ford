using UnityEngine;

public struct ResourceNeighbour
{
    public Vector3Int neighbourPos;
    public Vector3Int resourcePos;

    public ResourceNeighbour(Vector3Int neighbour, Vector3Int resource)
    {
        neighbourPos = neighbour;
        resourcePos = resource;
    }
}