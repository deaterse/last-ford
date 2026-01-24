using UnityEngine;
using UnityEngine.InputSystem;

public class InputListener : MonoBehaviour
{
    [SerializeField] private MainControlMap _mainControlMap;
    
    private bool _initialized = false;

    public void Init()
    {
        EnableControlMap();

        _initialized = true;
    }

    private void Update()
    {
        if(_initialized)
        {
            CameraMovement();
            CameraZoom();
        }
    }

    private void EnableControlMap()
    {
        _mainControlMap = new MainControlMap();
        _mainControlMap.Enable(); 
    }

    private void OnEnable()
    {
        if (_mainControlMap != null)
        {
            _mainControlMap.Gameplay.BuildingSystem.performed += PlaceBuilding;
            _mainControlMap.Gameplay.FertilityMap.performed += ShowFertilityMap;
        }
    }

    private void OnDisable()
    {
        _mainControlMap.Gameplay.BuildingSystem.performed -= PlaceBuilding;
        _mainControlMap.Gameplay.FertilityMap.performed -= ShowFertilityMap;
    }

    private void ShowFertilityMap(InputAction.CallbackContext obj)
    {
        ServiceLocator.GetEventBus().Invoke<OnShowFertilityMap>(new OnShowFertilityMap());
    }

    private void PlaceBuilding(InputAction.CallbackContext obj)
    {
        ServiceLocator.GetEventBus().Invoke<OnInputBuildingBuilded>(new OnInputBuildingBuilded());
    }

    private void CameraMovement()
    {
        Vector2 movementDirection = _mainControlMap.Gameplay.CameraMovement.ReadValue<Vector2>();

        if(movementDirection.x != 0 || movementDirection.y != 0)
        {
            ServiceLocator.GetEventBus().Invoke<OnInputCameraMovement>(new OnInputCameraMovement(movementDirection));
        }
    }

    private void CameraZoom()
    {
        float zoomStrength = _mainControlMap.Gameplay.CameraZoom.ReadValue<float>();

        if(zoomStrength != 0)
        {
            ServiceLocator.GetEventBus().Invoke<OnInputCameraZoom>(new OnInputCameraZoom(zoomStrength));
        }
    }
}
