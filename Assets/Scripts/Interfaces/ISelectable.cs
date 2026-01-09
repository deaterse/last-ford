using UnityEngine;

public interface ISelectable
{
    GameObject gameObject { get; }
    Transform transform { get; }
    bool IsSelectable { get; }
    void OnSelected(GameObject obj);
    void OnDeselected(GameObject obj);
    void OnHoverStart(GameObject obj);
    void OnHoverEnd(GameObject obj);
}

