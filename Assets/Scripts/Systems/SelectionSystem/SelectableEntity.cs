using UnityEngine;

public abstract class SelectableEntity : MonoBehaviour, ISelectable
{
    [Header("Selection Settings")]
    [SerializeField] protected SelectableType type;
    [SerializeField] protected bool isSelectable = true;
    [SerializeField] protected GameObject selectionVisual;
    [SerializeField] protected Color selectionColor = Color.green;
    
    protected bool isSelected = false;
    protected bool isHovered = false;
    
    // Реализация интерфейса
    public bool IsSelectable => isSelectable;

    public virtual void Start()
    {
        SelectionManager.Instance.OnObjectSelected += OnSelected;
        SelectionManager.Instance.OnObjectDeselected += OnDeselected;
    }
    
    public virtual void OnSelected(GameObject obj)
    {
        if(obj == gameObject)
        {
            if (!isSelectable) return;
                
            isSelected = true;
        }
    }
    
    public virtual void OnDeselected(GameObject obj)
    {
        if(obj == gameObject)
        {
            isSelected = false;
        }
    }
    
    public virtual void OnHoverStart(GameObject obj)
    {
        if (isSelected || !isSelectable) return;
        isHovered = true;
    }
    
    public virtual void OnHoverEnd(GameObject obj)
    {
        isHovered = false;
    }
    
    public virtual void SetSelectable(bool selectable)
    {
        isSelectable = selectable;
        if (!selectable && isSelected) OnDeselected(gameObject);
    }
}