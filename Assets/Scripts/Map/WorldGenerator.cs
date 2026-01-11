using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class WorldGenerator : MonoBehaviour
{
    [Header("Configs")]
    [SerializeField] private MapConfig _mapConfig;
    [SerializeField] private NoiseConfig _noiseConfig;
    

    [Header("Renderer Components")]
    [SerializeField] private TerrainRenderer _terrainRenderer;
    [SerializeField] private ResourcesRenderer _resourcesRenderer;

    private HeightMap _heightMap;
    private TerrainMap _terrainMap;

    private void Start()
    {
        GenerateWorld();
    }

    private void GenerateWorld()
    {
        //Generate Grass X x Y
        _terrainRenderer.GenerateTerrain(_mapConfig.MapSize);

        //Generate Heights
        HeightGenerator heightGenerator = new HeightGenerator(_noiseConfig);
        _heightMap = heightGenerator.GenerateHeightMap(_mapConfig.MapSize.x, _mapConfig.MapSize.y);

        _terrainRenderer.VisualizeHeightMap(_heightMap);

        Debug.Log("Terrain succesfully generated.");

        //Make a Terrain Map
        _terrainMap = new TerrainMap(_mapConfig.MapSize.x, _mapConfig.MapSize.y);

        //Generate River
        RiverGenerator riverGenerator = new RiverGenerator();
        riverGenerator.GenerateRivers(_terrainMap);

        Debug.Log("River succesfully generated.");

        //Generate Shore
        ShoreGenerator shoreGenerator = new ShoreGenerator();
        shoreGenerator.GenerateShore(_terrainMap);

        Debug.Log("Shore succesfully generated.");

        //Visualize Terrain
        _terrainRenderer.Visualize(_terrainMap);

        //Generate Stones
        StonesGenerator stonesGenerator = new StonesGenerator(_terrainMap);
        stonesGenerator.Generate();

        Debug.Log("Stones succesfully generated.");

        //Generate Forests
        ForestsGenerator forestsGenerator = new ForestsGenerator(_terrainMap);
        forestsGenerator.Generate();

        Debug.Log("Forests succesfully generated.");

        //Visualize Resources
        _resourcesRenderer.Visualize(_terrainMap);
    }
}
