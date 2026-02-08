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
        if(x >= Width || y >= Height || x < 0 || y < 0)  return;

        TerrainData[x, y] = tile;
    }

    public void SetTerrainType(int x, int y, TerrainType type)
    {
        if(x >= Width || y >= Height || x < 0 || y < 0)  return;

        TileData tile = TerrainData[x, y];

        tile.Type = type;
        TerrainData[x, y] = tile;
    }

    public TerrainType GetTerrainType(int x, int y)
    {
        if(x >= Width || y >= Height || x < 0 || y < 0)  return TerrainType.None;

        TileData tile = TerrainData[x, y];

        return tile.Type;
    }

    public ResourceType GetResourceType(int x, int y)
    {
        if(x >= Width || y >= Height || x < 0 || y < 0)  return ResourceType.None;

        TileData tile = TerrainData[x, y];

        return tile.Resource.Type;
    }

    public void SetResource(int x, int y, Resource resource)
    {
        TileData tile = TerrainData[x, y];
        
        tile.Resource = resource;
        TerrainData[x, y] = tile;
    }

    public bool CanBuild(Vector2Int pos, Vector2Int size)
    {
        if(pos.x + size.x > Width || pos.y + size.y > Height || pos.x < 0 || pos.y < 0) return false;

        for(int x = 0; x < size.x; x++)
        {
            for(int y = 0; y < size.y; y++)
            {
                if(!IsWalkable(pos.x + x, pos.y + y))
                {
                    return false;
                }
            }
        }

        return true;
    }

    public bool IsWalkable(int x, int y)
    {
        if(x >= Width || y >= Height || x < 0 || y < 0)  return false;

        if(TerrainData[x, y].HasResource || !TerrainData[x, y].IsWalkable)
        {
            return false;
        }

        return true;
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

    public bool InBorders(int x, int y)
    {
        if(x < Width && y < Width && y >= 0 && x >= 0)
        {
            return true;
        }
        
        return false;
    }

    public bool AvaliableForCastle(int x, int y)
    {
        if(InBorders(x, y) && !IsWater(x, y))
        {
            return true;
        }

        return false;
    }
}
