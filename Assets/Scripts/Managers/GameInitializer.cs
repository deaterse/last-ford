using UnityEngine;
using UnityEngine.Tilemaps;

public class GameInitializer : MonoBehaviour
{
    [SerializeField] private Tilemap _terrainTilemap;

    [Header("Configs")]
    [SerializeField] private StartResourcesConfig _startResourcesConfig;
    [SerializeField] private MapGenerateConfig _mapGenerateConfig;
    [SerializeField] private ResourcesSubtypeConfig _resourcesSubtypeConfig;

    [Header("UI Managers")]
    [SerializeField] private DebugUI _debugUI;
    [SerializeField] private ResourceUI _resourceUI;
    [SerializeField] private BuildingSystemUI _buildingUI;


    [Header("Managers")]
    [SerializeField] private InputListener _inputListener;
    [SerializeField] private BuildSystem _buildSystem;
    [SerializeField] private BuildingManager _buildingManager;
    [SerializeField] private JobManager _jobManager;
    [SerializeField] private WorldGenerator _worldGenerator;
    [SerializeField] private WorldGeneratorOld _worldGeneratorOld;
    [SerializeField] private Visualizer _visualizer;
    [SerializeField] private DayCycle _dayCycle;
    [SerializeField] private TerrainMapManager _terrainMapManager;
    [SerializeField] private WindowLightManager _windowLightManager;

    [SerializeField] private bool oldGeneration;

    private TerrainMap _terrainMap;
    private FertilityMap _fertilityMap;
    private HeightMap _heightMap;

    private void Awake()
    {
        InitEventBus();
    
        ServiceLocator.GetService<EventBus>().Subscribe<OnTerrainMapGenerated>(GetTerrainMap);
        
        _inputListener.Init();

        InitDebugUI();
        _resourceUI.Init();

        _buildSystem.Init();
        _buildingManager.Init();

        InitResourceManager();
        _jobManager.Init();

        _buildingUI.Init(_buildSystem);
        GenerateWorld();

        InitResourceLocator();
        InitMapManager();
        InitPathfinder();

        Visualize();

        _windowLightManager.Init();
        InitDayCycle();
    }

    private void GetTerrainMap(OnTerrainMapGenerated signal)
    {
        _terrainMap = signal._terrainMap;
    }

    private void InitResourceManager()
    {
        ResourceManager _resourceManager = new ResourceManager(_startResourcesConfig);
        ServiceLocator.ProvideService<ResourceManager>(_resourceManager);
    }

    private void InitDebugUI()
    {
        #if UNITY_EDITOR || DEVELOPMENT_BUILD
            _debugUI.Init();
        #endif
    }

    private void InitResourceLocator()
    {
        ResourceLocator resourceLocator = new ResourceLocator(_terrainMap);
        ServiceLocator.ProvideService<ResourceLocator>(resourceLocator);
    }

    private void InitMapManager()
    {
        _terrainMapManager.Init(_terrainMap);
        ServiceLocator.ProvideService<TerrainMapManager>(_terrainMapManager);
    }

    private void GenerateWorld()
    {
        if(oldGeneration)
        {
            _worldGeneratorOld.GenerateWorld();
        }
        else
        {
            _worldGenerator.GenerateWorld();
        }
    }

    private void Visualize()
    {
        _visualizer.VisualizeEverything(_terrainMap, _mapGenerateConfig, _heightMap, _fertilityMap, _resourcesSubtypeConfig);
    }

    private void InitDayCycle()
    {
        ServiceLocator.ProvideService<DayCycle>(_dayCycle);
        _dayCycle.Init();
    }

    private void InitPathfinder()
    {
        Pathfinder _pathFinder = new Pathfinder(_terrainMap, _terrainTilemap);
        ServiceLocator.ProvideService<Pathfinder>(_pathFinder);
    }

    private void InitEventBus()
    {
        EventBus _eventBus = new EventBus();
        ServiceLocator.ProvideService<EventBus>(_eventBus);
    }

}
