using UnityEngine;
using TMPro;

public class DebugUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _forestCountText;
    [SerializeField] private TMP_Text _stoneCountText;
    
    public void Init()
    {
        ServiceLocator.GetService<EventBus>().Subscribe<OnForestsGenerated>(UpdateForestUI);
        ServiceLocator.GetService<EventBus>().Subscribe<OnStonesGenerated>(UpdateStoneUI);
    }

    private void OnDisable()
    {
        ServiceLocator.GetService<EventBus>().Unsubscribe<OnForestsGenerated>(UpdateForestUI);
        ServiceLocator.GetService<EventBus>().Unsubscribe<OnStonesGenerated>(UpdateStoneUI);
    }

    private void UpdateForestUI(OnForestsGenerated signal)
    {
        _forestCountText.text = $"{signal._value}";
    }

    private void UpdateStoneUI(OnStonesGenerated signal)
    {
        _stoneCountText.text = $"{signal._value}";
    }
}
