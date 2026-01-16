using UnityEngine;
using UnityEngine.InputSystem;

public class InputListener : MonoBehaviour
{
    [SerializeField] private MainControlMap _mainControlMap;

    private void Awake()
    {
        EnableControlMap();
    }

    private void Update()
    {
        CameraMovement();
        CameraZoom();
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
        GameEvents.InvokeOnShowFertilityMap();
    }

    private void PlaceBuilding(InputAction.CallbackContext obj)
    {
        GameEvents.InvokeOnInputBuildingBuilded();
    }

    private void CameraMovement()
    {
        Vector2 movementDirection = _mainControlMap.Gameplay.CameraMovement.ReadValue<Vector2>();

        if(movementDirection.x != 0 || movementDirection.y != 0)
        {
            GameEvents.InvokeOnInputCameraMovement(movementDirection);
        }
    }

    private void CameraZoom()
    {
        float zoomStrength = _mainControlMap.Gameplay.CameraZoom.ReadValue<float>();

        if(zoomStrength != 0)
        {
            GameEvents.InvokeOnInputCameraZoom(zoomStrength);
        }
    }
}
