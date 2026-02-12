using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections;
using System.Collections.Generic;

public class IdleState: State
{
    [SerializeField] private int _radius;
    [SerializeField] private float _moveSpeed;

    public override void Enter()
    {
        StartCoroutine(ChoosePoint());
    }

    public override void OnUpdate()
    {
    
    }

    public override void Exit()
    {
        StopAllCoroutines();
    }

    private IEnumerator ChoosePoint()
    {
        yield return new WaitForSeconds(3);

        Vector3 randomPos = transform.position + GetRandomPointOnCircle(_radius);
        randomPos = new Vector3(randomPos.x, randomPos.y, 0);

        Vector3Int gridPos = ServiceLocator.GetService<Pathfinder>().WorldToCell(randomPos);

        MovingData moveData = new MovingData(gridPos, () => {
            GetComponent<Worker>().ChangeState<IdleState>();
        });
    
        GetComponent<Worker>().ChangeState<MovingState>(moveData);
    }

    private Vector3 GetRandomPointOnCircle(float radius)
    {
        Vector3 randomDirection = Random.insideUnitCircle.normalized;
        return randomDirection * radius;
    }
}