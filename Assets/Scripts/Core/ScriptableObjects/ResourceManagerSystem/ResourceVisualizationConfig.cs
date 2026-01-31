using UnityEngine;

[CreateAssetMenu(fileName = "ResourceVisualizationConfig", menuName = "Scriptable Objects/Resource System/ResourceVisualization Config")]
public class ResourceVisualizationConfig : ScriptableObject
{
    [SerializeField] private string _displayedName;
    [SerializeField] private ResourceType _resourceType;
    [SerializeField] private Sprite _resourceSprite;

    public string DisplayedName => _displayedName;
    public ResourceType resourceType => _resourceType;
    public Sprite ResourceSprite => _resourceSprite;
}
