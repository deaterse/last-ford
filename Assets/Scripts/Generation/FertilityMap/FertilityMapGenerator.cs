using UnityEngine;
using System.Collections.Generic;

public class FertilityMapGenerator
{
    private TerrainMap _terrainMap;
    private NoiseConfig _noiseSettings;

    //[0, 1, 2, 3]
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
                if(_terrainMap.GetTerrainType(x, y) == TerrainType.Grass)
                {
                    fertilityMap.FertilityData[x, y] = 1;
                }
            }
        }

        int widthT = _terrainMap.Width;
        int heightT = _terrainMap.Height;

        int waterRadius = 5;

        for(int x = 0; x < widthT; x++)
        {
            for(int y = 0; y < heightT; y++)
            {
                // List<Vector2Int> allNeighbours = GetNeighbours(x, y);

                // foreach(Vector2Int neighbour in allNeighbours)
                // {
                //     if(neighbour.x >= 0 && neighbour.x < widthT && neighbour.y >= 0 && neighbour.y < heightT)
                //     {
                //         if(_terrainMap.GetTerrainType(neighbour.x, neighbour.y) == TerrainType.Sand)
                //         {
                //             fertilityMap.SetFertility(neighbour.x, neighbour.y, 3);
                //         }
                //         else if(_terrainMap.GetTerrainType(neighbour.x, neighbour.y) == TerrainType.Grass_Sand)
                //         {
                //             fertilityMap.SetFertility(neighbour.x, neighbour.y, 2);
                //         }
                //     }
                // }
                float waterInfluence = CalculateWaterInfluence(x, y, waterRadius);
                fertilityMap.SetFertility(x, y, waterInfluence);
            }
        }

        return fertilityMap;
    }

    private float CalculatePerlinNoise(int xPos, int yPos, float offSetX, float offSetY)
    {
        float currentPN = Mathf.PerlinNoise((xPos + offSetX) * _noiseSettings.Scale, (yPos + offSetY) * _noiseSettings.Scale);

        return currentPN;
    }

    private List<Vector2Int> GetNeighbours(int x, int y)
    {
        List<Vector2Int> allNeighbours = new List<Vector2Int>();

        if(x >= 0 && y >= 0 && x < _terrainMap.Width && y < _terrainMap.Height)
        {
            TerrainType currentType = _terrainMap.GetTerrainType(x, y);

            if(currentType != TerrainType.Water)
            {
                Vector2Int n1 = new Vector2Int(x + 1, y);
                Vector2Int n2 = new Vector2Int(x + 1, y + 1);
                Vector2Int n3 = new Vector2Int(x - 1, y - 1);
                Vector2Int n4 = new Vector2Int(x + 1, y - 1);
                Vector2Int n5 = new Vector2Int(x - 1, y + 1);
                Vector2Int n6 = new Vector2Int(x, y + 1);
                Vector2Int n7 = new Vector2Int(x, y - 1);
                Vector2Int n8 = new Vector2Int(x - 1, y);

                allNeighbours.Add(n1);
                allNeighbours.Add(n2);
                allNeighbours.Add(n3);
                allNeighbours.Add(n4);
                allNeighbours.Add(n5);
                allNeighbours.Add(n6);
                allNeighbours.Add(n7);
                allNeighbours.Add(n8);
            }
        }

        return allNeighbours;
    }

    private float CalculateWaterInfluence(int centerX, int centerY, int maxDistance)
    {
        float maxInfluence = 0f;
        float falloffSharpness = 0.4f;

        for (int dx = -maxDistance; dx <= maxDistance; dx++)
        {
            for (int dy = -maxDistance; dy <= maxDistance; dy++)
            {
                int checkX = centerX + dx;
                int checkY = centerY + dy;
                
                if (checkX >= 0 && checkX < _terrainMap.Width && 
                    checkY >= 0 && checkY < _terrainMap.Height && 
                    _terrainMap.IsWater(checkX, checkY))
                {
                    float distance = Mathf.Sqrt(dx * dx + dy * dy);
                    float normalizedDistance = Mathf.Clamp01(distance / maxDistance);
                    
                    float influence = Mathf.Exp(-falloffSharpness * normalizedDistance * 5f);
                    
                    maxInfluence = Mathf.Max(maxInfluence, influence);
                }
            }
        }
        
        return maxInfluence;
    }

}
