using UnityEngine;
using UnityEngine.InputSystem;

public class obstacles : MonoBehaviour
{
    Rigidbody2D rb;
    int health = 2;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rb.linearVelocity == Vector2.zero)
        {
            gameObject.tag = "Obstacle";
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Red Bird") ||
            collision.gameObject.CompareTag("Big Bird") ||
            collision.gameObject.CompareTag("Green Bird")||
            collision.gameObject.CompareTag("Spinning Bird"))
        {
            
            health--;
            if(health <= 0)
            {
                Destroy(gameObject);
            }
            if (rb.linearVelocity != Vector2.zero)
            {
                gameObject.tag = "Moving Obstacle";
            }
        }
    }

}
