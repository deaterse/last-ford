using UnityEngine;

public class ResourceMap
{
    public float[,] ResourceData {get; private set;}
    public int Width {get; private set;}
    public int Height {get; private set;}

    public ResourceMap(int width, int height)
    {
        Width = width;
        Height = height;
        ResourceData = new float[Width, Height];
    }
}
