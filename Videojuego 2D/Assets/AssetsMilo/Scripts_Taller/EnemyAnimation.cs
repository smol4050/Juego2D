using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    private Vector2 previousPosition, newPosition;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        previousPosition = transform.position;
    }

    void Update()
    {
        newPosition = transform.position;
        Vector2 velocity = (newPosition - previousPosition) / Time.deltaTime;
        previousPosition = newPosition;

        animator.SetFloat("Speed", velocity.magnitude);

    }
}

