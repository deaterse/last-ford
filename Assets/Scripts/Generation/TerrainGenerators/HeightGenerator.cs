using UnityEngine;
using UnityEngine.Tilemaps;

public class HeightGenerator
{
    private readonly NoiseConfig _noiseSettings;
    private readonly System.Random _random; // For Seed

    public HeightGenerator(NoiseConfig noiseSettings, System.Random random = null)
    {
        _noiseSettings = noiseSettings;
        _random = random ?? new System.Random();
    }

    public HeightMap GenerateHeightMap(int width, int height)
    {
        HeightMap heightMap = new HeightMap(width, height);

        float offSetX = Random.Range(0f, 100f);
        float offSetY = Random.Range(0f, 100f);

        for(int x = 0; x < width; x++)
        {
            for(int y = 0; y < height; y++)
            {
                heightMap.HeightData[x, y] = CalculatePerlinNoise(x, y, offSetX, offSetY);
            }
        }

        return heightMap;
    }

    private float CalculatePerlinNoise(int xPos, int yPos, float offSetX, float offSetY)
    {
        float currentPN = Mathf.PerlinNoise((xPos + offSetX) * _noiseSettings.Scale, (yPos + offSetY) * _noiseSettings.Scale);

        return currentPN;
    }
}