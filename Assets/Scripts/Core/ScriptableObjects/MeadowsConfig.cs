using UnityEngine;

[CreateAssetMenu(fileName = "MeadowsConfig", menuName = "Scriptable Objects/Meadows Config")]
public class MeadowsConfig : ScriptableObject
{
    [SerializeField] private int _meadowsCount;
    [SerializeField] private int _minMeadowSize;
    [SerializeField] private int _maxMeadowSize;
    [SerializeField] private int _minMeadowRadius;
    [SerializeField] private int _maxMeadowRadius;

    public int MeadowsCount => _meadowsCount;
    public int MinMeadowSize => _minMeadowSize;
    public int MaxMeadowSize => _maxMeadowSize;
    public int MinMeadowRadius => _minMeadowRadius;
    public int MaxMeadowRadius => _maxMeadowRadius;
}
