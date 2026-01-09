using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private Vector2 _startPos;
    private Vector2 _endPos;

    void Update()
    {
        //Scrolling
        float _mouseScroll = Input.GetAxis("Mouse ScrollWheel");
        Camera.main.orthographicSize -= _mouseScroll * 3;

        //Camera Movement
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 worldPos = GetMousePosition();
            _startPos = worldPos;
        }
        if (Input.GetMouseButton(0))
        {
            Vector2 currentPos = GetMousePosition();

            Vector2 difference = _startPos - currentPos;

            transform.Translate(difference, Space.World);
        }
    }

    public Vector2 GetMousePosition()
    {
        Vector2 mousePos = Input.mousePosition;
        Vector2 worldPos = Camera.main.ScreenToWorldPoint(mousePos);

        return worldPos;
    }
}
