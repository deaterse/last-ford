public class FertilityMap
{
    public float[,] FertilityData {get; private set;}
    public int Width {get; private set;}
    public int Height {get; private set;}

    public FertilityMap(int width, int height)
    {
        Width = width;
        Height = height;
        FertilityData = new float[width, height];
    }
}
