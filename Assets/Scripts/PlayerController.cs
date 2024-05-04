using UnityEngine;


[RequireComponent (typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private MobileJoystick playerJoystick;

    [Header("Settings")]
    [SerializeField] private float moveSpeed;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rb.velocity = playerJoystick.GetMoveVector() * moveSpeed * Time.deltaTime;
    }
}
