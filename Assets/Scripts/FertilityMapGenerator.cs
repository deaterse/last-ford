using UnityEngine;

public class FertilityMapGenerator
{
    private TerrainMap _terrainMap;
    private NoiseConfig _noiseSettings;

    public FertilityMapGenerator(TerrainMap terrainMap, NoiseConfig noiseSettings)
    {
        _terrainMap = terrainMap;
        _noiseSettings = noiseSettings;
    }
    public FertilityMap GenerateFertilityMap(int width, int height)
    {
        FertilityMap fertilityMap = new FertilityMap(width, height);

        float offSetX = Random.Range(0f, 100f);
        float offSetY = Random.Range(0f, 100f);

        for(int x = 0; x < width; x++)
        {
            for(int y = 0; y < height; y++)
            {
                if(_terrainMap.GetTerrainType(x, y) == TerrainType.Grass && _terrainMap.GetResourceType(x, y) == ResourceType.None)
                {
                    fertilityMap.FertilityData[x, y] = CalculatePerlinNoise(x, y, offSetX, offSetY);
                }
            }
        }

        return fertilityMap;
    }

    private float CalculatePerlinNoise(int xPos, int yPos, float offSetX, float offSetY)
    {
        float currentPN = Mathf.PerlinNoise((xPos + offSetX) * _noiseSettings.Scale, (yPos + offSetY) * _noiseSettings.Scale);

        return currentPN;
    }
}
