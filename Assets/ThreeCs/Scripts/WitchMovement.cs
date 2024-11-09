using UnityEngine;

public class WitchMovement : MonoBehaviour
{
    public float moveSpeed = 5f;

    private Rigidbody2D rb;
    private Animator animator;
    private Vector2 movement;
    private bool isFacingLeft = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Capture input from the player
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        animator.SetBool("IsMoving", movement != Vector2.zero);
        if (movement.x != 0)
        {
            isFacingLeft = movement.x < 0;
        }
        animator.SetBool("IsFacingLeft", isFacingLeft);
    }

    void FixedUpdate()
    {
        // Apply movement to the player's rigidbody
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
