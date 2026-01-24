using UnityEngine;

public class OnInputCameraZoom: ISignal
{
    public float _value;
    public OnInputCameraZoom(float value) => _value = value;
}