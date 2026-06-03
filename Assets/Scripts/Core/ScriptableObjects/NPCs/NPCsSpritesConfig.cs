using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "NPCsConfig", menuName = "Scriptable Objects/NPCs/NPCsConfig")]
public class NPCsConfig : ScriptableObject
{
    [SerializeField] private List<Sprite> _villagersTypes = new();
    [SerializeField] private Vector2 _avaliableSpeed;

    public float MinSpeed => _avaliableSpeed.x;
    public float MaxSpeed => _avaliableSpeed.y;
    public List<Sprite> VillagersTypes => _villagersTypes;
}
