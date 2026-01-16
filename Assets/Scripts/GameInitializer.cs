using UnityEngine;

public class GameInitializer : MonoBehaviour
{
    [SerializeField] private WorldGeneratorNew _worldGenerator;

    [SerializeField] private DebugUI _debugUI;

    [SerializeField] private BuildSystem _buildSystem;
    [SerializeField] private BuildingManager _buildingManager;

    private void Awake()
    {
        #if UNITY_EDITOR || DEVELOPMENT_BUILD
            _debugUI.Init();
        #endif
        _buildSystem.Init();
        _buildingManager.Init();

        _worldGenerator.GenerateWorld();
    }
}
