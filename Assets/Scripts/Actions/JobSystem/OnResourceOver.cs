using UnityEngine;

public class OnResourceOver: ISignal
{
    public Vector3Int _resourcePosition;
    public int _value;
    
    public OnResourceOver(Vector3Int resourcePosition, int value)
    {
        _resourcePosition = resourcePosition;
        _value = value;
    }
}