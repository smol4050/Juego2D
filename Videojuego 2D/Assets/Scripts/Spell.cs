using UnityEngine;
using System.Collections.Generic;

public class Spell : MonoBehaviour
{
    public int damagePerSecond = 20;
    private Collider2D hitbox;
    private Dictionary<GameObject, float> damageTimers = new Dictionary<GameObject, float>();

    private void Awake()
    {
        hitbox = GetComponent<Collider2D>();
        hitbox.enabled = false;
    }

    private void OnEnable()
    {
        Vector3 newPosition = transform.position;
        newPosition.y = -1.25f;
        transform.position = newPosition;
    }
    public void EnableHitbox()
    {
        hitbox.enabled = true;
    }

    public void DisableHitbox()
    {
        hitbox.enabled = false;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (!damageTimers.ContainsKey(other.gameObject))
            {
                damageTimers[other.gameObject] = 0f;
            }

            damageTimers[other.gameObject] += Time.deltaTime;

            if (damageTimers[other.gameObject] >= 1f)
            {
                var player = other.GetComponent<PlayerController>();
                if (player != null)
                {
                    player.TakeDamage(damagePerSecond);
                }
                damageTimers[other.gameObject] = 0f;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (damageTimers.ContainsKey(other.gameObject))
        {
            damageTimers.Remove(other.gameObject);
        }
    }

    public void DestroySpell()
    {
        Destroy(gameObject);
    }

}
