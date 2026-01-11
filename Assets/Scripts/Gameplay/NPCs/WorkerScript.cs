using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.EventSystems;

public class WorkerScript : MonoBehaviour
{
    [SerializeField] private Tilemap _terrainMap;
    [SerializeField] private float _moveSpeed;

    [SerializeField] private GameObject _pointPrefab;
    private GameObject _currentPointObj;

    private void Update()
    {
        if(Input.GetMouseButtonDown(1) && !BuildSystem.Instance.isBuilding && GetComponent<NPController>().isChoosen)
        {
            ClearPoint();

            StopAllCoroutines();

            Vector3 mousePos = Input.mousePosition;
            Vector3 mousePosWorld = Camera.main.ScreenToWorldPoint(mousePos);

            Vector3Int cellMousePos = _terrainMap.WorldToCell(mousePosWorld);
            Vector3 forPoint = new Vector3(cellMousePos.x + 0.5f, cellMousePos.y + 0.5f, 0);

            MoveTo(cellMousePos);
        }
    }

    public void SpawnPoint()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 mousePosWorld = Camera.main.ScreenToWorldPoint(mousePos);

        Vector3Int cellMousePos = _terrainMap.WorldToCell(mousePosWorld);
        Vector3 forPoint = new Vector3(cellMousePos.x + 0.5f, cellMousePos.y + 0.5f, 0);

        _currentPointObj = Instantiate(_pointPrefab, forPoint, Quaternion.identity);
    }

    public void ClearPoint()
    {
        Destroy(_currentPointObj);
        _currentPointObj = null;
    }
    
    public void MoveTo(Vector3Int targetCell)
    {
        Vector3Int startCell = _terrainMap.WorldToCell(transform.position);
        List<Vector3Int> path = Pathfinder.Instance.FindPath(startCell, targetCell);
        
        if (path != null)
        {
            SpawnPoint();
            GetComponent<NPController>().ChangeToMove();

            StartCoroutine(FollowPath(path));
        }
    }
    
    private IEnumerator FollowPath(List<Vector3Int> path)
    {
        foreach (Vector3Int cell in path)
        {
            Vector3 worldPos = _terrainMap.GetCellCenterWorld(cell);
            while (Vector3.Distance(transform.position, worldPos) > 0.1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, worldPos, _moveSpeed * Time.deltaTime);
                yield return null;
            }
        }

        ClearPoint();

        GetComponent<NPController>().ChangeToIdle();
    }
}
