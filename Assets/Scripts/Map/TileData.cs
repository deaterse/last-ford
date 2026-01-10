using UnityEngine;

public struct TileData {
    public TerrainType Type;
    public Resource Resource; // { ResourceType Type; int Amount; }

    public TileData(TerrainType type, Resource resource)
    {
        Type = type;
        Resource = resource;
    }

    public bool HasResource => Resource.Type != ResourceType.None;
    public bool IsWalkable => Type != TerrainType.Water && Type != TerrainType.Mountain;
}
