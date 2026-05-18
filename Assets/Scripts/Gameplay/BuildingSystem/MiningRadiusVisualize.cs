using UnityEngine;

public class MiningRadiusVisualize : MonoBehaviour
{
    public void SetSize(int size)
    {
        transform.localScale = new Vector3(size*2 + 0.5f, size*2 + 0.5f, 1);
    }
}
