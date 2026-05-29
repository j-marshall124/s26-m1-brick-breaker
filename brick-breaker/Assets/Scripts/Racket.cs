using UnityEngine;

public class Racket : MonoBehaviour
{
    // Racket rigidbody, we will control this to move
    public Rigidbody2D rb2d;
    // How fast we move horizontally in pixels per second
    public float moveSpeed = 100; // in px

    // Racket pixel sizing w/ collider
    public SpriteRenderer racketLeft;
    public SpriteRenderer racketCentre;
    public SpriteRenderer racketRight;
    public CapsuleCollider2D capsuleCollider2D;

    void AutoSizeRacket()
    {
        // Change size of collider
        // Get size of the paddle segments
        float width = racketLeft.bounds.size.x + racketRight.bounds.size.x + racketCentre.bounds.size.x;
        float height = racketCentre.bounds.size.y;
        capsuleCollider2D.size = new Vector2(width, height);

        // Set position of left and right segments
        float leftOffsetX = racketCentre.bounds.extents.x + racketLeft.bounds.extents.x;
        float rightOffsetX = racketCentre.bounds.extents.x + racketRight.bounds.extents.x;
        racketLeft.transform.position = racketCentre.transform.position + new Vector3(-leftOffsetX, 0, 0);
        racketRight.transform.position = racketCentre.transform.position + new Vector3(+rightOffsetX, 0, 0);
    }

    // FixedUpdate is called 50 times per second
    void FixedUpdate()
    {
        AutoSizeRacket();

        // Move player horizontally
        float moveX = Input.GetAxis("Horizontal") * moveSpeed;
        rb2d.linearVelocityX = moveX;
    }

    void OnValidate()
    {
        // Get rigidbodt automatically if it is null and attached to this object
        if (rb2d == null)
            rb2d = GetComponent<Rigidbody2D>();
        if (capsuleCollider2D == null)
            capsuleCollider2D = GetComponent<CapsuleCollider2D>();
    }
}
