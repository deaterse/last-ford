using UnityEngine;


[CreateAssetMenu(fileName = "MapConfig", menuName = "Scriptable Objects/GenerationSystem/Map Config")]
public class MapConfig : ScriptableObject
{
    [SerializeField] private Vector2Int _mapSize;

    public Vector2Int MapSize => _mapSize;
}