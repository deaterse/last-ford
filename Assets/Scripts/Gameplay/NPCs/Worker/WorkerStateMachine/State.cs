using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class State: MonoBehaviour
{
    public virtual void SetData(object data) { }
    public virtual void ClearData() { }

    public abstract void Enter();
    public abstract void OnUpdate();
    public abstract void Exit();

    protected Transform Transform =>  GetComponent<Worker>().transform;
}