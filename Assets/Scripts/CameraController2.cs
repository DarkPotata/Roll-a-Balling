using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController2 : MonoBehaviour
{
    [Header("Цель")]
    public Transform target;

    [Header("Расстояние")]
    public float distance = 8f;

    [Header("Чувствительность мыши")]
    public float mouseSensitivity = 3f;

    [Header("Ограничения по вертикали")]
    public float minVerticalAngle = 10f;
    public float maxVerticalAngle = 60f;

    [Header("Зум")]
    public float minDistance = 3f;
    public float maxDistance = 15f;
    public float zoomSpeed = 2f;

    private float _currentHorizontalAngle = 0f;
    private float _currentVerticalAngle = 30f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (target == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
                target = player.transform;
        }
    }

    void OnEnable()
    {
        GameEvents.OnSensitivityChanged += OnSensitivityChanged;
    }

    void OnDisable()
    {
        GameEvents.OnSensitivityChanged -= OnSensitivityChanged;
    }

    void OnSensitivityChanged(float newSensitivity)
    {
        mouseSensitivity = newSensitivity;
    }

    void Update()
    {
        if (target == null) return;

        // Ввод мыши
        Vector2 mouseDelta = Mouse.current.delta.ReadValue();
        _currentHorizontalAngle += mouseDelta.x * mouseSensitivity;
        _currentVerticalAngle -= mouseDelta.y * mouseSensitivity;
        _currentVerticalAngle = Mathf.Clamp(_currentVerticalAngle, minVerticalAngle, maxVerticalAngle);

        // Зум
        float scroll = Mouse.current.scroll.ReadValue().y;
        distance -= scroll * zoomSpeed;
        distance = Mathf.Clamp(distance, minDistance, maxDistance);
    }

    void LateUpdate()
    {
        if (target == null) return;

        // Простая позиция — без сглаживания, без SmoothDamp
        Vector3 direction = new Vector3(0, 0, -distance);
        Quaternion rotation = Quaternion.Euler(_currentVerticalAngle, _currentHorizontalAngle, 0);
        transform.position = target.position + rotation * direction;

        // Просто смотрим на цель
        transform.LookAt(target.position);
    }

    void OnDestroy()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}