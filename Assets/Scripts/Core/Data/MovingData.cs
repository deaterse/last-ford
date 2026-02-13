using UnityEngine;

public class MovingData
{
    public Vector3Int Target { get; }
    public System.Action OnReached { get; }
    
    public MovingData(Vector3Int target, System.Action onReached)
    {
        Target = target;
        OnReached = onReached;
    }
}