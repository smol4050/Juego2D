using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Player : MonoBehaviour
{
    float horizontalInput;
    public float speed = 1;
    private Rigidbody2D rigidBody;
    private Animator animator;
    public Transform groundCheck;
    public LayerMask groundLayer;
    private bool enElPiso;
    public float jumpForce = 1f;
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        movimiento();
        enElPiso = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);
        Salto();
    }

    private void movimiento(){
        horizontalInput = Input.GetAxis("Horizontal");
        Vector2 movimiento = new Vector2(speed*horizontalInput, 0f);
        animator.SetBool("isRunning", true);
        if(horizontalInput > 0)
        {   
            transform.localScale = new Vector3(1,1,1);
            rigidBody.velocity = new Vector2(movimiento.x, rigidBody.velocity.y);
        }else if(horizontalInput < 0)
        {
            transform.localScale = new Vector3(-1,1,1);
            rigidBody.velocity = new Vector2(movimiento.x, rigidBody.velocity.y);
        }else
        {
            animator.SetBool("isRunning", false);
        }
    }

    private void Salto()
    {
        if (Input.GetKeyDown("space") && enElPiso)
        {
            rigidBody.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            animator.SetTrigger("jump");
        }
    }
}
