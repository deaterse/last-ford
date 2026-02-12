using UnityEngine;

public struct ResourceNeighbour
{
    public ResourceType resourceType;
    public Vector3Int neighbourPos;
    public Vector3Int resourcePos;

    public static ResourceNeighbour None => new ResourceNeighbour(Vector3Int.zero, Vector3Int.zero);

    public ResourceNeighbour(Vector3Int neighbour, Vector3Int resource, ResourceType resourceT = ResourceType.None)
    {
        resourceType = resourceT;
        neighbourPos = neighbour;
        resourcePos = resource;
    }
}