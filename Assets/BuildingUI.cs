using UnityEngine;
using UnityEngine.UI;

public class BuildingUI : MonoBehaviour
{
    [SerializeField] private Building _thisBuilding;

    [Header("Buttons")]
    [SerializeField] private Button _upgradeButton;
    [SerializeField] private Button _removeButton;

    private void Start()
    {
        BindButtons();
    }

    private void BindButtons()
    {
        _upgradeButton.onClick.AddListener(UpgradeBuilding);
        _removeButton.onClick.AddListener(RemoveBuilding);
    }

    private void UpgradeBuilding()
    {
        ServiceLocator.GetService<EventBus>().Invoke(new TryUpdateBuilding(_thisBuilding));
    }


    private void RemoveBuilding()
    {
        ServiceLocator.GetService<EventBus>().Invoke(new TryRemoveBuilding(_thisBuilding));
    }
}
