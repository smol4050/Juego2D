using UnityEngine;

public class Spell : MonoBehaviour
{
    public float speed = 5f;
    public int damage = 20;
    private Vector2 target;

    public void Initialize(Vector2 targetPosition)
    {
        target = targetPosition;
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
        if ((Vector2)transform.position == target)
        {
            Destroy(gameObject);
        }
    }

    /*void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Player>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }*/
}

