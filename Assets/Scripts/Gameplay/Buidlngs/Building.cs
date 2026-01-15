using UnityEngine;

public class Building : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;

    public void ChangeColor(Color color)
    {
        _spriteRenderer.color = color;
    }
}
