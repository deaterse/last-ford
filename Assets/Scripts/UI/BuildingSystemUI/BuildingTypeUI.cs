using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuildingTypeUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _typeText;
    [SerializeField] private Image _typeImage;

    public TMP_Text TypeText => _typeText;
    public Image TypeImage => _typeImage;
}
