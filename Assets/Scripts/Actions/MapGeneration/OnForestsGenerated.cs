using UnityEngine;

public class OnForestsGenerated: ISignal
{
    public int _value;
    public OnForestsGenerated(int value) => _value = value;
}