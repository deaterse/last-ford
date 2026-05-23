using UnityEngine;

[CreateAssetMenu(fileName = "DayNightConfig", menuName = "Scriptable Objects/Cycle System/DayNight Config")]
public class DayNightConfig : ScriptableObject
{
    [SerializeField] private int _dayLength;
    [SerializeField] private int _nightLength;

    [Header("Light Intensity")]
    [SerializeField] private float _minIntensity;
    [SerializeField] private float _maxIntensity;

    public int DayLength => _dayLength;
    public int NightLength => _nightLength;

    public float MinIntesity => _minIntensity;
    public float MaxIntensity => _maxIntensity;
}
