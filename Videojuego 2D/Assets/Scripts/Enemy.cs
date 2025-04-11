using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public abstract class Enemy : MonoBehaviour, IDamage
{
    [SerializeField] protected int enemyMaxHealth = 100;
    [SerializeField] protected int enemyCurrentHealth;

    protected virtual void Start()
    {
        enemyCurrentHealth = enemyMaxHealth;
    }

    public virtual void TakeDamage(int damage)
    {
        enemyCurrentHealth -= damage;
        if (enemyCurrentHealth <= 0)
        {
            Die();
        }
    }
    
    protected virtual void Die(){
        Debug.Log("Enemy died!");   
        gameObject.SetActive(false);
    }
    
    void Update()
    {
        
    }
}
