using UnityEngine;
using DG.Tweening;

public class tryingtween : MonoBehaviour
{
    public Transform target;

    void Update()
    {
        Vector3 direction = target.position - transform.position;
        Debug.Log(direction);
        direction.x = 0;
        direction.y = 0;

        transform.DORotate(direction, 1f, RotateMode.LocalAxisAdd);
    }
}
