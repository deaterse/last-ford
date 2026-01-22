using UnityEngine;

public class GameInitializer : MonoBehaviour
{
    [Header("Configs")]
    [SerializeField] private StartResourcesConfig _startResourcesConfig;

    [Header("UI Managers")]
    [SerializeField] private DebugUI _debugUI;
    [SerializeField] private ResourceUI _resourceUI;
    [SerializeField] private BuildingSystemUI _buildingUI;


    [Header("Managers")]
    [SerializeField] private SceneCleaner _sceneCleaner;
    [SerializeField] private InputListener _inputListener;
    [SerializeField] private BuildSystem _buildSystem;
    [SerializeField] private BuildingManager _buildingManager;
    [SerializeField] private JobManager _jobManager;
    [SerializeField] private WorldGeneratorNew _worldGenerator;

    private void Awake()
    {
        _sceneCleaner.Init();
        _inputListener.Init();

        #if UNITY_EDITOR || DEVELOPMENT_BUILD
            _debugUI.Init();
        #endif
        _resourceUI.Init();

        _buildSystem.Init();
        _buildingManager.Init();

        InitResourceManager();
        _jobManager.Init();

        _buildingUI.Init(_buildSystem);

        _worldGenerator.GenerateWorld();
    }

    private void InitResourceManager()
    {
        ResourceManager _resourceManager = new ResourceManager(_startResourcesConfig);
        ServiceLocator.ProvideResourceManager(_resourceManager);
    }
}
