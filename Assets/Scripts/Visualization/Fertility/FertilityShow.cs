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
        ServiceLocator.GetEventBus().Subscribe<OnShowFertilityMap>(ShowFertilityMap);
    }

    private void OnDisable()
    {
       ServiceLocator.GetEventBus().Unsubscribe<OnShowFertilityMap>(ShowFertilityMap);
    }

    private void ShowFertilityMap(OnShowFertilityMap signal)
    {
        _fertilityOverlay.SetActive(!_isEnabled);

        _isEnabled = !_isEnabled;
    }
}
