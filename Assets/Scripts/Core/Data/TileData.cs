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
    public bool IsWalkable => Type != TerrainType.Water;

    public bool TryDecreaseResource(int amount)
    {
        if(Resource.Amount > amount && HasResource)
        {
            Resource.DecreaseResource(amount);
            return true;
        }

        Resource.DecreaseResource(amount);

        return false;
    }
}
