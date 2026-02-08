using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections;
using System.Collections.Generic;

public class WorkingState: State
{
    private float _workingTime;
    private System.Action _onReachedCallback;

    public override void SetData(object data)
    {
        if (data is WorkingData workingData)
        {
            _workingTime = workingData.Time;
            _onReachedCallback = workingData.OnReached;
        }
    }

    public override void Enter()
    {
        StartCoroutine(StartWork());
    }

    public override void OnUpdate()
    {
    
    }

    public override void Exit()
    {
        StopAllCoroutines();
    }

    private IEnumerator StartWork()
    {
        yield return new WaitForSeconds(_workingTime);
    
        _onReachedCallback?.Invoke();
    }
}