using UnityEngine;
using UnityEngine.Tilemaps;

public class GameInitializer : MonoBehaviour
{
    [SerializeField] private Tilemap _terrainTilemap;

    [Header("Configs")]
    [SerializeField] private StartResourcesConfig _startResourcesConfig;

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
    [SerializeField] private DayCycle _dayCycle;

    [SerializeField] private bool oldGeneration;

    private void Awake()
    {
        InitEventBus();
    
        ServiceLocator.GetService<EventBus>().Subscribe<OnTerrainMapGenerated>(InitPathfinder);
        ServiceLocator.GetService<EventBus>().Subscribe<OnTerrainMapGenerated>(InitResourceLocator);
        
        _inputListener.Init();

        InitDebugUI();
        _resourceUI.Init();

        _buildSystem.Init();
        _buildingManager.Init();

        InitResourceManager();
        _jobManager.Init();

        _buildingUI.Init(_buildSystem);
        GenerateWorld();

        InitDayCycle();
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

    private void InitResourceLocator(OnTerrainMapGenerated signal)
    {
        ResourceLocator resourceLocator = new ResourceLocator(signal._terrainMap);
        ServiceLocator.ProvideService<ResourceLocator>(resourceLocator);
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

    private void InitDayCycle()
    {
        ServiceLocator.ProvideService<DayCycle>(_dayCycle);
        _dayCycle.Init();
    }

    private void InitPathfinder(OnTerrainMapGenerated signal)
    {
        Pathfinder _pathFinder = new Pathfinder(signal._terrainMap, _terrainTilemap);
        ServiceLocator.ProvideService<Pathfinder>(_pathFinder);
    }

    private void InitEventBus()
    {
        EventBus _eventBus = new EventBus();
        ServiceLocator.ProvideService<EventBus>(_eventBus);
    }

}
