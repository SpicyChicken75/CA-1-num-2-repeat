using UnityEngine;

public class _animator : MonoBehaviour
{
    public float speed = 5f;

    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private float moveX;

    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>(); // get the sprite
    }

    void Update()
    {
        moveX = Input.GetAxisRaw("Horizontal");

        // Move character
        Vector2 position = transform.position;
        position.x += speed * Time.deltaTime * moveX;
        transform.position = position;

        // Set animator parameter (for run/idle)
        animator.SetFloat("Move X", Mathf.Abs(moveX));  //Makes number positive

        // Flip sprite based on direction
        if (moveX < 0)
            spriteRenderer.flipX = true;   // facing left
        else if (moveX > 0)
            spriteRenderer.flipX = false;  // facing right
    }
}
