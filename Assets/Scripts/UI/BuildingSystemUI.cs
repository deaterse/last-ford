using UnityEngine;
using System;
using System.Collections.Generic;
using TMPro;

public class BuildingSystemUI : MonoBehaviour
{
    [SerializeField] private Transform _typesContentBox;
    [SerializeField] private Transform _buildingsContentBox;
    [SerializeField] private AllBuildingsConfig _allBuildingsConfig;

    [Header("Prefabs")]
    [SerializeField] private GameObject _buildingTypePrefab;
    [SerializeField] private GameObject _buildingButtonPrefab;
    [SerializeField] private GameObject _buildingContainerPrefab;

    private Dictionary<string, GameObject> _typesButtons = new();
    private Dictionary<string, GameObject> _typesContainers = new();

    public void Init()
    {
        SpawnBuildingTypes();
        SpawnTypesContainers();
        SpawnBuildingsButtons();
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
    }

    private void SpawnBuildingsButtons()
    {
        foreach(BuildingData bd in _allBuildingsConfig.AllBuildingsData)
        {
            BuildingType currentType = bd.buildingType;
            Transform currentTransform = _typesContainers[currentType.ToString()].transform;

            GameObject currentButton = Instantiate(_buildingButtonPrefab, currentTransform);

            if (currentButton.TryGetComponent<BuildingButtonUI>(out BuildingButtonUI buildingButtonUI))
            {
                TMP_Text currentNameText = buildingButtonUI.TypeText;
                // later add here image and etc.
                currentNameText.text = bd.displayedName;
            }
        }
    }
}
