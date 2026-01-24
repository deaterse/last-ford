using UnityEngine;

public class OnStonesGenerated: ISignal
{
    public int _value;
    public OnStonesGenerated(int value) => _value = value;
}