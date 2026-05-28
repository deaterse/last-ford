using UnityEngine;

public class OnHeightMapGenerated : ISignal
{
    public HeightMap _heightMap;
    public OnHeightMapGenerated(HeightMap heightMap) => _heightMap = heightMap;
}
