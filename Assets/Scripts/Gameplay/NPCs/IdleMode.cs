using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections;
using System.Collections.Generic;

public class IdleMode : MonoBehaviour
{
    /*

      REQUIRED A FULL REFACTORING  

    */




    // [SerializeField] private int radius;
    // [SerializeField] private float _moveSpeed;

    // [SerializeField] private Tilemap _terrainMap;


    // public void StartIdle()
    // {
    //     StartCoroutine(ChoosePoint());    
    // }

    // public void StopIdle()
    // {
    //     StopAllCoroutines();
    // }

    // private IEnumerator ChoosePoint()
    // {
    //     yield return new WaitForSeconds(3);

    //     Vector3 randomPos = transform.position + GetRandomPointOnCircle(radius);
    //     randomPos = new Vector3(randomPos.x, randomPos.y, 0);

    //     Vector3Int gridPos = _terrainMap.WorldToCell(randomPos);

    //     MoveTo(gridPos);
    // }

    // private Vector3 GetRandomPointOnCircle(float radius)
    // {
    //     Vector3 randomDirection = Random.insideUnitCircle.normalized;
    //     return randomDirection * radius;
    // }

    // public void MoveTo(Vector3Int targetCell)
    // {
    //     Vector3Int startCell = _terrainMap.WorldToCell(transform.position);
    //     List<Vector3Int> path = Pathfinder.Instance.FindPath(startCell, targetCell);
        
    //     if (path != null)
    //     {
    //         StartCoroutine(FollowPath(path));
    //     }
    //     else
    //     {
    //        StartCoroutine(ChoosePoint());
    //     }
    // }
    
    // private IEnumerator FollowPath(List<Vector3Int> path)
    // {
    //     foreach (Vector3Int cell in path)
    //     {
    //         Vector3 worldPos = _terrainMap.GetCellCenterWorld(cell);
    //         while (Vector3.Distance(transform.position, worldPos) > 0.1f)
    //         {
    //             transform.position = Vector3.MoveTowards(transform.position, worldPos, _moveSpeed * Time.deltaTime);
    //             yield return null;
    //         }
    //     }

    //     StartCoroutine(ChoosePoint());
    // }

}
