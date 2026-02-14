using UnityEngine;
using UnityEngine.UI;

public class WorkerUI : MonoBehaviour
{
    [SerializeField] private AllResourcesVisualizationConfig _resourcesVisConfig;

    [Header("UI Panels")]
    [SerializeField] private GameObject _carryPanel;

    [Header("Carrying Panel")]
    [SerializeField] private Image _carryResource;

    private Worker _worker;

    public void Init(Worker worker)
    {
        _worker = worker;

        ServiceLocator.GetService<EventBus>().Subscribe<OnResourceMined>(ChangeCarryingResource);
        ServiceLocator.GetService<EventBus>().Subscribe<OnJobFinished>(ClearCarryingResource);
    }

    private void OpenCarryPanel()
    {
        _carryPanel.SetActive(true);
    }

    private void CloseAllPanels()
    {
        _carryPanel.SetActive(false);
    }

    private void ClearCarryingResource(OnJobFinished signal)
    {
        if(signal._worker == _worker)
        {
            _carryResource.sprite = null;
            _carryPanel.SetActive(false);
        }
    }

    private void ChangeCarryingResource(OnResourceMined signal)
    {
        if(signal._worker == _worker)
        {
            _carryPanel.SetActive(true);

            ResourceType resourceType = signal._resourceType;
        
            ResourceVisualizationConfig currentRvs = null;
            foreach(ResourceVisualizationConfig rvs in _resourcesVisConfig.AllResourcesVisConfigs)
            {
                if(rvs.resourceType.ToString() == resourceType.ToString())
                {
                    currentRvs = rvs;
                }
            }

            if(currentRvs != null)
            {
                _carryResource.sprite = currentRvs.ResourceSprite;
            }
            else
            {
                Debug.LogWarning($"ResourceVisualizationConfig for {resourceType.ToString()} not founded.");
            }
        }
    }
}
