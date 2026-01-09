using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

public class StonesGenerator : MonoBehaviour
{
    // public static StonesGenerator Instance { get; private set; }

    // [SerializeField] private TilesConfig _tilesConfig;
    // [Header("List of Tiles")]
    // [SerializeField] private List<TileBase> _stoneTiles;
    // [SerializeField] private List<TileBase> _stoneTerrainTiles;
    
    // [SerializeField] private List<TileBase> _ironTiles;
    // [SerializeField] private List<TileBase> _goldTiles;

    // [Header("Tilemap Components")]
    // [SerializeField] private Tilemap _resourceMap;
    // [SerializeField] private Tilemap _terrainMap;
    // [SerializeField] private Tilemap _waterMap;

    // private MapConfig _mapConfig;

    // private void Awake()
    // {
    //     Instance = this;
    // }

    // public void StartGenerateStones(MapConfig mapConfig)
    // {
    //     _mapConfig = mapConfig;

    //     int stonesCount = Random.Range(5, 7);
    //     int stoneSize = mapConfig.StoneClusterSize;

    //     for(int i = 0; i < stonesCount; i++)
    //     {
    //         GenerateStones(stoneSize);
    //     }
    // }

    // private TileBase RandomTile(List<TileBase> tileList)
    // {
    //     return tileList[Random.Range(0, tileList.Count)];
    // }

    // private Vector3Int RandomPosMap()
    // {
    //     return new Vector3Int(Random.Range(0, _mapConfig.MapSize.x), Random.Range(0, _mapConfig.MapSize.y), 0);
    // }

    // private void GenerateStones(int stoneSize)
    // {
    //     Vector2Int mapSize = _mapConfig.MapSize;
    //     Vector3Int current = RandomPosMap();

    //     bool isEligTile(Vector3Int pos) 
    //     {
    //         var waterCheck = _waterMap.GetTile(pos);
    //         var sandCheck = _terrainMap.GetTile(pos);

    //         if(!_tilesConfig._sandTiles.Contains(sandCheck) && waterCheck == null)
    //         {
    //             return true;
    //         }
    //         else
    //         {
    //             return false;
    //         }
    //     }

    //     int placedCount = 0;
    //     int iterations = 0;
    //     int oreTiles = Random.Range(0, 10);
        
    //     List<TileBase> choosedOre = Random.Range(0, 2) == 0 ? _ironTiles : _goldTiles;

    //     while (placedCount < stoneSize && iterations < stoneSize*5)
    //     {
    //         iterations++;

    //         if (GenerateMap.Instance.CanPlace(current))
    //         {
    //             if (isEligTile(current) && _resourceMap.GetTile(current) == null)
    //             {
    //                 _resourceMap.SetTile(current, RandomTile(choosedOre));
    //                 _terrainMap.SetTile(current, RandomTile(_stoneTerrainTiles));

    //                 placedCount++;
    //             }
    //         }
            
    //         List<Vector3Int> allNeighbours = GetNeighbours(current);
            
    //         for (int j = 0; j < allNeighbours.Count; j++)
    //         {
    //             int randomIndex = Random.Range(j, allNeighbours.Count);
    //             Vector3Int temp = allNeighbours[j];
    //             allNeighbours[j] = allNeighbours[randomIndex];
    //             allNeighbours[randomIndex] = temp;
    //         }
            
    //         foreach (Vector3Int n in allNeighbours)
    //         {
    //             if (placedCount >= stoneSize) break;
                
    //             if (GenerateMap.Instance.CanPlace(n))
    //             {
    //                 if (_resourceMap.GetTile(n) == null)
    //                 {
    //                     if (isEligTile(n))
    //                     {
    //                         if(placedCount < oreTiles)
    //                         {
    //                             _resourceMap.SetTile(n, RandomTile(choosedOre));
    //                         }
    //                         else
    //                         {
    //                             _resourceMap.SetTile(n, RandomTile(_stoneTiles));
    //                         }
    //                         _terrainMap.SetTile(n, RandomTile(_stoneTerrainTiles));

    //                         placedCount++;
    //                     }
    //                 }
    //             }
    //         }
            
    //         Vector3Int randomN = allNeighbours[Random.Range(0, allNeighbours.Count)];
    //         current = randomN;
    //     }
    // }

    // private List<Vector3Int> GetNeighbours(Vector3Int pos)
    // {
    //     List<Vector3Int> allNeighbours = new List<Vector3Int>();

    //     allNeighbours.Add(new Vector3Int(pos.x-1, pos.y, 0));
    //     allNeighbours.Add(new Vector3Int(pos.x+1, pos.y, 0));
    //     allNeighbours.Add(new Vector3Int(pos.x, pos.y-1, 0));
    //     allNeighbours.Add(new Vector3Int(pos.x, pos.y+1, 0));

    //     return allNeighbours;
    // }
}
