using UnityEngine;

public class OnResourceMined: ISignal
{
    public Vector3Int _resourcePosition;
    public int _value;
    
    public OnResourceMined(Vector3Int resourcePosition, int value)
    {
        _resourcePosition = resourcePosition;
        _value = value;
    }
}