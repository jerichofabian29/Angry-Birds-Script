using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class shoot : MonoBehaviour
{
    public Vector2 currentMousePosition;
    public Vector2 startingPosition;

    public birdSpawner birdSpawner;

    private Vector2 shotPoint;
    private Vector2 finalPosition;

    LineRenderer lineRen;
    Rigidbody2D rb;
    public Animator anim;

    public LineRenderer line1;
    public LineRenderer line2;
    public TrailRenderer trail;
    new CircleCollider2D collider;
    public GameObject guideDot;

    public float proximityLimit = 1f;
    public float forceMultiplier = 10f;
    public float guideInterval = 0.1f;

    bool isBirdFlying = false;
    public int guideLength = 25;

    [SerializeField] List <GameObject> guideDots;

    [SerializeField] AudioSource stretchAudio; 
    [SerializeField] AudioSource release;

    void Start()
    {
        collider = GetComponent<CircleCollider2D>();
        birdSpawner = GameObject.Find("Slingshot").GetComponent<birdSpawner>();
        rb = GetComponent<Rigidbody2D>();
        stretchAudio = GetComponent<AudioSource>();
        line1 = GameObject.Find("Line 1").GetComponent<LineRenderer>();
        line2 = GameObject.Find("Line 2").GetComponent<LineRenderer>();
        trail = GetComponent<TrailRenderer>();
       
        trail.enabled = false;

        shotPoint = GameObject.Find("Shotpoint").gameObject.transform.position; 

        
        for (int i = 0; i < guideLength; i++)
        {
            GameObject temp = Instantiate(guideDot);
            guideDots.Add(temp);
        }

        if(gameObject.CompareTag("Big Bird"))
        {
            rb.mass = 0.1f;
        }
    }

    void Update()
    {
        currentMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        finalPosition = transform.position;


        if (gameObject.CompareTag("Green Bird"))
        {
            if (rb.linearVelocity.magnitude > 0.01f && Input.GetKeyDown(KeyCode.Space))
            {
                gameObject.tag = "Spinning Bird";
                anim.Play("boomerang_activated");
                // Boomerang Physics (Corrected Falling Arc and Spin effect)

                // Get the current linear velocity
                Vector2 currentVelocity = rb.linearVelocity;

                // Reverse the horizontal (X) component, and reduce its speed. This drives the bird back toward the slingshot area.  
                float newXVelocity = -currentVelocity.x * 0.9f; // Use a factor (like 0.7f) for horizontal return speed

                // Kill the vertical (Y) component, forcing a downward arc immediately
                float newYVelocity = -2f; // Start with a small negative or downward push

                // Apply the new velocity
                rb.linearVelocity = new Vector2(newXVelocity, newYVelocity);

                // Apply a rapid spin (Angular Velocity)
                float spinForce = 1080f;
                rb.angularVelocity = spinForce;
            }
        }
    }

    private void OnMouseDown()
    {
        startingPosition = shotPoint;
        stretchAudio.Play(); 
    }

    private void OnMouseDrag()
    {
        if(isBirdFlying == false)
        {
            StartCoroutine(activateCollider());
            if (Vector2.Distance(currentMousePosition, startingPosition) < 1.5f)
            {
                transform.position = currentMousePosition;
            }
            if (Vector2.Distance(transform.position, shotPoint) > 0.5f)
            {
                Vector2 direction = startingPosition - finalPosition;

                for (int i = 0; i < guideDots.Count; i++)
                {
                    guideDots[i].transform.position = trajectoryPoint(i * guideInterval, direction);
                }
            }
            else
            {
                for (int i = 0; i < guideDots.Count; i++)
                {
                    guideDots[i].transform.position = new Vector2(100, 100); // 
                }
            }

            line1.SetPositions(new Vector3[] { line1.gameObject.transform.position, transform.position });
            line2.SetPositions(new Vector3[] { line2.gameObject.transform.position, transform.position });
        }
    }

    private void OnMouseUp()
    {
        collider.enabled = true;
        if (gameObject.CompareTag("Big Bird"))
        {
            rb.mass = 1f;
        }
        finalPosition = transform.position; 

        Vector2 direction = startingPosition - finalPosition;
        rb.linearVelocity = direction * forceMultiplier;
        rb.gravityScale = 1;

        // remove the guide dots upon relese
        for (int i = 0; i < guideDots.Count; i++)
        {
            guideDots[i].transform.position = new Vector2(100, 100); // 
        }

        if (Vector2.Distance(transform.position, shotPoint) < 0.5f)
        {
            StartCoroutine(returnBall());
            collider.enabled = false;
        }
        else
        {
            trail.enabled = true;
            isBirdFlying = true;
        }
            release.Play();

        birdSpawner.spawnBird();
    }

    

    Vector2 trajectoryPoint(float timeInterval, Vector2 direction)
    {
        // get the finalPosition instead of current position to avoid the dots from still moving even when the distance for dragging is reached
        Vector2 dotPoint = finalPosition + (direction * forceMultiplier * timeInterval) + 0.5f * Physics2D.gravity * (timeInterval * timeInterval);
        return dotPoint;
    }

   private IEnumerator returnBall() 
   {
        yield return new WaitForSeconds(0f);
        rb.gravityScale = 0;
        rb.angularVelocity = 0;
        rb.linearVelocity = Vector2.zero;
        transform.position = startingPosition;
    }

    private IEnumerator activateCollider()
    {
        yield return new WaitForSeconds(1.5f);
        collider.enabled = true;
    }
}
