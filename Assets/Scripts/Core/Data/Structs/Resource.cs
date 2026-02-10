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

    public void DecreaseResource(int amount)
    {
        if(Amount > 0)
        {
            Amount -= amount;

            Debug.Log($"Current resource amount: {Amount}");
            return;
        }

        Debug.Log($"Current resource amount ZERO");
    }
}
