using UnityEngine;
using System.Collections.Generic;

public class StonesGenerator: ResourceGenerator
{
    public StonesGenerator(TerrainMap terrainMap, ResourceSettings resourceSettings) : base(terrainMap, resourceSettings) { }

    public override void Generate()
    {
        int stonesCount = Random.Range(_resourceSettings.MinClusterCount, _resourceSettings.MaxClusterCount);
        int stoneSize = GetClusterSize(stonesCount);

        int allStonesCount = 0;

        for(int i = 0; i < stonesCount; i++)
        {
            allStonesCount += GenerateStones(stoneSize);
        }

        GameEvents.InvokeOnStonesGenerated(allStonesCount);
    }

    private int GenerateStones(int stoneSize)
    {
        Vector3Int current = RandomPosMap();

        int placedCount = 0;
        int iterations = 0;

        TileData stoneTile = new TileData(TerrainType.Stone,
            new Resource(
                ResourceType.Stone, 
                100
        ));

        if(current != Vector3Int.zero)
        {
            while (placedCount < stoneSize && iterations < stoneSize*5)
            {
                iterations++;

                if (IsEligibleTile(current) && !HasResource(current))
                {
                    _terrainMap.SetTile(current.x, current.y, stoneTile);

                    placedCount++;
                }
                
                List<Vector3Int> allNeighbours = GetNeighbours(current);

                if (allNeighbours.Count == 0) break;

                for (int i = 0; i < allNeighbours.Count; i++)
                {
                    int randomIndex = Random.Range(i, allNeighbours.Count);
                    Vector3Int temp = allNeighbours[i];
                    allNeighbours[i] = allNeighbours[randomIndex];
                    allNeighbours[randomIndex] = temp;
                }
                
                foreach (Vector3Int neighbour in allNeighbours)
                {
                    if (placedCount >= stoneSize) break;
                    
                    if (IsEligibleTile(neighbour))
                    {
                        if(!HasResource(neighbour))
                        {
                            _terrainMap.SetTile(neighbour.x, neighbour.y, stoneTile);

                            placedCount++;
                        }
                    }
                }
                
                Vector3Int randomN = allNeighbours[Random.Range(0, allNeighbours.Count)];
                current = randomN;
            }
        }
        else
        {
            Debug.LogWarning("Didnt found any RandomPos for StoneGenerator!");
        }

        Debug.Log($"Successfully placed Stone: {placedCount} / {stoneSize}");

        return placedCount;
    }
}
