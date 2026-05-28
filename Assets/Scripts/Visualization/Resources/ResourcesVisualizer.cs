using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;
public class ResourcesVisualizer : MonoBehaviour
{

    public void Visualize(TerrainMap _terrainMap, ResourcesSubtypeConfig _resourcesSubtypeConfig)
    {
        int width = _terrainMap.Width;
        int height = _terrainMap.Height;

        GameObject[,] resourcesObjs = new GameObject[width, height];

        for(int x = 0; x < width; x++)
        {
            for(int y = 0; y < height; y++)
            {
                ResourceType currentResourceType = _terrainMap.TerrainData[x, y].Resource.Type;
                int currentSubType = _terrainMap.TerrainData[x, y].Resource.SubType;

                ResourceTypesConfig config = _resourcesSubtypeConfig.GetConfigByType(currentResourceType);
        
                if (config != null)
                {
                    List<GameObject> resourceTiles = _resourcesSubtypeConfig.GetTypeFromResourceType(currentResourceType).GetObjByIndex(currentSubType);

                    Vector3 pos = new Vector3(x + 0.5f, y + 0.5f, 0);
                    GameObject newRes = Instantiate(RandomTile(resourceTiles), pos, Quaternion.identity);

                    resourcesObjs[x, y] = newRes;
                }
            }
        }

        ServiceLocator.GetService<EventBus>().Invoke<OnResourcesVisualized>(new OnResourcesVisualized(resourcesObjs));
    }

    private GameObject RandomTile(List<GameObject> objList)
    {
        return objList[Random.Range(0, objList.Count)];
    }
}
