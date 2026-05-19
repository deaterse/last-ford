using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class BuildingUI : MonoBehaviour
{
    private Building _thisBuilding;

    [Header("Canvas")]
    [SerializeField] private GameObject _buildingCanvas;

    [Header("Buttons")]
    [SerializeField] private Button _upgradeButton;
    [SerializeField] private Button _removeButton;

    [Header("Labels")]
    [SerializeField] private TMP_Text _buildingName;
    [SerializeField] private TMP_Text _workersCount;
    [SerializeField] private TMP_Text _levelCount;

    [Header("Progress Slider")]
    [SerializeField] private GameObject _progressCanvas;
    [SerializeField] private Slider _progressSlider;

    private bool _activeCanvas;

    public void Init()
    {
        InitEvents();

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

    private void InitEvents()
    {
        ServiceLocator.GetService<EventBus>().Subscribe<OnWorkerAssigned>(UpdateWorkerText);
    }

    private void UpdateWorkerText(OnWorkerAssigned signal)
    {
        if(signal._building == _thisBuilding)
        {
            if(_thisBuilding.buildingData.GetLevel(_thisBuilding.Level).WorkerSlots > 0)
            {
                _workersCount.text = $"Workers: {_thisBuilding.AssignedWorkers.Count}/{_thisBuilding.buildingData.GetLevel(_thisBuilding.Level).WorkerSlots}";
            }
            else
            {
                _workersCount.gameObject.SetActive(false);
            }
        }
    }

    public void InitBuildingUI()
    {
        _buildingName.text = _thisBuilding.buildingData.displayedName;
        _levelCount.text = $"Level: {_thisBuilding.Level}";

        if(_thisBuilding.buildingData.GetLevel(_thisBuilding.Level).WorkerSlots > 0)
        {
            _workersCount.text = $"Workers: {_thisBuilding.AssignedWorkers.Count}/{_thisBuilding.buildingData.GetLevel(_thisBuilding.Level).WorkerSlots}";
        }
        else
        {
            _workersCount.gameObject.SetActive(false);
        }
    }

    public void OnBuildingBuilded()
    {
        _progressSlider.maxValue = _thisBuilding.buildingData.BuildingTime;
        _progressCanvas.SetActive(true);

        InitBuildingUI();
        BindButtons();
    }

    public void OnBuildingClicked()
    {
        if(_thisBuilding.IsBuilded)
        {
            if(!_activeCanvas)
            {
                ShowUI();
            }
            else
            {
                HideUI();
            }
        }
    }

    public void HideUI()
    {
        if(TryGetComponent<MiningRadius>(out MiningRadius _miningRadius))
        {
            _miningRadius.OffVisualize();
        }

        _buildingCanvas.SetActive(false);
        _activeCanvas = false;
    }

    public void ShowUI()
    {
        if(TryGetComponent<MiningRadius>(out MiningRadius _miningRadius))
        {
            _miningRadius.OnVisualize();
        }

        _buildingCanvas.SetActive(true);
        _activeCanvas = true;
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

    private void OnDestroy()
    {
        ServiceLocator.GetService<EventBus>().Unsubscribe<OnWorkerAssigned>(UpdateWorkerText);
    }
}
