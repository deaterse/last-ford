using UnityEngine;
using System.Collections.Generic;

public class ResidentHouse : MonoBehaviour
{
    [SerializeField] private int _residentsCount;
    [SerializeField] private List<WorkerScript> _allResidents;

    private void Start()
    {
        // Вынести куда нибудь
        _residentsCount = Random.Range(3, 5);

        _allResidents.Clear();
    }
}
