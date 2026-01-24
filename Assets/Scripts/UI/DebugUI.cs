using UnityEngine;
using TMPro;

public class DebugUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _forestCountText;
    [SerializeField] private TMP_Text _stoneCountText;
    
    public void Init()
    {
        ServiceLocator.GetEventBus().Subscribe<OnForestsGenerated>(UpdateForestUI);
        ServiceLocator.GetEventBus().Subscribe<OnStonesGenerated>(UpdateStoneUI);
    }

    private void OnDisable()
    {
        ServiceLocator.GetEventBus().Unsubscribe<OnForestsGenerated>(UpdateForestUI);
        ServiceLocator.GetEventBus().Unsubscribe<OnStonesGenerated>(UpdateStoneUI);
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
