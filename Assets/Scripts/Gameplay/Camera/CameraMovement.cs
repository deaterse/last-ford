using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private float _scrollSpeed = 3f;

    private Vector2 _startPos;
    private Vector2 _endPos;

    private Camera _camera;

    private void Start()
    {
        _camera = GetComponent<Camera>();
    }

    private void Update()
    {
        //Scrolling
        float _mouseScroll = Input.GetAxis("Mouse ScrollWheel");
        _camera.orthographicSize -= _mouseScroll * _scrollSpeed;

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

    private Vector2 GetMousePosition()
    {
        Vector2 mousePos = Input.mousePosition;
        Vector2 worldPos = Camera.main.ScreenToWorldPoint(mousePos);

        return worldPos;
    }
}
