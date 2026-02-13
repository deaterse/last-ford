using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

public class Pathfinder: IService
{
  private TerrainMap _terrainMap;
  private Tilemap _terrainTilemap;

  public Pathfinder(TerrainMap terrainMap, Tilemap terrainTilemap)
  {
    _terrainMap = terrainMap;
    _terrainTilemap = terrainTilemap;
  }

  public List<Vector3Int> FindPath(Vector3Int start, Vector3Int end)
  {
    if (start == end) return new List<Vector3Int> { start };
    if (!IsWalkable(end)) return null;
    
    Dictionary<Vector3Int, Vector3Int> cameFrom = new Dictionary<Vector3Int, Vector3Int>();
    Dictionary<Vector3Int, float> gScore = new Dictionary<Vector3Int, float>();
    Dictionary<Vector3Int, float> fScore = new Dictionary<Vector3Int, float>();
    
    List<Vector3Int> openSet = new List<Vector3Int> { start };
    HashSet<Vector3Int> closedSet = new HashSet<Vector3Int>();
    
    gScore[start] = 0;
    fScore[start] = Heuristic(start, end);
    
    int maxIterations = 4096;
    int currentIterations = 0;

    while (openSet.Count > 0 && currentIterations <= maxIterations)
    {
      currentIterations++;

      Vector3Int current = openSet[0];
      float minF = fScore.ContainsKey(current) ? fScore[current] : float.MaxValue;
      
      for (int i = 1; i < openSet.Count; i++)
      {
          Vector3Int node = openSet[i];
          if (fScore.ContainsKey(node) && fScore[node] < minF)
          {
            current = node;
            minF = fScore[node];
          }
      }
      
      if (current == end)
        return ReconstructPath(cameFrom, current);

      openSet.Remove(current);
      closedSet.Add(current);

      foreach (Vector3Int neighbor in GetNeighbours(current))
      {
        if (closedSet.Contains(neighbor) || !IsWalkable(neighbor))
          continue;
        
        float tentativeGScore = gScore[current] + Distance(current, neighbor);
        
        if (!gScore.ContainsKey(neighbor) || tentativeGScore < gScore[neighbor])
        {
          cameFrom[neighbor] = current;
          gScore[neighbor] = tentativeGScore;
          fScore[neighbor] = tentativeGScore + Heuristic(neighbor, end);
          
          if (!openSet.Contains(neighbor)) openSet.Add(neighbor);
        }
      }
    }

    return null;
  }
  
  private bool IsWalkable(Vector3Int cell)
  {
    if(_terrainMap.IsWalkable(cell.x, cell.y)) return true;

    return false;
  }
  
  private List<Vector3Int> GetNeighbours(Vector3Int cell)
  {
    return new List<Vector3Int>
    {
      cell + Vector3Int.up,
      cell + Vector3Int.down,
      cell + Vector3Int.left,
      cell + Vector3Int.right,
      
      cell + new Vector3Int(1, 1, 0),
      cell + new Vector3Int(-1, 1, 0),
      cell + new Vector3Int(1, -1, 0),
      cell + new Vector3Int(-1, -1, 0)
    };
  }
  
  private float Heuristic(Vector3Int a, Vector3Int b)
  {
    return Mathf.Abs(a.x - b.x) + Mathf.Abs(a.y - b.y);
  }
  
  private float Distance(Vector3Int a, Vector3Int b)
  {
    bool isDiagonal = a.x != b.x && a.y != b.y;
    return isDiagonal ? 1.414f : 1f;
  }

  private Vector3Int GetLowestFScore(List<Vector3Int> openSet, Dictionary<Vector3Int, float> fScore)
  {
    Vector3Int lowest = openSet[0];
    foreach (var node in openSet)
    {
      if (fScore.ContainsKey(node) && fScore[node] < fScore[lowest]) lowest = node;
    }
    return lowest;
  }
  
  private List<Vector3Int> ReconstructPath(Dictionary<Vector3Int, Vector3Int> cameFrom, Vector3Int current)
  {
    List<Vector3Int> path = new List<Vector3Int> { current };
    while (cameFrom.ContainsKey(current))
    {
      current = cameFrom[current];
      path.Insert(0, current);
    }
    return path;
  }

  public bool HasWay(Vector3Int start, Vector3Int end)
  {
    List<Vector3Int> path = new();
    path = FindPath(start, end);

    if(path == null)
    {
      return false;
    }

    return true;
  }

  //Create another service 
  public Vector3Int WorldToCell(Vector3 pos)
  {
    Vector3Int gridPos = _terrainTilemap.WorldToCell(pos);

    return gridPos;
  }

  public Vector3 GetCellCenterWorld(Vector3Int cell)
  {
    Vector3 worldPos = _terrainTilemap.GetCellCenterWorld(cell);

    return worldPos;
  }
}
