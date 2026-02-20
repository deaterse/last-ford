using Unity.Collections;
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

    public TileData GetTileData(int x, int y)
    {
        if(x >= Width || y >= Height || x < 0 || y < 0)  return new TileData(TerrainType.None, Resource.None);

        return TerrainData[x, y];
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
        if(x >= Width || y >= Height || x < 0 || y < 0)  return;

        TileData tile = TerrainData[x, y];
        
        tile.Resource = resource;
        TerrainData[x, y] = tile;
    }

    public bool CanBuild(Vector2Int startPos, Vector3Int[] sizeArray)
    {
        if(startPos.x > Width || startPos.y > Height || startPos.x < -1 || startPos.y < -1) return false;

        int rowIndex = 0;
        foreach(Vector3Int row in sizeArray)
        {
            for (int x = 0; x < 3; x++)
            {
                if(row[x] != 0)
                {
                    int checkX = startPos.x + x;
                    int checkY = startPos.y - rowIndex;

                    if(!IsWalkable(checkX, checkY))
                    {
                        return false;
                    }
                }
            }

            rowIndex++;
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
