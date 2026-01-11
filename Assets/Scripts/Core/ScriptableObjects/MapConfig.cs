using UnityEngine;


[CreateAssetMenu(fileName = "MapConfig", menuName = "Scriptable Objects/Map Config")]
public class MapConfig : ScriptableObject
{
    [SerializeField] private Vector2Int _mapSize;
    [SerializeField] private int _forestPercent = 80;
    [SerializeField] private int _stoneClusterSize = 10;

    public Vector2Int MapSize => _mapSize;

    public int ForestPercent => _forestPercent;
    public int StoneClusterSize => _stoneClusterSize;
}