using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private int count;

    private Vector2 _movementInput;

    public float speed = 10f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
    }

    void OnMove(InputValue movementValue)
    {
        _movementInput = movementValue.Get<Vector2>();
    }

    void FixedUpdate()
    {
        if (_movementInput == Vector2.zero) return;

        // Получаем направление камеры
        Vector3 cameraForward = Camera.main.transform.forward;
        Vector3 cameraRight = Camera.main.transform.right;

        // Обнуляем вертикальную составляющую (чтобы не двигаться вверх/вниз)
        cameraForward.y = 0f;
        cameraRight.y = 0f;

        // Нормализуем (чтобы длина вектора была = 1)
        cameraForward.Normalize();
        cameraRight.Normalize();

        // Направление относительно камеры
        Vector3 moveDirection = cameraForward * _movementInput.y + cameraRight * _movementInput.x;

        // Применяем силу
        rb.AddForce(moveDirection * speed);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            GameEvents.RaisePickUpCollected(count);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
            GameEvents.RaiseLose();
        }
    }
}