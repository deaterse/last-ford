using UnityEngine;

public class CropMap
{
    public CropData[,] CropData {get; private set;}

    public int Width {get; private set;}
    public int Height {get; private set;}

    public CropMap(int width, int height)
    {
        Width = width;
        Height = height;
        CropData = new CropData[width, height];
    }

    public CropData GetTileData(int x, int y)
    {
        if(x >= Width || y >= Height || x < 0 || y < 0)  return new CropData(new Crop(CropType.None));

        return CropData[x, y];
    }

    public void SetTile(int x, int y, CropData tile)
    {
        if(x >= Width || y >= Height || x < 0 || y < 0)  return;

        CropData[x, y] = tile;
    }

    // public bool IsEmpty(int x, int y)
    // {
    //     if(x >= Width || y >= Height || x < 0 || y < 0)  return false;

    //     // if(CropData[x, y] != null)
    //     // {
    //     //     return false;
    //     // }
    //     // else
    //     // {
    //     //     return true;
    //     // }
    // }
}
