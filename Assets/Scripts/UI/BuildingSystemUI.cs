using UnityEngine;
using System;
using TMPro;

public class BuildingSystemUI : MonoBehaviour
{
    [SerializeField] private Transform _contentBox;

    [Header("Prefabs")]
    [SerializeField] private GameObject _buildingTypePrefab;

    public void Init()
    {
        SpawnBuildingTypes();
    }

    private void SpawnBuildingTypes()
    {
        string[] allTypes = Enum.GetNames(typeof(BuildingType));

        if(allTypes.Length <= 0) return;

        for(int i = 0; i < allTypes.Length; i++)
        {
            GameObject currentObj = Instantiate(_buildingTypePrefab, _contentBox);

            if (currentObj.TryGetComponent<BuildingTypeUI>(out BuildingTypeUI buildingTypeUI))
            {
                TMP_Text currentNameText = buildingTypeUI.TypeText;
                // later add here image and etc.
                currentNameText.text = allTypes[i];
            }
        }
    }
}
