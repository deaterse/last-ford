using UnityEngine;

[CreateAssetMenu(fileName = "DayNightConfig", menuName = "Scriptable Objects/DayNight Config")]
public class DayNightConfig : ScriptableObject
{
    [SerializeField] private int _dayLength;
    [SerializeField] private int _nightLength;

    public int DayLength => _dayLength;
    public int NightLength => _nightLength;
}
