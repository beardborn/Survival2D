using UnityEngine;


[RequireComponent (typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private MobileJoystick playerJoystick;
    private Vector3 keyboardMovement = new Vector3();


    [Header("Settings")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float keyboardMoveSpeed;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        keyboardMovement.x = Input.GetAxisRaw("Horizontal");
        keyboardMovement.y = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        if (playerJoystick.GetMoveVector() != Vector3.zero)
        {
            rb.velocity = playerJoystick.GetMoveVector() * moveSpeed * Time.deltaTime;
        }
        else { rb.velocity = keyboardMovement.normalized * keyboardMoveSpeed * Time.deltaTime; }
    }
}
