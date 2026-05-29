using UnityEngine;

public class Ball : MonoBehaviour
{
    // Controls the ball in play
    public Rigidbody2D rb2d;
    // Speed of ball at launch
    public float launchSpeed = 100; // in px
    //
    public BlockManager blockManager;

    // Used to reset ball position
    private Vector3 startPosition;

    void Start()
    {
        // Record where the ball began in the scene
        startPosition = transform.position;

        // Begin play
        LaunchBall();
    }

    private void OnValidate()
    {
        if (rb2d == null)
            rb2d = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Block") == true)
        {
            // remove block from scene
            Destroy(collision.gameObject);
            // have block manager count that
            blockManager.numberOfBlocksDestroyed += 1;
        }

        if (collision.gameObject.CompareTag("Player") == true)
        {
            CollideWithRacket(collision.collider);
        }
    }

    void LaunchBall()
    {
        // Move ball back to starting position when we ran the game / scene
        rb2d.MovePosition(startPosition);
        // Add velocity to ball: give it a direction, give it a speed
        rb2d.linearVelocity = Vector2.up * launchSpeed;
    }

    void CollideWithRacket(Collider2D racket)
    {
        // Subtract racket position from ball position
        // This gives X relative to centre of paddle
        float relativeXofBall = this.transform.position.x - racket.transform.position.x;

        float boundWidth = racket.bounds.size.x;
        float boundWidthThird = boundWidth / 3;

        bool isCentred = Mathf.Abs(relativeXofBall) < boundWidthThird / 2;
        if (isCentred)
        {
            // nothing?
        }
        else // in third on either side
        {
            // if negative == left side
            if (relativeXofBall < 0)
            {
                Vector2 direction = rb2d.linearVelocity.normalized + Vector2.left;
                direction.Normalize();
                rb2d.linearVelocity = direction * rb2d.linearVelocity.magnitude;
            }

            // if negative == right side
            if (relativeXofBall > 0)
            {
                Vector2 direction = rb2d.linearVelocity.normalized + Vector2.right;
                direction.Normalize();
                rb2d.linearVelocity = direction * rb2d.linearVelocity.magnitude;
            }
        }
    }
}
