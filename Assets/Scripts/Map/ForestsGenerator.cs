using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

public class ForestsGenerator
{
    private TerrainMap _terrainMap;

    public ForestsGenerator(TerrainMap terrainMap)
    {
        _terrainMap = terrainMap;
    }

    public void GenerateForests()
    {
        int forestsCount = Random.Range(3, 5);

        float floatedForestSize = (0.8f*(_terrainMap.Width * _terrainMap.Height)) / forestsCount;
        int forestSize = (int) floatedForestSize;

        for(int i = 0; i < forestsCount; i++)
        {
            GenerateSizedForest(forestSize);
        }
    }
    
    private Vector3Int RandomPosMap()
    {
        return new Vector3Int(Random.Range(0, _terrainMap.Width - 1), Random.Range(0, _terrainMap.Height - 1), 0);
    }

    private void GenerateSizedForest(int forestSize)
    {
        Vector3Int current = RandomPosMap();

        int placedCount = 0;
        int iterations = 0;

        while (placedCount < forestSize && iterations < forestSize * 5)
        {
            iterations++;

            if (IsEligiableTile(current) && !HasResource(current))
            {
                _terrainMap.TerrainData[current.x, current.y].Resource = new Resource
                    (
                        ResourceType.Wood,
                        100
                    );

                placedCount++;
            }
            
            List<Vector3Int> allNeighbours = GetNeighbours(current);
            
            for (int i = 0; i < allNeighbours.Count; i++)
            {
                int randomIndex = Random.Range(i, allNeighbours.Count);
                Vector3Int temp = allNeighbours[i];
                allNeighbours[i] = allNeighbours[randomIndex];
                allNeighbours[randomIndex] = temp;
            }
            
            foreach (Vector3Int neighbour in allNeighbours)
            {
                if (placedCount >= forestSize) break;

                if (IsEligiableTile(neighbour))
                {
                    if(!HasResource(neighbour))
                    {
                        _terrainMap.TerrainData[neighbour.x, neighbour.y].Resource = new Resource
                            (
                                ResourceType.Wood,
                                100
                            );

                        placedCount++;
                    }
                }
            }
            
            Vector3Int randomN = allNeighbours[Random.Range(0, allNeighbours.Count)];
            current = randomN;
        }
    }
    private bool IsEligiableTile(Vector3Int pos) 
    {
        if(pos.x >= _terrainMap.Width || pos.y >= _terrainMap.Height || pos.x < 0 || pos.y < 0)
        {
            return false;
        }

        return _terrainMap.IsGrass(pos.x, pos.y);
    }

    private bool HasResource(Vector3Int pos)
    {
        return _terrainMap.HasResource(pos.x, pos.y);
    }

    private List<Vector3Int> GetNeighbours(Vector3Int pos)
    {
        List<Vector3Int> allNeighbours = new List<Vector3Int>();

        allNeighbours.Add(new Vector3Int(pos.x-1, pos.y, 0));
        allNeighbours.Add(new Vector3Int(pos.x+1, pos.y, 0));
        allNeighbours.Add(new Vector3Int(pos.x, pos.y-1, 0));
        allNeighbours.Add(new Vector3Int(pos.x, pos.y+1, 0));

        return allNeighbours;
    }
}
