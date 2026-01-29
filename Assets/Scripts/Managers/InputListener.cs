using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using System.Collections.Generic;

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
            _mainControlMap.Gameplay.OnMouseClick.performed += OnMouseClick;
        }
    }

    private void OnDisable()
    {
        _mainControlMap.Gameplay.BuildingSystem.performed -= PlaceBuilding;
        _mainControlMap.Gameplay.FertilityMap.performed -= ShowFertilityMap;
        _mainControlMap.Gameplay.OnMouseClick.performed -= OnMouseClick;
    }

    private void ShowFertilityMap(InputAction.CallbackContext obj)
    {
        ServiceLocator.GetService<EventBus>().Invoke<OnShowFertilityMap>(new OnShowFertilityMap());
    }

    private void PlaceBuilding(InputAction.CallbackContext obj)
    {
        if(IsPointerOverUI()) return;
        
        ServiceLocator.GetService<EventBus>().Invoke<OnInputBuildingBuilded>(new OnInputBuildingBuilded());
    }

    private void CameraMovement()
    {
        Vector2 movementDirection = _mainControlMap.Gameplay.CameraMovement.ReadValue<Vector2>();

        if(movementDirection.x != 0 || movementDirection.y != 0)
        {
            ServiceLocator.GetService<EventBus>().Invoke<OnInputCameraMovement>(new OnInputCameraMovement(movementDirection));
        }
    }

    private void CameraZoom()
    {
        float zoomStrength = _mainControlMap.Gameplay.CameraZoom.ReadValue<float>();

        if(zoomStrength != 0)
        {
            ServiceLocator.GetService<EventBus>().Invoke<OnInputCameraZoom>(new OnInputCameraZoom(zoomStrength));
        }
    }

    private void OnMouseClick(InputAction.CallbackContext obj)
    {
        if(IsPointerOverUI()) return;
        
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

        if(hit.collider != null)
        {
            if(hit.collider.TryGetComponent<BuildingUI>(out BuildingUI buildingUI))
            {
                buildingUI.OnBuildingClicked();
            }
        }
    }

    private bool IsPointerOverUI()
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = Input.mousePosition;
        
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);
        
        return results.Count > 0;
    }
}
