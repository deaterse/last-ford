using UnityEngine;

public class BuildingMap
{
    public BuildingData[,] BuildingData { get; private set; }
    public int Width { get; private set; }
    public int Height { get; private set; }
    
    public BuildingMap(int width, int height)
    {
        Width = width;
        Height = height;
        BuildingData = new BuildingData[width, height];
    }
    
    public bool CanPlaceBuilding(Vector2Int pos, Vector2Int size)
    {
        for (int x = 0; x < size.x; x++)
        {
            for (int y = 0; y < size.y; y++)
            {
                int checkX = pos.x + x;
                int checkY = pos.y + y;
                
                if (checkX >= Width || checkY >= Height || checkX < 0 || checkY < 0)
                    return false;
                    
                if (BuildingData[checkX, checkY] != null)
                    return false;
            }
        }
        return true;
    }
    
    public void PlaceBuilding(Vector2Int pos, Vector2Int size, BuildingData data)
    {
        for (int x = 0; x < size.x; x++)
        {
            for (int y = 0; y < size.y; y++)
            {
                BuildingData[pos.x + x, pos.y + y] = data;
            }
        }
    }
}
