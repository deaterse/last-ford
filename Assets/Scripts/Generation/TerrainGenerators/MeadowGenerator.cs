using UnityEngine;
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
        // logic using MeadowConfig
        for(int i = 0; i < _meadowsConfig.MeadowsCount; i++)
        {
            GenerateMeadow();
        }
    }

    private void GenerateMeadow()
    {
        int radius = Random.Range(_meadowsConfig.MinMeadowRadius, _meadowsConfig.MaxMeadowRadius);

        Queue<Vector2Int> positionsQueue = new Queue<Vector2Int>();

        Vector2Int currentPos = RandomPos();
        positionsQueue.Enqueue(currentPos);

        for(int i = 0; i < radius * 4; i++)
        {
            List<Vector2Int> allNeighbours = GetNeighbours(currentPos);
            foreach(Vector2Int n in allNeighbours)
            {
                if(_terrainMap.InBorders(n.x, n.y))
                {
                    _terrainMap.SetResource(n.x, n.y, Resource.None);

                    positionsQueue.Enqueue(n);
                }
            }

            currentPos = positionsQueue.Dequeue();
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
