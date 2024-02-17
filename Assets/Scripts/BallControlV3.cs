using UnityEngine;

public class BallControllerV3 : MonoBehaviour
{
    public float jumpForce = 10f; // bounce
    private Rigidbody2D rb;
    private Vector2 screenBounds;
    private float objectWidth;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Retrieve Rigidbody2D from the ball
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        objectWidth = GetComponent<SpriteRenderer>().bounds.extents.x;
    }

    void Update()
    {
        // Check if screen has been tapped
        if (Input.GetMouseButtonDown(0) || Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Jump();
        }

        // Check whether the ball has reached the screen bounds
        if (transform.position.x + objectWidth >= screenBounds.x || transform.position.x - objectWidth <= -screenBounds.x)
        {
            // Changing the direction of movement to bounce off the edge
            rb.velocity = new Vector2(-rb.velocity.x, rb.velocity.y);
        }
    }

    void Jump()
    {
        rb.velocity = Vector2.zero;
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }
}
