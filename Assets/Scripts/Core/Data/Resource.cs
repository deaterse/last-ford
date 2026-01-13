using UnityEngine;

public struct Resource
{
    public ResourceType Type;
    public int Amount;
    public int SubType;

    public static Resource None => new Resource(ResourceType.None, 0);

    public Resource(ResourceType type, int amount, int subType = 0)
    {
        Type = type;
        Amount = amount;
        SubType = subType;
    }
}
