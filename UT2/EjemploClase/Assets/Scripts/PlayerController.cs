using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform playerTransfor;
    Transform transform;
    Rigidbody2D rigidbody2D;
    public float horizontalVelocity = 5;
    public float verticalVelocity = 5;
    public float jumpForce = 5;
    private bool isGrounded = false;
    public float rayLength = 5;
    public Collider2D feetCollider;
    public LayerMask groundLayer;
    public float velocity = 10;
    private bool onFloor = true;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        transform = GetComponent<Transform>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

    }


    // void Update()
    // {
    //     float inputMovimiento = Input.GetAxis("Horizontal");
    //     rigidbody2D.velocity = new Vector2(inputMovimiento * velocity, rigidbody2D.velocity.y);

    //     RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, rayLength, groundLayer);

    //     if (hit.collider != null && Input.GetKeyDown(KeyCode.Space))
    //     {
    //         rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, jumpForce);
    //     }
    // }

    void OnCollisionEnter2D(Collision2D collision)
    {

        Debug.Log("Colisión detectado con: " + collision.gameObject.name);

        if (collision.gameObject.name == "Square23")
        {
            Physics2D.gravity = new Vector2(Physics2D.gravity.x * 2, Physics2D.gravity.y * 2);
        }

        if (collision.gameObject.CompareTag("Suelo"))
            onFloor = true;

    }


    //   void Update()
    //     {
    //         float inputMovimiento = Input.GetAxis("Horizontal");
    //         // rigidbody2D.velocity = new Vector2(inputMovimiento * velocity, rigidbody2D.velocity.y);

    //         isGrounded = feetCollider.IsTouchingLayers(groundLayer);

    //         if(isGrounded && Input.GetKeyDown(KeyCode.Space)){
    //             rigidbody2D.velocity = new Vector2(inputMovimiento * velocity, jumpForce);
    //         }
    //     }

    void Update()
    {
        float inputMovimiento = Input.GetAxis("Horizontal");
        // rigidbody2D.velocity = new Vector2(inputMovimiento * horizontalVelocity, rigidbody2D.velocity.y);
        rigidbody2D.velocity = new Vector2(inputMovimiento * velocity, rigidbody2D.velocity.y);

        // transform.position = new Vector3(playerTransfor.position.x,playerTransfor.position.y + );

        if (Input.GetAxis("Jump") != 0 && onFloor)
        {
            rigidbody2D.AddForce(Vector2.up * jumpForce);
            onFloor = false;
        }



        gestionarMovimiento(inputMovimiento);
        gestionarGiro(inputMovimiento);
    }


    // private void gestionarMovimiento(float movimiento)
    // {

    //     rigidbody2D.velocity = new Vector2(movimiento * velocity, rigidbody2D.velocity.y);

    //     if (movimiento != 0)
    //     {

    //         animator.SetBool("enMovimiento", true);
    //     }
    //     else
    //     {
    //         animator.SetBool("enMovimiento", false);
    //     }
    // }

     private void gestionarMovimiento(float movimiento)
    {

        rigidbody2D.velocity = new Vector2(movimiento * velocity, rigidbody2D.velocity.y);
        // animator.SetFloat();

        if (movimiento != 0)
        {

            animator.SetBool("enMovimiento", true);
        }
        else
        {
            animator.SetBool("enMovimiento", false);
        }
    }

    void gestionarGiro(float inputMovimiento)
    {

        if (inputMovimiento > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (inputMovimiento < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

    }

    //   void OnCollisionEnter2D(Collision2D collision)
    // {
    //     if(collision.gameObject.CompareTag("Suelo"))
    //     onFloor = true;

    // }

    // void Update()
    // {
    //     // float inputMovimiento = Input.GetAxis("Horizontal");
    //     rigidbody2D.AddForce(Vector2.right * 10f);
    // }

    // void OnCollisionEnter2D(Collision2D collision)
    // {

    //     Debug.Log("Colisión detectado con: " + collision.gameObject.name);

    //     int veces = 0;

    //     while (veces == 10)
    //     {

    //         if (collision.gameObject.name == "Square")
    //         {
    //             veces++;
    //         }

    //         if (veces == 10)
    //         {
    //             Debug.Log("HAS GANADO");
    //         }

    //     }

    // }

    // void OnTriggerStay(Collider other)
    // {

    //     Debug.Log("Permaneciendo en el trigger: " + other.gameObject.name);
    // }

    // void Update()
    // {
    //     float inputMovimiento = Input.GetAxis("Horizontal");
    //     rigidbody2D.velocity = new Vector2(inputMovimiento * horizontalVelocity, rigidbody2D.velocity.y);
    // }

    // // Update is called once per frame
    // void Update()
    // {
    //     Vector3 position = transform.position;
    //     if (Input.GetKey(KeyCode.D))
    //     position.x = transform.position.x + 0.01f;
    //     if (Input.GetKey(KeyCode.A))
    //     position.x = transform.position.x - 0.01f;
    //     if (Input.GetKey(KeyCode.W))
    //     position.y = transform.position.y + 0.01f;
    //     if (Input.GetKey(KeyCode.S))
    //     position.y = transform.position.y - 0.01f;


    // transform.position = position;
    // }

    // void Update(){

    //     Vector2 position = transform.position;
    //     position.x = position.x + Input.GetAxis("Horizontal") * horizontalVelocity;
    //     position.y = position.y + Input.GetAxis("Vertical") * verticalVelocity;
    //     transform.position = position;

    //     Debug.Log("Horizontal: " + Input.GetAxis("Horizontal"));
    //     Debug.Log("Vertical: " + Input.GetAxis("Vertical"));

    // }


}
