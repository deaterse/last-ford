using UnityEngine;
using System;
using System.Collections.Generic;
using TMPro;

public class ResourceUI : MonoBehaviour
{
    [SerializeField] private AllResourcesVisualizationConfig _resourcesVisConfig;
    [SerializeField] private Transform _resourcePanelParent;
    [SerializeField] private GameObject _resourcePanelPrefab;
    [SerializeField] private Dictionary<string, ResourcePanelUI> _allResourcesPanels = new();

    public void Init()
    {
        ServiceLocator.GetService<EventBus>().Subscribe<OnResourceChanged>(UpdateUI);

        SpawnResourcePanels();
    }

    private void OnDisable()
    {
        ServiceLocator.GetService<EventBus>().Unsubscribe<OnResourceChanged>(UpdateUI);
    }

    private void SpawnResourcePanels()
    {
        string[] allTypesStr = Enum.GetNames(typeof(ResourceType));

        foreach(string rtStr in allTypesStr)
        {
            GameObject resourcePanelObj = Instantiate(_resourcePanelPrefab, _resourcePanelParent);
            if(resourcePanelObj.TryGetComponent<ResourcePanelUI>(out ResourcePanelUI resourcePanelUI))
            {
                ResourceVisualizationConfig currentRvs = null;
                foreach(ResourceVisualizationConfig rvs in _resourcesVisConfig.AllResourcesVisConfigs)
                {
                    if(rvs.resourceType.ToString() == rtStr)
                    {
                        currentRvs = rvs;
                    }
                }
                resourcePanelUI.ResourceCountText.text = $"{0}";
                if(currentRvs != null)
                {
                    resourcePanelUI.ResourceNameText.text = $"{currentRvs.DisplayedName}";
                    resourcePanelUI.ResourceImage.sprite = currentRvs.ResourceSprite;
                }
                else
                {
                    Debug.LogWarning($"ResourceVisualizationConfig for {rtStr} not founded.");
                }

                _allResourcesPanels[rtStr] = resourcePanelUI;
            }
        }
    }

    private void UpdateUI(OnResourceChanged signal)
    {
        string resourceTypeStr = signal._resourceType.ToString();
        int amount = signal._value;

        if (!_allResourcesPanels.ContainsKey(resourceTypeStr)) return;

        ResourcePanelUI resourcePanelUI = _allResourcesPanels[resourceTypeStr];
        resourcePanelUI.ResourceCountText.text = $"{signal._value}";
    }
}
