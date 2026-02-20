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

    [Header("Progress Slider")]
    [SerializeField] private GameObject _progressCanvas;
    [SerializeField] private Slider _progressSlider;

    private bool _activeCanvas;

    public void Init()
    {
        if(TryGetComponent<Building>(out Building building))
        {
            _thisBuilding = building;
        }
        else
        {
            Debug.LogError("Cant find Building component!");
        }
        _progressCanvas.SetActive(false);
        _buildingCanvas.SetActive(false);

        _activeCanvas = false;
    }

    public void OnBuildingBuilded()
    {
        _progressSlider.maxValue = _thisBuilding.buildingData.BuildingTime;
        _progressCanvas.SetActive(true);

        BindButtons();
    }

    public void OnBuildingClicked()
    {
        if(_thisBuilding.IsBuilded)
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
    }

    public void UpdateSlider(float value)
    {
        _progressSlider.value = value;
    }
    
    public void HideSlider()
    {
        _progressCanvas.SetActive(false);
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
