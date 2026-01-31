using UnityEngine;

[CreateAssetMenu(fileName = "MeadowsConfig", menuName = "Scriptable Objects/GenerationSystem/Meadows Config")]
public class MeadowsConfig : ScriptableObject
{
    [SerializeField] private int _minMeadowsCount;
    [SerializeField] private int _maxMeadowsCount;

    [SerializeField] private int _minMeadowSize;
    [SerializeField] private int _maxMeadowSize;

    [SerializeField] private int _minMeadowRadius;
    [SerializeField] private int _maxMeadowRadius;

    public int MinMeadowsCount => _minMeadowsCount;
    public int MaxMeadowsCount => _maxMeadowsCount;

    public int MinMeadowSize => _minMeadowSize;
    public int MaxMeadowSize => _maxMeadowSize;

    public int MinMeadowRadius => _minMeadowRadius;
    public int MaxMeadowRadius => _maxMeadowRadius;
}
