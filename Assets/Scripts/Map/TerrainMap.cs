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
    
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                TerrainData[x, y] = new TileData 
                (
                    TerrainType.Grass,  // ← должно быть Grass по умолчанию!
                    Resource.None 
                );
            }
        }
    }


}
