using UnityEngine;

public class SelectableUnit : SelectableEntity
{
    private NPController _unitController;

    public override void Start()
    {
        base.Start();
        _unitController = GetComponent<NPController>();
    }

    public override void OnSelected(GameObject obj)
    {
        base.OnSelected(obj);

        if(obj == gameObject)
        {
            _unitController.SetChoosen(true);
        }
    }

    public override void OnDeselected(GameObject obj)
    {
        base.OnDeselected(obj);

        if(obj == gameObject)
        {
            _unitController.SetChoosen(false);
        }
    }
}
