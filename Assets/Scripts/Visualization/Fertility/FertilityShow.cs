using UnityEngine;

public class FertilityShow : MonoBehaviour
{
    [SerializeField] private GameObject _fertilityOverlay;

    private bool _isEnabled = false;

    private void Start()
    {
        _fertilityOverlay.SetActive(false);
    }

    private void OnEnable()
    {
        GameEvents.OnShowFertilityMap += ShowFertilityMap;
    }

    private void OnDisable()
    {
        GameEvents.OnShowFertilityMap -= ShowFertilityMap;
    }

    private void ShowFertilityMap()
    {
        _fertilityOverlay.SetActive(!_isEnabled);

        _isEnabled = !_isEnabled;
    }
}
