using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections;
using System.Collections.Generic;
public class MovingState : State
{
    private Vector3Int _target;
    private float _moveSpeed = 5f;

    private System.Action _onReachedCallback;

    public override void SetData(object data)
    {
        if (data is MovingData movingData)
        {
            _target = movingData.Target;
            _onReachedCallback = movingData.OnReached;
        }
    }

    public override void Enter()
    {
        MoveTo(_target);
    }
    
    public override void OnUpdate()
    {

    }

    public void MoveTo(Vector3Int targetCell)
    {
        Vector3Int startCell = ServiceLocator.GetService<Pathfinder>().WorldToCell(transform.position);
        List<Vector3Int> path = ServiceLocator.GetService<Pathfinder>().FindPath(startCell, targetCell);

        Debug.Log(targetCell);
        
        if (path != null)
        {
            StartCoroutine(FollowPath(path));
        }
        else
        {
            Debug.Log("i cant go there");
            GetComponent<Worker>().ChangeState<IdleState>();
        }
    }
    
    private IEnumerator FollowPath(List<Vector3Int> path)
    {
        foreach (Vector3Int cell in path)
        {
            Vector3 worldPos = ServiceLocator.GetService<Pathfinder>().GetCellCenterWorld(cell);
            while (Vector3.Distance(transform.position, worldPos) > 0.1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, worldPos, _moveSpeed * Time.deltaTime);
                yield return null;
            }
        }


        _onReachedCallback?.Invoke();
    }

    public override void Exit()
    {
        StopAllCoroutines();
    }
}