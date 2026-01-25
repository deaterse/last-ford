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
        ServiceLocator.GetService<EventBus>().Subscribe<OnShowFertilityMap>(ShowFertilityMap);
    }

    private void OnDisable()
    {
       ServiceLocator.GetService<EventBus>().Unsubscribe<OnShowFertilityMap>(ShowFertilityMap);
    }

    private void ShowFertilityMap(OnShowFertilityMap signal)
    {
        _fertilityOverlay.SetActive(!_isEnabled);

        _isEnabled = !_isEnabled;
    }
}
