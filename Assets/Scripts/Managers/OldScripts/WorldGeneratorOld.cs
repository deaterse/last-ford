using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.Events;
using System.Collections.Generic;
using System.Collections;

public class WorldGeneratorOld : MonoBehaviour
{

    // [Header("Configs")]
    // [SerializeField] private MapConfig _mapConfig;
    // [SerializeField] private NoiseConfig _noiseConfig;
    // [SerializeField] private ResourceSpawnConfig _resourceSpawnConfig;
    // [SerializeField] private ResourcesSubtypeConfig _resourceSubtypeConfig;
    

    // [Header("Renderer Components")]
    // [SerializeField] private TerrainRenderer _terrainRenderer;
    // [SerializeField] private ResourcesRenderer _resourcesRenderer;

    // private HeightMap _heightMap;
    // private TerrainMap _terrainMap;

    // public void GenerateButton()
    // {
    //     GenerateWorld();
    // }

    // public void GenerateWorld()
    // {
    //     _terrainRenderer.CleanTerrainTilemap();
    //     _resourcesRenderer.CleanResourcesTilemap();

    //     int widthX = _mapConfig.MapSize.x;
    //     int heightY =  _mapConfig.MapSize.y;

    //     //Generate Heights
    //     HeightGenerator heightGenerator = new HeightGenerator(_noiseConfig);
    //     _heightMap = heightGenerator.GenerateHeightMap(widthX, heightY);

    //     Debug.Log("Terrain succesfully generated.");

    //     //Make a Terrain Map
    //     _terrainMap = new TerrainMap(widthX, heightY);

    //     //Generate Grass X x Y
    //     FillTerrain fillTerrain = new FillTerrain(_terrainMap);
    //     fillTerrain.GenerateTerrain();

    //     //Generate River
    //     RiverGenerator riverGenerator = new RiverGenerator(_terrainMap);
    //     riverGenerator.GenerateRivers();

    //     Debug.Log("River succesfully generated.");

    //     //Generate Shore
    //     ShoreGenerator shoreGenerator = new ShoreGenerator(_terrainMap);
    //     shoreGenerator.GenerateShore();

    //     Debug.Log("Shore succesfully generated.");

    //     //Generate "pre-shore"
    //     PreSandGenerator preSandGenerator = new PreSandGenerator(_terrainMap);
    //     preSandGenerator.GeneratePreSand();

    //     //Generate Stones
    //     StonesGenerator stonesGenerator = new StonesGenerator(_terrainMap, _resourceSpawnConfig.GetResourceSettings(ResourceType.Stone));
    //     stonesGenerator.Generate();

    //     Debug.Log("Stones succesfully generated.");

    //     //Generate Forests
    //     ForestsGenerator forestsGenerator = new ForestsGenerator(_terrainMap, _resourceSpawnConfig.GetResourceSettings(ResourceType.Wood), _resourceSubtypeConfig);
    //     forestsGenerator.Generate();

    //     Debug.Log("Forests succesfully generated.");

    //     //Generate FertilityMap
    //     FertilityMapGenerator fertilityMapGenerator = new FertilityMapGenerator(_terrainMap, _noiseConfig);
    //     FertilityMap _fertilityMap = fertilityMapGenerator.GenerateFertilityMap(widthX, heightY);

    //     //Modify to apply resources impact to TerrainMap
    //     TerrainModifier terrainModifier = new TerrainModifier(_terrainMap, _resourceSpawnConfig);
    //     terrainModifier.Modify();

    //     //Visualize All
    //     _terrainRenderer.Visualize(_terrainMap);
    //     _terrainRenderer.VisualizeHeightMap(_heightMap);
    //     _terrainRenderer.VisualizeFertilityMap(_fertilityMap);
    //     _resourcesRenderer.Visualize(_terrainMap, _resourceSubtypeConfig);

    //     OnTerrainMapGenerated signal = new OnTerrainMapGenerated(_terrainMap);
    //     ServiceLocator.GetService<EventBus>().Invoke<OnTerrainMapGenerated>(signal);
    // }
}
