using UnityEngine;

public class BaseVisualize : MonoBehaviour
{
    [SerializeField] private BuildingData _castleData;

    public void BuildBase(Vector2Int pos)
    {
        GameObject buildingObj = Instantiate(_castleData.GetLevel(1).ObjPrefab);
        buildingObj.transform.position = new Vector3(pos.x + 0.5f, pos.y + 0.5f, 0);
            
        ServiceLocator.GetService<BuildingManager>().AddBuilding(_castleData, pos, buildingObj);
    }
}
