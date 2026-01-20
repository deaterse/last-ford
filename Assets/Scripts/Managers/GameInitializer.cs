using UnityEngine;

public class GameInitializer : MonoBehaviour
{
    //Configs
    [SerializeField] private StartResourcesConfig _startResourcesConfig;

    [SerializeField] private WorldGeneratorNew _worldGenerator;

    [SerializeField] private DebugUI _debugUI;
    [SerializeField] private ResourceUI _resourceUI;
    [SerializeField] private BuildingSystemUI _buildingUI;

    [SerializeField] private BuildSystem _buildSystem;
    [SerializeField] private BuildingManager _buildingManager;

    private void Awake()
    {
        #if UNITY_EDITOR || DEVELOPMENT_BUILD
            _debugUI.Init();
        #endif
        _resourceUI.Init();
        _buildingUI.Init();

        _buildSystem.Init();
        _buildingManager.Init();

        _worldGenerator.GenerateWorld();

        InitResourceManager();
    }

    private void InitResourceManager()
    {
        ResourceManager _resourceManager = new ResourceManager(_startResourcesConfig);
        ServiceLocator.ProvideResourceManager(_resourceManager);
    }
}
