using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerController : MonoBehaviour, IDamage
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
    public LayerMask enemyLayers;
    public int attackDamage = 20;
    public float attackCooldown = 1f;
    private float attackTimer = 0f;
    public int PlayermaxHealth = 100;
    public int PlayercurrentHealth;
    public bool Hactivo;
    private bool enElPisoAnterior;


    [SerializeField] controladorSonidoJugador controladorSonidoJugador;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        PlayercurrentHealth = PlayermaxHealth;
    }

    // Update is called once per frame
    void Update()
    {   
        attack();
        movimiento();
        gestionarOrientacion();
        Salto();
        Dead();
        enElPisoAnterior = enElPiso;
    }

    private void movimiento(){
        animator.SetFloat("xVelocity", Mathf.Abs(rigidBody.velocity.x));
        animator.SetFloat("yVelocity", rigidBody.velocity.y);
        horizontalInput = Input.GetAxis("Horizontal");
        Vector2 movimiento = new Vector2(speed*horizontalInput, 0f);
        if(horizontalInput != 0)
        {  
            Hactivo = true;
            rigidBody.velocity = new Vector2(movimiento.x, rigidBody.velocity.y);
            
        }
        else{
            if(enElPiso)
            {
                rigidBody.velocity = new Vector2(0, rigidBody.velocity.y);
            }
            Hactivo = false;
        }

        if (enElPiso && Hactivo)
        {
            if (!controladorSonidoJugador.pasos.isPlaying)
            {
                controladorSonidoJugador.pasos.Play();
            }
        }
        else
        {
            if (controladorSonidoJugador.pasos.isPlaying)
            {
                controladorSonidoJugador.pasos.Stop();
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
            controladorSonidoJugador.soundJump();
        }
        
    }
 
    private void attack()
{
        
        // Si se presiona "f" y se cumplió el cooldown:
        if (Input.GetKeyDown("f") && attackTimer <= 0f)
    {
        controladorSonidoJugador.selectAudioAtack();
        // Resetea el temporizador del cooldown
        attackTimer = attackCooldown;

        // Detecta enemigos en el rango de ataque
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        animator.SetBool("isAttacking", true);

        // Recorre cada enemigo detectado y le aplica daño
        foreach (Collider2D enemy in hitEnemies)
        {
            try
            {
                IDamage damageable = enemy.GetComponent<IDamage>();
                damageable.TakeDamage(attackDamage);
            }
            catch (System.Exception e)
            {
                Debug.Log("Error 1: " + e.Message);
            }
            try{
                enemy.GetComponent<BossController>().TakeDamage(attackDamage);
                Debug.Log("Se detectó " + enemy.gameObject.name);
            }
            catch (System.Exception e)
            {
                Debug.Log("Error 2: " + e.Message);
            }

            
        }
    }
    else
    {
        animator.SetBool("isAttacking", false);

        // Aquí restamos el tiempo transcurrido del cooldown
        if (attackTimer > 0)
        {
            attackTimer -= Time.deltaTime;
        }

        // Actualiza la animación de salto de forma sencilla
        animator.SetBool("isJumping", !enElPiso);
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

    public void TakeDamage(int damage)
    {
        PlayercurrentHealth -= damage;
        animator.SetTrigger("hit");
        controladorSonidoJugador.selectAudioDamageReceived();
        Debug.Log("Player took damage: " + damage + ", current health: " + PlayercurrentHealth);
    }

    private void Dead(){
        if (PlayercurrentHealth <= 0){
            Debug.Log("Player died!");
            animator.SetTrigger("die");
            controladorSonidoJugador.audioSource.Stop();
            controladorSonidoJugador.selectAudioDied();
            


        }
    }
    public void DesacivarJugador()
    {
        Debug.Log("Jugador desactivado tras animación.");
        gameObject.SetActive(false);
    }
}
