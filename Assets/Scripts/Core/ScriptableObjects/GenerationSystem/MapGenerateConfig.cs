using UnityEngine;

[CreateAssetMenu(fileName = "MapGenerateConfig", menuName = "Scriptable Objects/MapGenerate Config")]
public class MapGenerateConfig : ScriptableObject
{
    [SerializeField] private bool _generateHeight;
    [SerializeField] private bool _generateRiver;
    [SerializeField] private bool _generateShore;
    [SerializeField] private bool _generatePreSand;
    [SerializeField] private bool _generateStones;
    [SerializeField] private bool _generateForests;
    [SerializeField] private bool _generateFertility;

    public bool GenerateHeight => _generateHeight;
    public bool GenerateRiver => _generateRiver;
    public bool GenerateShore => _generateShore;
    public bool GeneratePreSand => _generatePreSand;
    public bool GenerateStones => _generateStones;
    public bool GenerateForests => _generateForests;
    public bool GenerateFertility => _generateFertility;
}
