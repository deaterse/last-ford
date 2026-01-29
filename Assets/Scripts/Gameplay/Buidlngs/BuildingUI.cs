using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BuildingUI : MonoBehaviour
{
    [SerializeField] private Building _thisBuilding;
    [SerializeField] private GameObject _buildingCanvas;

    [Header("Buttons")]
    [SerializeField] private Button _upgradeButton;
    [SerializeField] private Button _removeButton;

    private bool _activeCanvas;

    private void Start()
    {
        _buildingCanvas.SetActive(false);
        _activeCanvas = false; 

        BindButtons();
    }

    public void OnBuildingClicked()
    {
        if(!_activeCanvas)
        {
            _buildingCanvas.SetActive(true);
            _activeCanvas = true;
        }
        else
        {
            _buildingCanvas.SetActive(false);
            _activeCanvas = false;
        }
    }

    private void BindButtons()
    {
        _upgradeButton.onClick.AddListener(UpgradeBuilding);
        _removeButton.onClick.AddListener(RemoveBuilding);
    }

    private void UpgradeBuilding()
    {
        ServiceLocator.GetService<EventBus>().Invoke(new TryUpdateBuilding(_thisBuilding));
    }


    private void RemoveBuilding()
    {
        ServiceLocator.GetService<EventBus>().Invoke(new TryRemoveBuilding(_thisBuilding));
    }
}
