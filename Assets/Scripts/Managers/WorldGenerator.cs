using UnityEngine;

public class WorldGenerator : MonoBehaviour
{

    [Header("Configs")]
    [SerializeField] private MapConfig _mapConfig;
    [SerializeField] private NoiseConfig _noiseConfig;
    [SerializeField] private MeadowsConfig _meadowsConfig;
    [SerializeField] private ResourceSpawnConfig _resourceSpawnConfig;
    [SerializeField] private ResourcesSubtypeConfig _resourceSubtypeConfig;
    

    [Header("Renderer Components")]
    [SerializeField] private TerrainRenderer _terrainRenderer;
    [SerializeField] private ResourcesRenderer _resourcesRenderer;

    private HeightMap _heightMap;
    private TerrainMap _terrainMap;
    private MeadowMap _meadowMap;

    public void GenerateButton()
    {
        GenerateWorld();
    }

    public void GenerateWorld()
    {
        _terrainRenderer.CleanTerrainTilemap();
        _resourcesRenderer.CleanResourcesTilemap();

        int widthX = _mapConfig.MapSize.x;
        int heightY =  _mapConfig.MapSize.y;

        //Generate Heights
        HeightGenerator heightGenerator = new HeightGenerator(_noiseConfig);
        _heightMap = heightGenerator.GenerateHeightMap(widthX, heightY);

        Debug.Log("Terrain succesfully generated.");

        //Make a Terrain Map
        _terrainMap = new TerrainMap(widthX, heightY);

        //Generate Grass X x Y
        FillTerrain fillTerrain = new FillTerrain(_terrainMap);
        fillTerrain.GenerateTerrain();

        //Generate Forests
        ForestsGeneratorFill forestsGenerator = new ForestsGeneratorFill(_terrainMap, _resourceSpawnConfig.GetResourceSettings(ResourceType.Wood), _resourceSubtypeConfig);
        forestsGenerator.Generate();

        Debug.Log("Forests succesfully generated.");

        //Make a Meadow Map
        _meadowMap = new MeadowMap(widthX, heightY);

        //Generate Meadows
        MeadowGenerator meadowGenerator = new MeadowGenerator(_meadowMap, _terrainMap, _meadowsConfig);
        meadowGenerator.Generate();

        //Modify to apply resources impact to TerrainMap
        TerrainModifier terrainModifier = new TerrainModifier(_terrainMap, _resourceSpawnConfig);
        terrainModifier.Modify();

        //Visualize All
        _terrainRenderer.Visualize(_terrainMap);
        _terrainRenderer.VisualizeHeightMap(_heightMap);
        _resourcesRenderer.Visualize(_terrainMap, _resourceSubtypeConfig);

        OnTerrainMapGenerated signal = new OnTerrainMapGenerated(_terrainMap);
        ServiceLocator.GetService<EventBus>().Invoke<OnTerrainMapGenerated>(signal);
    }
}
