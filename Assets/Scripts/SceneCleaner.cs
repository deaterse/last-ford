using UnityEngine;

public class SceneCleaner : MonoBehaviour
{
    private void Awake()
    {
        GameEvents.ClearAllEvents();
        Debug.Log("GameEvents is cleared");
    }
}
