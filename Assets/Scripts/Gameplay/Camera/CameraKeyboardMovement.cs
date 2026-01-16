using UnityEngine;

public class CameraKeyboardMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _scrollSpeed;

    [Header("Scroll Borders")]
    [SerializeField] private float _minOrtoSize;
    [SerializeField] private float _maxOrtoSize;
    
    private Camera _camera;

    private void Start()
    {
        _camera = GetComponent<Camera>();
    }

    private void OnEnable()
    {
        GameEvents.OnInputCameraMovement += CameraMove;
        GameEvents.OnInputCameraZoom += CameraZoom;
    }

    private void OnDisable()
    {
        GameEvents.OnInputCameraMovement -= CameraMove;
        GameEvents.OnInputCameraZoom -= CameraZoom;
    }

    private void CameraMove(Vector2 movementVector)
    {
        transform.Translate(new Vector3(movementVector.x, movementVector.y, 0) * _moveSpeed * Time.deltaTime);
    }

    private void CameraZoom(float zoomStrength)
    {
        _camera.orthographicSize -= zoomStrength * _scrollSpeed;
        SizeInBorders();
    }

    private void SizeInBorders()
    {
        if(_camera.orthographicSize > _maxOrtoSize)
        {
            _camera.orthographicSize = _maxOrtoSize;
        }
        else if(_camera.orthographicSize < _minOrtoSize)
        {
            _camera.orthographicSize = _minOrtoSize;
        }
    }
}
