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
                    TerrainType.Grass,
                    Resource.None 
                );
            }
        }
    }

    public void SetTile(int x, int y, TileData tile)
    {
        TerrainData[x, y] = tile;
    }

    public void SetTerrainType(int x, int y, TerrainType type)
    {
        TileData tile = TerrainData[x, y];

        tile.Type = type;
        TerrainData[x, y] = tile;
    }

    public TerrainType GetTerrainType(int x, int y)
    {
        TileData tile = TerrainData[x, y];

        return tile.Type;
    }

    public ResourceType GetResourceType(int x, int y)
    {
        TileData tile = TerrainData[x, y];

        return tile.Resource.Type;
    }

    public void SetResource(int x, int y, Resource resource)
    {
        TileData tile = TerrainData[x, y];
        
        tile.Resource = resource;
        TerrainData[x, y] = tile;
    }

    public bool CanBuild(int x, int y)
    {
        if(x > Width || y > Height || x < 0 || y < 0) return false;
        return !TerrainData[x, y].HasResource && TerrainData[x, y].IsWalkable;
    }

    public bool IsGrass(int x, int y)
    {
        if(x > Width || y > Height || x < 0 || y < 0) return false;
        return TerrainData[x, y].Type == TerrainType.Grass;
    }

    public bool IsWater(int x, int y)
    {
        if(x > Width || y > Height || x < 0 || y < 0) return false;
        return TerrainData[x, y].Type == TerrainType.Water;
    }

    public bool HasResource(int x, int y)
    {
        if(x > Width || y > Height || x < 0 || y < 0) return false;
        return TerrainData[x, y].HasResource;
    }
}
