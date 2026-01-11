using UnityEngine;
using System.Collections.Generic;

public class ForestsGenerator: ResourceGenerator
{
    public ForestsGenerator(TerrainMap terrainMap) : base(terrainMap) { }

    public override void Generate()
    {
        int forestsCount = Random.Range(3, 5);

        float floatedForestSize = (0.8f*(_terrainMap.Width * _terrainMap.Height)) / forestsCount;
        int forestSize = (int) floatedForestSize;

        for(int i = 0; i < forestsCount; i++)
        {
            GenerateSizedForest(forestSize);
        }
    }

    private void GenerateSizedForest(int forestSize)
    {
        Vector3Int current = RandomPosMap();

        int placedCount = 0;
        int iterations = 0;

        while (placedCount < forestSize && iterations < forestSize * 5)
        {
            iterations++;

            if (IsEligibleTile(current) && !HasResource(current))
            {
                _terrainMap.SetResource
                (current.x, current.y, 
                new Resource(
                    ResourceType.Wood,
                    100
                ));

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
                if (placedCount >= forestSize) break;

                if (IsEligibleTile(neighbour))
                {
                    if(!HasResource(neighbour))
                    {
                        _terrainMap.SetResource
                        (neighbour.x, neighbour.y, 
                        new Resource(
                            ResourceType.Wood,
                            100
                        ));

                        placedCount++;
                    }
                }
            }
            
            Vector3Int randomN = allNeighbours[Random.Range(0, allNeighbours.Count)];
            current = randomN;
        }

        Debug.Log($"Successfully placed: {placedCount} / {forestSize}");
    }
}
