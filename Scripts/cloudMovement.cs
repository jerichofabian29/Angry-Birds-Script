using UnityEngine;

public class cloudMovement : MonoBehaviour
{
    MeshRenderer meshRen;
    float speed = 0.1f;

    void Start()
    {
        meshRen = GetComponent<MeshRenderer>(); // Get the MeshRenderer component attached to this GameObject
    }

    // Update is called once per frame
    void Update()
    {
        meshRen.material.mainTextureOffset += Vector2.right * speed * Time.deltaTime;
    }

}
