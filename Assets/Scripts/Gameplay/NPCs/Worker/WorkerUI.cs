using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WorkerUI : MonoBehaviour
{
    [SerializeField] private AllResourcesVisualizationConfig _resourcesVisConfig;

    [Header("UI Panels")]
    [SerializeField] private GameObject _carryPanel;

    [Header("Carrying Panel")]
    [SerializeField] private Image _carryResource;
    [SerializeField] private TMP_Text _carryIntText;

    private Worker _worker;

    public void Init(Worker worker)
    {
        _worker = worker;

        ServiceLocator.GetService<EventBus>().Subscribe<OnInventoryChanged>(ChangeCarryingResource);
    }

    private void OpenCarryPanel()
    {
        _carryPanel.SetActive(true);
    }

    private void CloseAllPanels()
    {
        _carryPanel.SetActive(false);
    }

    private void ClearCarryingResource()
    {
        _carryResource.sprite = null;
        _carryPanel.SetActive(false);
    }

    private void ChangeCarryingResource(OnInventoryChanged signal)
    {
        if(signal.worker == _worker)
        {
            if(signal.resource.Type != ResourceType.None)
            {
                _carryPanel.SetActive(true);

                ResourceType resourceType = signal.resource.Type;
            
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

                if(_carryIntText != null)
                {
                    _carryIntText.text = $"{signal.resource.Amount}";
                }
            }
            else
            {
                ClearCarryingResource();
            }
        }
    }
}
