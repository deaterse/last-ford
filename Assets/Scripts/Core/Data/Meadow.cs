using UnityEngine;

public struct Meadow
{
    public MeadowType Type;
    public Vector2Int[] Positions;

    public Meadow(MeadowType type, Vector2Int[] positions)
    {
        Type = type;
        Positions = positions;
    }
}
