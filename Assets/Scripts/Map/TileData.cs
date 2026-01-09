using UnityEngine;

public struct TileData {
    public TerrainType Type {get;}
    public Resource Resource; // { ResourceType Type; int Amount; }

    public TileData(TerrainType type, Resource resource)
    {
        Type = type;
        Resource = resource;
    }
}
