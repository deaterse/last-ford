using UnityEngine;
using System.Collections.Generic;

public class GenerateBase
{
    private TerrainMap _terrainMap;
    private int _baseRadius = 10;

    public GenerateBase(TerrainMap terrainMap)
    {
        _terrainMap = terrainMap;
    }

    public Vector2Int Generate()
    {
        int width = _terrainMap.Width;
        int height = _terrainMap.Height;

        Vector2Int randomPos = GetRandomPos();

        Queue<Vector2Int> allPos = new();
        allPos.Enqueue(randomPos);

        List<Vector2Int> allPlaced = new();

        int needToPlace = 2 * (_baseRadius^2) + 2 * _baseRadius + 1;
        int placed = 0;
        while(allPlaced.Count < needToPlace)
        {
            Vector2Int current = allPos.Dequeue();

            if(!allPlaced.Contains(current))
            {
                allPlaced.Add(current);
                _terrainMap.SetResource(current.x, current.y, Resource.None);
            }

            List<Vector2Int> allNeighbours = GetNeighbours(current);
            foreach(Vector2Int n in allNeighbours)
            {
                if(!allPlaced.Contains(n))
                {
                    allPlaced.Add(n);
                    _terrainMap.SetResource(n.x, n.y, Resource.None);
                }

                allPos.Enqueue(n);
            }
        }

        return randomPos;
    }

    private Vector2Int GetRandomPos()
    {
        int width = _terrainMap.Width;
        int height = _terrainMap.Height;

        Vector2Int randomPos = new Vector2Int(Random.Range(0, width), Random.Range(0, height));
        while(!_terrainMap.AvaliableForCastle(randomPos.x, randomPos.y) && RadiusInBorders(randomPos))
        {
            randomPos = new Vector2Int(Random.Range(0, width), Random.Range(0, height));
        }

        return randomPos;
    }

    private bool RadiusInBorders(Vector2Int pos)
    {
        return _terrainMap.InBorders(pos.x + _baseRadius, pos.y) && 
        _terrainMap.InBorders(pos.x - _baseRadius, pos.y) && 
        _terrainMap.InBorders(pos.x, pos.y + _baseRadius) && 
        _terrainMap.InBorders(pos.x, pos.y - _baseRadius);
    }

    private List<Vector2Int> GetNeighbours(Vector2Int pos)
    {
        List<Vector2Int> allNeighbours = new List<Vector2Int>();

        allNeighbours.Add(new Vector2Int(pos.x-1, pos.y));
        allNeighbours.Add(new Vector2Int(pos.x+1, pos.y));
        allNeighbours.Add(new Vector2Int(pos.x, pos.y-1));
        allNeighbours.Add(new Vector2Int(pos.x, pos.y+1));

        return allNeighbours;
    }
}
