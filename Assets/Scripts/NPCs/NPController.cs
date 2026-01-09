using UnityEngine;
using TMPro;

public class NPController : MonoBehaviour
{
    public enum NPCBehaviorMode
    {
        Idle,
        Move
    }
    
    [Header("Behavior Settings")]
    [SerializeField] private NPCBehaviorMode currentMode = NPCBehaviorMode.Idle;

    [SerializeField] private TMP_Text _behaviourText;

    private bool _isIdle = false;
    private bool _isChoosen = false;
    

    private void Update()
    {
        switch (currentMode)
        {
            case NPCBehaviorMode.Idle:
                if(!_isIdle && !_isChoosen)
                {
                    _isIdle = true;
                    
                    HandleIdle();
                }
                break;
                
            case NPCBehaviorMode.Move:
                if(_isChoosen)
                {
                    HandleMove();
                }
                break;
            default:
                Debug.LogWarning("Unknown Behaviour Mode");
                break;
        }
    }

    private void HandleIdle()
    {
        _behaviourText.text = "Idle";

        GetComponent<IdleMode>().StartIdle();
    }

    private void HandleMove()
    {
        _behaviourText.text = "Move";

        _isIdle = false;
        GetComponent<IdleMode>().StopIdle();
    }

    public void ChangeToMove()
    {
        currentMode = NPCBehaviorMode.Move;
    }

    public void ChangeToIdle()
    {
        currentMode = NPCBehaviorMode.Idle;
    }

    public void SetChoosen(bool isChoosen)
    {
        _isChoosen = isChoosen;
    }

    public bool isChoosen => _isChoosen;
}
