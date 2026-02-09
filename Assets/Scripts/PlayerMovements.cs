using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovements : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f; 
    [SerializeField] private float turnSpeed = 180f;

    private Rigidbody2D rb;
    private Vector2 movement;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    public void MovePlayer(InputAction.CallbackContext context)
    {
        movement = context.ReadValue<Vector2>();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        rb.MoveRotation(rb.rotation - turnSpeed * movement.x * Time.fixedDeltaTime);
        rb.MovePosition(rb.position + (Vector2)transform.up * moveSpeed * movement.y * Time.fixedDeltaTime);
            
    }
}
