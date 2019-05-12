using UnityEngine;

public class SmartCamera : MonoBehaviour
{
    [SerializeField] private GameObject _player = null;

    private Camera _camera = null;

    private int _currentGridX = 0;
    private int _currentGridY = 0;
    private float _halfScreenHeight = 0.0f;
    private float _halfScreenWidth = 0.0f;
    private float _screenHeight = 0.0f;
    private float _screenWidth = 0.0f;

    private Vector3 _target = new Vector3();

    protected void Start()
    {
        //Initialize the player object
        _player = GameObject.Find("Player");
        //Initialize the camera component
        _camera = GetComponent<Camera>();

        //Sets the half of the current screen height
        _halfScreenHeight = _camera.orthographicSize;
        //Sets the half of the current screen width, half the screen * camera aspect
        _halfScreenWidth = _halfScreenHeight * _camera.aspect;

        //Sets the screen height
        _screenHeight = _halfScreenHeight * 2.0f;
        //sets the screen width
        _screenWidth = _halfScreenWidth * 2.0f;

        //Sets the target
        _target = transform.position;
    }

    protected void Update()
    {
        //Sets the player position
        Vector3 playerPosition = _player.transform.position;

        // Start at (0, 0)
        int gridX = 0;
        int gridY = 0;

        // Horizontal movement
        if (playerPosition.x < 0)
        {
            gridX = (int)((playerPosition.x - _halfScreenWidth) / _screenWidth);
        }
        else if (playerPosition.x > 0)
        {
            gridX = (int)((playerPosition.x + _halfScreenWidth) / _screenWidth);
        }

        // Vertical movement
        if (playerPosition.y < 0)
        {
            gridY = (int)((playerPosition.y - _halfScreenHeight) / _screenHeight);
        }
        else if (playerPosition.y > 0)
        {
            gridY = (int)((playerPosition.y + _halfScreenHeight) / _screenHeight);
        }

        // Check if we're somewhere else now
        if (gridX != _currentGridX || gridY != _currentGridY)
        {
            _currentGridX = gridX;
            _currentGridY = gridY;

            // So we keep the z value
            Vector3 newTarget = transform.position;
            newTarget.x = _currentGridX * _screenWidth;
            newTarget.y = _currentGridY * _screenHeight;
            _target = newTarget;
        }

        transform.position -= (transform.position - _target) / 4.0f;
    }
}