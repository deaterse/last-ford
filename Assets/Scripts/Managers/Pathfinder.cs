using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

public class Pathfinder: IService
{
  private TerrainMap _terrainMap;
  private Tilemap _terrainTilemap;

  private List<Vector3Int> _neighbours = new();
  private List<Vector3Int> _openSet = new();
  private HashSet<Vector3Int> _closedSet = new();

  private Dictionary<Vector3Int, Vector3Int> _cameFrom = new();
  private Dictionary<Vector3Int, float> _gScore = new();
  private Dictionary<Vector3Int, float> _fScore = new();

  public Pathfinder(TerrainMap terrainMap, Tilemap terrainTilemap)
  {
    _terrainMap = terrainMap;
    _terrainTilemap = terrainTilemap;
  }

  public List<Vector3Int> FindPath(Vector3Int start, Vector3Int end, bool withCorners = true)
  {
    if (start == end) return new List<Vector3Int> { start };
    if (!IsWalkable(end)) return null;
    
    _cameFrom.Clear();
    _gScore.Clear();
    _fScore.Clear();
    
    _openSet.Clear();
    _closedSet.Clear();

    _openSet.Add(start);
    
    _gScore[start] = 0;
    _fScore[start] = Heuristic(start, end);
    
    int maxIterations = 4096;
    int currentIterations = 0;

    while (_openSet.Count > 0 && currentIterations <= maxIterations)
    {
      currentIterations++;

      Vector3Int current = _openSet[0];
      float minF = _fScore.ContainsKey(current) ? _fScore[current] : float.MaxValue;
      
      for (int i = 1; i < _openSet.Count; i++)
      {
          Vector3Int node = _openSet[i];
          if (_fScore.ContainsKey(node) && _fScore[node] < minF)
          {
            current = node;
            minF = _fScore[node];
          }
      }
      
      if (current == end)
        return ReconstructPath(_cameFrom, current);

      _openSet.Remove(current);
      _closedSet.Add(current);

      if(withCorners)
      {
        GetNeighbours(current);
      }
      else
      {
        GetNeighboursWithourCorners(current);
      }

      foreach (Vector3Int neighbor in _neighbours)
      {
        if (_closedSet.Contains(neighbor) || !IsWalkable(neighbor))
          continue;
        
        float tentativeGScore = _gScore[current] + Distance(current, neighbor);
        
        if (!_gScore.ContainsKey(neighbor) || tentativeGScore < _gScore[neighbor])
        {
          _cameFrom[neighbor] = current;
          _gScore[neighbor] = tentativeGScore;
          _fScore[neighbor] = tentativeGScore + Heuristic(neighbor, end);
          
          if (!_openSet.Contains(neighbor)) _openSet.Add(neighbor);
        }
      }
    }

    return null;
  }
  
  private bool IsWalkable(Vector3Int cell)
  {
    if(_terrainMap.IsWalkable(cell.x, cell.y) && !ServiceLocator.GetService<BuildingManager>().buildingMap.IsTaken(cell.x, cell.y)) return true;

    return false;
  }

  private void GetNeighboursWithourCorners(Vector3Int cell)
  {
    _neighbours.Clear();

    _neighbours.Add(cell + Vector3Int.up);
    _neighbours.Add(cell + Vector3Int.down);
    _neighbours.Add(cell + Vector3Int.left);
    _neighbours.Add(cell + Vector3Int.right);
  }
  
  private void GetNeighbours(Vector3Int cell)
  {
    _neighbours.Clear();

    _neighbours.Add(cell + Vector3Int.up);
    _neighbours.Add(cell + Vector3Int.down);
    _neighbours.Add(cell + Vector3Int.left);
    _neighbours.Add(cell + Vector3Int.right);
      
    _neighbours.Add(cell + new Vector3Int(1, 1, 0));
    _neighbours.Add(cell + new Vector3Int(-1, 1, 0));
    _neighbours.Add(cell + new Vector3Int(1, -1, 0));
    _neighbours.Add(cell + new Vector3Int(-1, -1, 0));
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
  
  private List<Vector3Int> ReconstructPath(Dictionary<Vector3Int, Vector3Int> cameFrom, Vector3Int current)
  {
    List<Vector3Int> path = new List<Vector3Int> {current};

    while (cameFrom.ContainsKey(current))
    {
      current = cameFrom[current];
      path.Insert(0, current);
    }

    return path;
  }

  public bool HasWay(Vector3Int start, Vector3Int end)
  {
    List<Vector3Int> path = FindPath(start, end);

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
