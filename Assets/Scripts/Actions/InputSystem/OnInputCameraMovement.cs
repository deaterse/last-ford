using UnityEngine;

public class OnInputCameraMovement: ISignal
{
    public Vector2 _vectorMove;
    public OnInputCameraMovement(Vector2 vectorMove) => _vectorMove = vectorMove;
}