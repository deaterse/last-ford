using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using TMPro;

public class BuildingSystemUI : MonoBehaviour
{
    [Header("Transform (To Use Instantiate)")]
    [SerializeField] private Transform _typesContentBox;
    [SerializeField] private Transform _buildingsContentBox;
    [SerializeField] private GameObject _buildingsGridBox;

    [Header("Config")]
    [SerializeField] private AllBuildingsConfig _allBuildingsConfig;

    [Header("Prefabs")]
    [SerializeField] private GameObject _buildingTypePrefab;
    [SerializeField] private GameObject _buildingButtonPrefab;
    [SerializeField] private GameObject _buildingContainerPrefab;

    private Dictionary<string, GameObject> _typesButtons = new();
    private Dictionary<string, GameObject> _typesContainers = new();
    private Dictionary<BuildingData, GameObject> _buildingButtons = new();

    private BuildSystem _buildSystem;
    private GameObject _openedGrid;

    public void Init(BuildSystem buildSystem)
    {
        _buildSystem = buildSystem;

        ClearAllListeners();

        SpawnBuildingTypes();
        SpawnTypesContainers();
        SpawnBuildingsButtons();

        ServiceLocator.GetService<EventBus>().Subscribe<OnResourceChanged>(CheckAllBuildings);

        CheckAllBuildings();
    }

    private void OnDisable()
    {
        ServiceLocator.GetService<EventBus>().Unsubscribe<OnResourceChanged>(CheckAllBuildings);
    }

    private void SpawnBuildingTypes()
    {
        string[] allTypes = Enum.GetNames(typeof(BuildingType));

        if(allTypes.Length <= 0) return;

        for(int i = 0; i < allTypes.Length; i++)
        {
            GameObject currentObj = Instantiate(_buildingTypePrefab, _typesContentBox);
            _typesButtons[allTypes[i]] = currentObj;

            if (currentObj.TryGetComponent<BuildingTypeUI>(out BuildingTypeUI buildingTypeUI))
            {
                TMP_Text currentNameText = buildingTypeUI.TypeText;
                // later add here image and etc.
                currentNameText.text = allTypes[i];
            }
        }
    }

    private void SpawnTypesContainers()
    {
        foreach(string type in _typesButtons.Keys)
        {
            GameObject current = Instantiate(_buildingContainerPrefab, _buildingsContentBox);

            _typesContainers[type.ToString()] = current;

            current.SetActive(false);
        }

        AddTypesButtonsListeners();
    }

    private void AddTypesButtonsListeners()
    {
        foreach(KeyValuePair<string, GameObject> kvp in _typesButtons)
        {
            if(kvp.Value.TryGetComponent<Button>(out Button currentTypeButton))
            {
                GameObject currentContainer = _typesContainers[kvp.Key];
                currentTypeButton.onClick.AddListener(() => OpenBuildingsContainer(currentContainer));
            }
        }
    }

    private void AddBuildingsButtonsListeners()
    {
        foreach(KeyValuePair<BuildingData, GameObject> kvp in _buildingButtons)
        {
            if(kvp.Value.TryGetComponent<Button>(out Button currentBuildingButton))
            {
                BuildingData currentBuildingData = kvp.Key;
                currentBuildingButton.onClick.AddListener(() => _buildSystem.StartBuilding(currentBuildingData));
            }
        }
    }

    private void SpawnBuildingsButtons()
    {
        foreach(BuildingData bd in _allBuildingsConfig.AllBuildingsData)
        {
            BuildingType currentType = bd.buildingType;
            Transform currentTransform = _typesContainers[currentType.ToString()].transform;

            GameObject currentButton = Instantiate(_buildingButtonPrefab, currentTransform);

            _buildingButtons[bd] = currentButton;

            if (currentButton.TryGetComponent(out BuildingButtonUI buildingButtonUI))
            {
                TMP_Text currentNameText = buildingButtonUI.TypeText;
                // later add here image and etc.
                currentNameText.text = bd.displayedName;
            }
        }

        AddBuildingsButtonsListeners();
    }

    private void OpenBuildingsContainer(GameObject container)
    {
        foreach(GameObject tc in _typesContainers.Values)
        {
            tc.SetActive(false);
        }

        if(_typesContainers.ContainsValue(container))
        {
            if(_openedGrid == container)
            {
                _buildingsGridBox.SetActive(false);
                container.SetActive(false);

                _openedGrid = null;
            }
            else
            {
                _buildingsGridBox.SetActive(true);
                container.SetActive(true);

                _openedGrid = container;
            }
        }
    }

    private void CheckAllBuildings(OnResourceChanged signal = null)
    {
        foreach(BuildingData buildingData in _buildingButtons.Keys)
        {
            bool isEnough = _buildSystem.IsResourcesEnoughPublic(buildingData);
            if(_buildingButtons[buildingData].TryGetComponent(out Button currentBuildingButton))
            {
                SetButtonInteractable(currentBuildingButton, isEnough);
            }
        }
    }

    private void SetButtonInteractable(Button button, bool value)
    {
        button.interactable = value;
    }

    private void ClearAllListeners()
    {
        ServiceLocator.GetService<EventBus>().Unsubscribe<OnResourceChanged>(CheckAllBuildings);

        foreach (Transform child in _typesContentBox)
            Destroy(child.gameObject);
        
        foreach (Transform child in _buildingsContentBox)
            Destroy(child.gameObject);

        _buildingButtons.Clear();
        _typesButtons.Clear();
        _typesContainers.Clear();
    }
}
