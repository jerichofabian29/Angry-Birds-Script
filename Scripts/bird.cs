using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class bird : MonoBehaviour
{

    birdSpawner birdSpawner;
    Rigidbody2D rb;
    public Animator anim;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        birdSpawner = GameObject.Find("Slingshot").GetComponent<birdSpawner>();
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x >= 10 || (transform.position.x <= -8.5f && transform.position.x <= -10))
        {
            gameObject.tag = "Untagged";
            birdSpawner.spawnBird();
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Pig King" ||
            collision.gameObject.CompareTag("Normal Pig") ||
            collision.gameObject.CompareTag("Obstacle") ||
            collision.gameObject.CompareTag("Surface"))
        {

            StartCoroutine(killBird());
        }

        if(gameObject.CompareTag("Spinning Bird"))
        {
            anim.Play("collided");
        }
    }

    private IEnumerator killBird()
    {
        Vector2 temp = transform.position;
        yield return new WaitForSeconds(5f);
        gameObject.tag = "Untagged";
        birdSpawner.spawnBird();
        Destroy(gameObject);

        //if (Vector2.Distance(temp, transform.position) < 0.1f)
        //{
        //    gameObject.tag = "Untagged";
        //    birdSpawner.spawnBird();
        //    Destroy(gameObject);
        //}

    }
}
