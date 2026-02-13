using UnityEngine;

public class OnResourceMined: ISignal
{
    public Worker _worker;
    public Vector3Int _resourcePosition;
    public ResourceType _resourceType;
    public int _value;
    
    public OnResourceMined(Worker worker, Vector3Int resourcePosition, int value, ResourceType resourceType)
    {
        _worker = worker;
        _resourcePosition = resourcePosition;
        _value = value;
        _resourceType = resourceType;
    }
}