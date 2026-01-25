using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class ResourceUI : MonoBehaviour
{
    [SerializeField] private List<TMP_Text> _resourcesTexts;
    [SerializeField] private List<ResourceType> _resourcesTypes;

    public void Init()
    {
        ServiceLocator.GetService<EventBus>().Subscribe<OnResourceChanged>(UpdateUI);
    }

    private void OnDisable()
    {
        ServiceLocator.GetService<EventBus>().Unsubscribe<OnResourceChanged>(UpdateUI);
    }

    private void UpdateUI(OnResourceChanged signal)
    {
        ResourceType type = signal._resourceType;
        int amount = signal._value;

        if (!_resourcesTypes.Contains(type)) return;

        int index = _resourcesTypes.IndexOf(type);
        if(_resourcesTexts.Count >= index)
        {
            TMP_Text currentText = _resourcesTexts[index];
            currentText.text = $"{type.ToString()}: {amount}";
        }
    }
}
