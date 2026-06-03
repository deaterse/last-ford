public class HeightMap
{
    public float[,] HeightData {get; private set;}
    public int Width {get; private set;}
    public int Height {get; private set;}

    public HeightMap(int width, int height)
    {
        Width = width;
        Height = height;
        HeightData = new float[width, height];
    }
}
