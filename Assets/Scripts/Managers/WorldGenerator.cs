using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.Events;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.SceneManagement;

public class WorldGenerator : MonoBehaviour
{
    [SerializeField] private DebugUI _debugUI;

    [Header("Configs")]
    [SerializeField] private MapConfig _mapConfig;
    [SerializeField] private NoiseConfig _noiseConfig;
    [SerializeField] private ResourceSpawnConfig _resourceSpawnConfig;
    

    [Header("Renderer Components")]
    [SerializeField] private TerrainRenderer _terrainRenderer;
    [SerializeField] private ResourcesRenderer _resourcesRenderer;

    private HeightMap _heightMap;
    private TerrainMap _terrainMap;

    private void Start()
    {
        StartGeneration();
    }

    public void GenerateButton()
    {
        StartGeneration();
    }

    private void StartGeneration()
    {
        #if UNITY_EDITOR || DEVELOPMENT_BUILD
            InitDebugUI();
        #endif

        GenerateWorld();
    }

    private void GenerateWorld()
    {
        _terrainRenderer.CleanTerrainTilemap();
        _resourcesRenderer.CleanResourcesTilemap();

        //Generate Grass X x Y
        _terrainRenderer.GenerateTerrain(_mapConfig.MapSize);

        int widthX = _mapConfig.MapSize.x;
        int heightY =  _mapConfig.MapSize.y;

        //Generate Heights
        HeightGenerator heightGenerator = new HeightGenerator(_noiseConfig);
        _heightMap = heightGenerator.GenerateHeightMap(widthX, heightY);

        Debug.Log("Terrain succesfully generated.");

        //Make a Terrain Map
        _terrainMap = new TerrainMap(widthX, heightY);

        //Generate River
        RiverGenerator riverGenerator = new RiverGenerator();
        riverGenerator.GenerateRivers(_terrainMap);

        Debug.Log("River succesfully generated.");

        //Generate Shore
        ShoreGenerator shoreGenerator = new ShoreGenerator();
        shoreGenerator.GenerateShore(_terrainMap);

        Debug.Log("Shore succesfully generated.");

        //Generate Stones
        StonesGenerator stonesGenerator = new StonesGenerator(_terrainMap, _resourceSpawnConfig.GetResourceSettings(ResourceType.Stone));
        stonesGenerator.Generate();

        Debug.Log("Stones succesfully generated.");

        //Generate Forests
        ForestsGenerator forestsGenerator = new ForestsGenerator(_terrainMap, _resourceSpawnConfig.GetResourceSettings(ResourceType.Wood));
        forestsGenerator.Generate();

        Debug.Log("Forests succesfully generated.");

        //Modify to apply resources impact to TerrainMap
        TerrainModifier terrainModifier = new TerrainModifier(_terrainMap, _resourceSpawnConfig);
        terrainModifier.Modify();

        //Visualize All
        _terrainRenderer.Visualize(_terrainMap);
        _terrainRenderer.VisualizeHeightMap(_heightMap);
        _resourcesRenderer.Visualize(_terrainMap);

        GameEvents.InvokeOnTerrainMapGenerated(_terrainMap);
    }

    private void InitDebugUI()
    {
        if(_debugUI != null)
        {
            _debugUI.Init();
        }
        else
        {
            Debug.LogWarning("DebugUI is null!");
        }
    }
}
