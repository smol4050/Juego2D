using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Player : MonoBehaviour
{
    float horizontalInput;
    public float speed = 1;
    private Rigidbody2D rigidBody;
    private Animator animator;
    public LayerMask groundLayer;
    private bool enElPiso = false;
    public float jumpForce = 1f;
    private bool mirandoDerecha;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemysLayer;
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {   
        attack();
        movimiento();
        gestionarOrientacion();
        Salto();
    }

    private void movimiento(){
        animator.SetFloat("xVelocity", Mathf.Abs(rigidBody.velocity.x));
        animator.SetFloat("yVelocity", rigidBody.velocity.y);
        horizontalInput = Input.GetAxis("Horizontal");
        Vector2 movimiento = new Vector2(speed*horizontalInput, 0f);
        if(horizontalInput != 0)
        {  
            rigidBody.velocity = new Vector2(movimiento.x, rigidBody.velocity.y);
        }
        else{
            if(enElPiso)
            {
                rigidBody.velocity = new Vector2(0, rigidBody.velocity.y);
            }
        }
        
    }
    private void gestionarOrientacion(){
        if((mirandoDerecha && horizontalInput > 0) || (!mirandoDerecha && horizontalInput < 0)){
            mirandoDerecha = !mirandoDerecha;
            transform.localScale = new Vector2(-transform.localScale.x, transform.lossyScale.y);
        }
    }
    private void Salto()
    {
        if (Input.GetKeyDown("space") && enElPiso)
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpForce);
            enElPiso = false;
            animator.SetBool("isJumping", !enElPiso);
        }
    }
 
    private void attack()
    {
        if (Input.GetKeyDown("f"))
        {
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemysLayer);// Detecta enemigos en el rango de ataque
            animator.SetBool("isAttacking", true); // Cambia el estado del animator a "isAttacking"

            foreach (Collider2D enemy in hitEnemies) //recorre cada objeto de la lista de enemigs  hitEnemies.
            {
                Debug.Log("se detectó " + enemy.gameObject.name); // nos da su nombre  
            }
        }else{
            animator.SetBool("isAttacking", false);
            if(enElPiso){
                animator.SetBool("isJumping", !enElPiso);
            }else{
                animator.SetBool("isJumping", enElPiso);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        enElPiso = true;
        animator.SetBool("isJumping", !enElPiso);
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return; // Si no hay un attackPoint, no dibuja nada
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange); // Dibuja un círculo rojo en el punto de ataque
    }
}
