public class MeadowMap
{
    private bool[,] _meadowsData;

    public int Width {get; private set;}
    public int Height {get; private set;}

    public MeadowMap(int width, int height)
    {
        Width = width;
        Height = height;

        _meadowsData = new bool[width, height];
    }

    public bool IsTaken(int x, int y)
    {
        if(x >= Width || y >= Height || x < 0 || y < 0) return true;
        
        return _meadowsData[x, y];
    }

    public void SetCell(int x, int y)
    {
        _meadowsData[x, y] = true;
    }
}
