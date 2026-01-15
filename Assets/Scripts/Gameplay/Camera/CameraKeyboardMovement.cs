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

    private void Update()
    {
        //Scrolling
        float _mouseScroll = Input.GetAxis("Mouse ScrollWheel");
        if(_mouseScroll != 0)
        {
            _camera.orthographicSize -= _mouseScroll * _scrollSpeed;
            SizeInBorders();
        }

        //Camera Movement By KeyBoard
        float _horizontalAxis = Input.GetAxis("Horizontal");
        float _verticalAxis = Input.GetAxis("Vertical");

        if(_horizontalAxis != 0 || _verticalAxis != 0)
        {
            transform.Translate(new Vector3(_horizontalAxis, _verticalAxis, 0) * _moveSpeed * Time.deltaTime);
        }
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
