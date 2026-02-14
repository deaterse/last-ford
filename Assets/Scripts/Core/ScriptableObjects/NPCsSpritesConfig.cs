using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "NPCsSprites", menuName = "Scriptable Objects/NPCs/NPCsSpritesConfig")]
public class NPCsSpritesConfig : ScriptableObject
{
    [SerializeField] private List<Sprite> _villagersTypes = new();

    public List<Sprite> VillagersTypes => _villagersTypes;
}
