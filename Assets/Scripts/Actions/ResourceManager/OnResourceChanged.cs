using UnityEngine;

public class OnResourceChanged: ISignal
{
    public ResourceType _resourceType;
    public int _value;
    
    public OnResourceChanged(ResourceType resourceType, int value)
    {
        _resourceType = resourceType;
        _value = value;
    }
}