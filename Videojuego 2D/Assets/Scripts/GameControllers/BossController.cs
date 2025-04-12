using System.Collections;
using UnityEngine;

public class BossController : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb;
    public Transform player;
    public GameObject spellPrefab;
    public GameObject energyOrbPrefab;

    public float moveSpeed = 1f;
    public float attackRange = 2f;
    public float detectionRange = 10f;
    public int maxHealth = 1000;
    public BossHealthBarUI healthBarUI;
    public float dashSpeed = 3f;
    public float dashDuration = 0.5f;
    public float dashCooldown = 5f;
    public float attackCooldown = 2f;
    public float attackSpeed = 3f;
    public float predictionTime = 1f;

    public GameObject orbPrefab;
    private bool isRetreating = false;
    public float spellCooldown = 4f;
    private bool canCastSpell = true;
    private int currentHealth;
    private bool isDead = false;
    private bool isInvulnerable = false;
    private bool isDashing = false;
    private bool canAttack = true;
    private bool canDash = true;
    private GameObject attackHitbox;

    private float orbCooldown = 15f;
    private bool canUseOrbAttack = true;

    [SerializeField] SonidoBoss sonidoBoss;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        attackHitbox = transform.Find("AttackHitbox").gameObject;
        attackHitbox.SetActive(false);

        currentHealth = maxHealth;
        healthBarUI.SetMaxHealth(maxHealth);
        healthBarUI.Hide();

        sonidoBoss.IniciarSonidosRandom(2f);

        StartCoroutine(OrbAttackRoutine());
    }

    private void Update()
    {
        if (isDead) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (currentHealth < maxHealth * 0.5f)
        {
            isRetreating = true;
        }

        if (isRetreating)
        {
            RetreatFromPlayer();
            return;
        }

        switch (currentState)
        {
            case BossState.Idle:
                if (distanceToPlayer <= detectionRange)
                {
                    currentState = BossState.Chasing;
                }
                break;

            case BossState.Chasing:
                if (distanceToPlayer <= attackRange)
                {
                    currentState = BossState.Attacking;
                }
                else if (canCastSpell)
                {
                    currentState = BossState.Casting;
                }
                else if (canDash)
                {
                    currentState = BossState.Dashing;
                }
                else
                {
                    ChasePlayer();
                }
                break;

            case BossState.Attacking:
                if (canAttack)
                {
                    Attack();
                }
                currentState = BossState.Chasing;
                break;

            case BossState.Dashing:
                StartCoroutine(Dash((player.position - transform.position).normalized));
                currentState = BossState.Chasing;
                break;

            case BossState.Casting:
                StartCoroutine(CastSpell());
                currentState = BossState.Chasing;
                break;
        }
    }

    private enum BossState
    {
        Idle,
        Chasing,
        Attacking,
        Dashing,
        Casting,
        Retreating
    }

    private BossState currentState = BossState.Idle;

    private void ChasePlayer()
    {
        animator.SetBool("isWalking", true);
        Vector2 direction = (player.position - transform.position).normalized;
        rb.velocity = new Vector2(direction.x * moveSpeed, rb.velocity.y);
    }

    private void Idle()
    {
        animator.SetBool("isWalking", false);
        rb.velocity = Vector2.zero;
    }

    private void Attack()
    {
        if (canAttack)
        {
            sonidoBoss.selectAudioAtack();
            animator.SetBool("isWalking", false);
            animator.SetTrigger("isAttacking");
            canAttack = false;
            Invoke(nameof(ResetAttack), attackCooldown);
        }
    }

    private void ResetAttack()
    {
        canAttack = true;
    }

    private IEnumerator Dash(Vector2 direction)
    {
        if (!canDash || isDashing)
            yield break;

        isDashing = true;
        canDash = false;
        SetInvulnerable(true);
        animator.SetTrigger("isDashing");

        float dashTime = 0.5f;
        float dashDistance = 6f;
        float elapsedTime = 0f;
        Vector2 startPosition = rb.position;
        Vector2 targetPosition = startPosition + direction.normalized * dashDistance;

        while (elapsedTime < dashTime)
        {
            rb.MovePosition(Vector2.Lerp(startPosition, targetPosition, elapsedTime / dashTime));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        rb.MovePosition(targetPosition);
        SetInvulnerable(false);
        isDashing = false;

        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }

    public void TakeDamage(int damage)
    {
        if (isInvulnerable || isDead)
            return;

        currentHealth -= damage;
        animator.SetTrigger("isHurt");
        healthBarUI.SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (isDead) return;
        rb.velocity = Vector2.zero;
        sonidoBoss.DetenerSonidosRandom();
        isDead = true;
        animator.SetTrigger("Die");

        StartCoroutine(HandleDeath());
    }

    private IEnumerator HandleDeath()
    {
        yield return new WaitForSeconds(3f);

        Collider2D[] colliders = GetComponents<Collider2D>();
        foreach (var collider in colliders)
        {
            collider.enabled = false;
        }

        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
    }

    private void RetreatFromPlayer()
    {
        animator.SetBool("isWalking", true);
        Vector2 retreatDirection = (transform.position - player.position).normalized;
        rb.velocity = new Vector2(retreatDirection.x * moveSpeed, rb.velocity.y);
        Vector2 playerFuturePosition = (Vector2)player.position + player.GetComponent<Rigidbody2D>().velocity * predictionTime;

        if (canAttack)
        {
            AttackTowards(playerFuturePosition);
        }
    }

    private void AttackTowards(Vector2 targetPosition)
    {
        if (canAttack)
        {
            animator.SetBool("isWalking", false);
            animator.SetTrigger("isAttacking");
            canAttack = false;

            Vector2 attackDirection = (targetPosition - (Vector2)transform.position).normalized;
            GameObject attackInstance = Instantiate(spellPrefab, transform.position, Quaternion.identity);
            attackInstance.GetComponent<Rigidbody2D>().velocity = attackDirection * attackSpeed;

            Invoke(nameof(ResetAttack), attackCooldown);
        }
    }

    private IEnumerator CastSpell()
    {
        if (!canCastSpell) yield break;

        canCastSpell = false;
        animator.SetTrigger("isCasting");

        
        float castAnimationDuration = 1.5f;
        yield return new WaitForSeconds(castAnimationDuration);

        
        int attackChoice = Random.Range(0, 2);

        if (attackChoice == 0)
        {
            Vector3 spellPosition = new Vector3(player.position.x, -1.25f, player.position.z);
            GameObject spellInstance = Instantiate(spellPrefab, spellPosition, Quaternion.identity);

            Animator spellAnimator = spellInstance.GetComponent<Animator>();
            if (spellAnimator != null)
            {
                yield return new WaitForSeconds(spellAnimator.GetCurrentAnimatorStateInfo(0).length);
            }

            Destroy(spellInstance);
        }
        else
        {
            Vector3 orbPosition = new Vector3(player.position.x, -1.25f, player.position.z);
            GameObject orbInstance = Instantiate(orbPrefab, orbPosition, Quaternion.identity);

            
        }

        yield return new WaitForSeconds(spellCooldown);
        canCastSpell = true;
    }


    public void SetInvulnerable(bool state)
    {
        isInvulnerable = state;
    }

    public void EnableHitbox()
    {
        attackHitbox.SetActive(true);
    }

    public void DisableHitbox()
    {
        attackHitbox.SetActive(false);
    }

    public void Appear()
    {
        healthBarUI.Show();
    }

    
    private IEnumerator OrbAttackRoutine()
    {
        while (!isDead)
        {
            yield return new WaitForSeconds(orbCooldown);
            StartCoroutine(SpawnAndRotateOrbs());
        }
    }

    private IEnumerator SpawnAndRotateOrbs()
    {
        float duration = 8f;
        float speed = 180f;
        float radius = 2f;

        Vector3[] directions = new Vector3[]
        {
            Vector3.up,
            Vector3.down,
            Vector3.left,
            Vector3.right
        };

        GameObject[] orbs = new GameObject[4];
        for (int i = 0; i < 4; i++)
        {
            Vector3 offset = directions[i] * radius;
            orbs[i] = Instantiate(energyOrbPrefab, transform.position + offset, Quaternion.identity);
            orbs[i].transform.SetParent(transform);
        }

        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float angle = speed * elapsed;

            for (int i = 0; i < orbs.Length; i++)
            {
                if (orbs[i] != null)
                {
                    float rad = (angle + i * 90f) * Mathf.Deg2Rad;
                    float x = Mathf.Cos(rad) * radius;
                    float y = Mathf.Sin(rad) * radius;
                    orbs[i].transform.localPosition = new Vector3(x, y, 0f);
                }
            }

            yield return null;
        }

        foreach (var orb in orbs)
        {
            if (orb != null) Destroy(orb);
        }
    }
}
