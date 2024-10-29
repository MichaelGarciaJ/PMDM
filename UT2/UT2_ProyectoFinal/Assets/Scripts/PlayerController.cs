using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    Transform transform;
    public float horizontalVelocity = 0.06f;
    public float verticalVelocity = 0.04f;
    
    void Start()
    {
        transform = GetComponent<Transform>();
    }

   
    void Update()
    {
        Vector2 position = transform.position;
        position.x = position.x + Input.GetAxis("Horizontal") * horizontalVelocity;
        position.y = position.y + Input.GetAxis("Vertical") *  verticalVelocity;
        transform.position = position;
    }
}
