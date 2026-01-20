using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class MeadowGenerator
{
    private MeadowMap _meadowMap;
    private TerrainMap _terrainMap;
    private MeadowsConfig _meadowsConfig;

    public MeadowGenerator(MeadowMap meadowMap, TerrainMap terrainMap, MeadowsConfig meadowsConfig)
    {
        _meadowMap = meadowMap;
        _terrainMap = terrainMap;
        _meadowsConfig = meadowsConfig;
    }

    public void Generate()
    {
        int meadowsCount = Random.Range(_meadowsConfig.MinMeadowsCount, _meadowsConfig.MaxMeadowsCount);
        for(int i = 0; i < meadowsCount; i++)
        {
            GenerateMeadow();
        }

        Debug.Log($"Generated {meadowsCount} Meadows");
    }

    private void GenerateMeadow()
    {
        Vector2Int center = RandomPos();
        if (center == Vector2Int.zero) return;
        
        int targetSize = Random.Range(
            _meadowsConfig.MinMeadowSize, 
            _meadowsConfig.MaxMeadowSize
        );
        
        HashSet<Vector2Int> meadow = new HashSet<Vector2Int>();
        List<Vector2Int> frontier = new List<Vector2Int>();
        
        if (_terrainMap.HasResource(center.x, center.y))
        {
            meadow.Add(center);
            frontier.Add(center);
        }
        
        while (meadow.Count < targetSize && frontier.Count > 0)
        {
            frontier = frontier
                .OrderBy(c => Vector2Int.Distance(center, c))
                .ToList();
            
            Vector2Int current = frontier[0];
            frontier.RemoveAt(0);
            
            foreach (Vector2Int neighbor in GetNeighbours(current))
            {
                if (meadow.Count >= targetSize) break;
                if (!_terrainMap.InBorders(neighbor.x, neighbor.y)) continue;
                if (meadow.Contains(neighbor)) continue;
                if (frontier.Contains(neighbor)) continue;
                if (!_terrainMap.HasResource(neighbor.x, neighbor.y)) continue;
                
                meadow.Add(neighbor);
                frontier.Add(neighbor);
            }
        }
        
        foreach (Vector2Int cell in meadow)
        {
            _terrainMap.SetResource(cell.x, cell.y, Resource.None);
            _meadowMap.SetCell(cell.x, cell.y);
        }
    }

    private Vector2Int RandomPos()
    {
        int iterations = 100;
        while(iterations > 0)
        {
            Vector2Int randomPos = new Vector2Int(Random.Range(0, _meadowMap.Width), Random.Range(0, _meadowMap.Height));
            if(!_meadowMap.IsTaken(randomPos.x, randomPos.y))
            {
                return randomPos;
            }

            iterations--;
        }

        return Vector2Int.zero;
    }

    protected List<Vector2Int> GetNeighbours(Vector2Int pos)
    {
        List<Vector2Int> allNeighbours = new List<Vector2Int>();

        allNeighbours.Add(new Vector2Int(pos.x-1, pos.y));
        allNeighbours.Add(new Vector2Int(pos.x+1, pos.y));
        allNeighbours.Add(new Vector2Int(pos.x, pos.y-1));
        allNeighbours.Add(new Vector2Int(pos.x, pos.y+1));

        return allNeighbours;
    }
}
