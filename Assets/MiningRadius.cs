using UnityEngine;

public class MiningRadius : MonoBehaviour
{
    [SerializeField] protected MiningRadiusVisualize _miningRadiusVis;

    public void SetSize(int size)
    {
        _miningRadiusVis.SetSize(size);
    }

    public void OffVisualize()
    {
        _miningRadiusVis.gameObject.SetActive(false);
    }
}
