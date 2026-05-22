using System.Drawing;
using UnityEngine;

public class BuildingMap
{
    public Building[,] BuildingData { get; private set; }
    public int Width { get; private set; }
    public int Height { get; private set; }
    
    public BuildingMap(int width, int height)
    {
        Width = width;
        Height = height;
        BuildingData = new Building[width, height];
    }
    
    public bool CanPlaceBuilding(Vector2Int pos, Vector3Int[] sizeArray)
    {
        int rowIndex = 0;
        foreach(Vector3Int row in sizeArray)
        {
            for (int x = 0; x < 3; x++)
            {
                if(row[x] != 0)
                {
                    int checkX = pos.x + x;
                    int checkY = pos.y - rowIndex;

                    if (checkX >= Width || checkY >= Height || checkX < 0 || checkY < 0)
                        return false;
                        
                    if (BuildingData[checkX, checkY] != null)
                        return false;
                }
            }

            rowIndex++;
        }

        return true;
    }
    
    public void PlaceBuilding(Vector2Int pos, Vector3Int[] sizeArray, Building building)
    {
        int rowIndex = 0;
        foreach(Vector3Int row in sizeArray)
        {
            for (int x = 0; x < 3; x++)
            {
                if(row[x] != 0)
                {
                    BuildingData[pos.x + x, pos.y - rowIndex] = building;
                }
            }

            rowIndex++;
        }
    }

    public void RemoveBuilding(Building building)
    {
        Vector2Int pos = building.GridPosition;
        Vector3Int[] sizeArray = building.buildingData.BuildingSize;

        int rowIndex = 0;
        foreach(Vector3Int row in sizeArray)
        {
            for (int x = 0; x < 3; x++)
            {
                if(row[x] != 0)
                {
                    BuildingData[pos.x + x, pos.y - rowIndex] = null;
                }
            }

            rowIndex++;
        }
    }
}
