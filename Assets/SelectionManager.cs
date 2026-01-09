using UnityEngine;
using System;

public class SelectionManager : MonoBehaviour
{
    public static SelectionManager Instance { get; private set; }
    
    [SerializeField] private LayerMask selectableLayer;
    

    public Action<GameObject> OnObjectSelected;
    public Action<GameObject> OnObjectDeselected;
    
    private GameObject currentSelection;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(1) && currentSelection == null)
        {
            HandleClick();
        }
        else if(Input.GetMouseButtonDown(0))
        {
            ClearSelection();
        }
    }

    private void HandleClick()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if(Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, selectableLayer))
        {
            RaycastHit2D hit2D = Physics2D.Raycast(ray.origin, ray.direction);
            if(hit2D.collider != null)
            {
                SelectObject(hit2D.collider.gameObject);
            }
        }
    }

    private void SelectObject(GameObject obj)
    {
        if (currentSelection == obj) return;

        if (currentSelection != null)
        {
            OnObjectDeselected?.Invoke(currentSelection);
        }

        currentSelection = obj;
        OnObjectSelected?.Invoke(obj);
    }

    public void ClearSelection()
    {
        if (currentSelection != null)
        {
            OnObjectDeselected?.Invoke(currentSelection);
            currentSelection = null;
        }
    }
}
