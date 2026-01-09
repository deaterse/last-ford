using UnityEngine;

public class TerrainMap
{
    public TileData[,] TerrainData {get; private set;}
    public int Width {get; private set;}
    public int Height {get; private set;}

    public TerrainMap(int width, int height)
    {
        Width = width;
        Height = height;
        TerrainData = new TileData[width, height];
    }
}
