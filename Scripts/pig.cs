using UnityEngine;
using UnityEngine.SceneManagement;

public class pig : MonoBehaviour
{
    public int health = 2;
    public Animator anim;
    public gameManager gm;
    new Collider2D collider;

    int initialHealth;
    void Start()
    {
        gm = GameObject.Find("Game Manager").GetComponent<gameManager>();
        collider = GetComponent<Collider2D>();
        
        initialHealth = health;
    }

    void Update()
    {
        if (transform.position.x >= 10 || (transform.position.x <= -8.5f && transform.position.x <= -10))
        {
            gm.decreasePigCount(1);
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Red Bird") ||
            collision.gameObject.CompareTag("Spinning Bird") ||
            collision.gameObject.CompareTag("Big Bird") ||
            collision.gameObject.CompareTag("Green Bird") || 
            collision.gameObject.CompareTag("Moving Obstacle") && health !>= 1)
        {
            if(collision.gameObject.CompareTag("Big Bird"))
            {
                health -= 2;
            }

            health -= 1;

            if(initialHealth > 2 && health > 1)
            {
                anim.Play("low_HP");
            }
            else
            {
                anim.Play("one_HP");
            }
            if (health <= 0)
            {
                collider.enabled = false;
                gm.decreasePigCount(1);
                anim.Play("death");
                Destroy(gameObject, 0.3f);
            }
        }
    }
}
