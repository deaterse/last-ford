using UnityEngine;

public struct Resource
{
    public ResourceType Type;
    public int Amount;

    public static Resource None => new Resource(ResourceType.None, 0);

    public Resource(ResourceType type, int amount)
    {
        Type = type;
        Amount = amount;
    }
}
