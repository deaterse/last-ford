using UnityEngine;
using TMPro;

public class DebugUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _forestCountText;
    [SerializeField] private TMP_Text _stoneCountText;
    
    public void Init()
    {
        GameEvents.OnForestsGenerated += UpdateForestUI;
        GameEvents.OnStonesGenerated += UpdateStoneUI;
    }

    private void OnDisable()
    {
        GameEvents.OnForestsGenerated -= UpdateForestUI;
        GameEvents.OnStonesGenerated -= UpdateStoneUI;
    }

    private void UpdateForestUI(int count)
    {
        _forestCountText.text = $"{count}";
    }

    private void UpdateStoneUI(int count)
    {
        _stoneCountText.text = $"{count}";
    }
}
