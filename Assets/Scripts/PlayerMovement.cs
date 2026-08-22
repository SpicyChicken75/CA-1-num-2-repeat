using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    // Floats set
    public float speed = 5f;
    public float jumpHeight = 5f;
    public float wallJumpForce = 7f;
    public float dashSpeed = 12f;     // how fast the dash is
    public float dashTime = 0.2f;     // how long the dash lasts


    //Booleans set
    private bool isJumping = false;
    private bool isTouchingWall = false;
    private bool isDashing = false;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Check for shift
        if (Input.GetKeyDown(KeyCode.LeftShift) && !isDashing)
        {
            StartCoroutine(Dash());
        }

        // Skip normal movement while dashing
        if (isDashing)
            return;

        // Movement
        float move = Input.GetAxis("Horizontal");
        Vector2 position = transform.position;
        position.x += speed * Time.deltaTime * move;
        transform.position = position;

        // Jump method
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!isJumping)
            {
                Jump(Vector2.up); // normal jump
            }

            else if (isTouchingWall)
            {
                // jump off wall in opposite direction
                float direction = move >= 0 ? -1 : 1;
                rb.linearVelocity = Vector2.zero;
                rb.AddForce(new Vector2(direction, 1) * wallJumpForce, ForceMode2D.Impulse);
            }
        }
    }

    // Dash method
    private System.Collections.IEnumerator Dash()
    {
        isDashing = true;

        float moveDir = Input.GetAxisRaw("Horizontal");
        if (moveDir == 0)
            moveDir = transform.localScale.x > 0 ? 1 : -1; // dash toward facing side

        Vector2 dashVelocity = new Vector2(moveDir * dashSpeed, 0);
        rb.linearVelocity = dashVelocity;

        yield return new WaitForSeconds(dashTime); // Wait faor the dash timer

        isDashing = false;
    }

    void Jump(Vector2 direction)
    {
        isJumping = true;
        rb.linearVelocity = Vector2.zero;
        float jumpForce = Mathf.Sqrt(-2f * Physics2D.gravity.y * jumpHeight);
        rb.AddForce(direction * jumpForce, ForceMode2D.Impulse);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Touching ground
        if (collision.gameObject.CompareTag("TilemapFlooring"))
        {
            isJumping = false;
        }

        // Touching wall
        if (collision.gameObject.CompareTag("Wall"))
        {
            isTouchingWall = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            isTouchingWall = false;
        }
    }
}
