using UnityEngine;

public class SceneCleaner : MonoBehaviour
{
    public void Init()
    {
        GameEvents.ClearAllEvents();
        Debug.Log("GameEvents is cleared");
    }
}
