﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public Animator animator;
    public Transform attackPoint;
    public LayerMask enemyLayers;

    public float attackRange = 0.5f;
    public int maxHealth = 100;
    public int damage = 20;

    public float attackRate = 2f;
    float nextAttackTime = 0f;

    protected int currentHealth;

    protected Vector3 respawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        respawnPoint = transform.position;
    }

    protected void Attack()
    {
        float time = Time.time;
        if (time >= nextAttackTime)
        {       
            //anim
            animator.SetTrigger("isAttacking");

            //detect
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

            //damage
            foreach (Collider2D enemy in hitEnemies)
            {
                Debug.Log("Hit " + enemy.name);
                enemy.GetComponent<Character>().TakeDamage(damage);
                //Debug.Log(currentHealth);
            }
            nextAttackTime = Time.time + 1f / attackRate;
            Debug.Log("attacking");
        } else Debug.Log("noAtkTime " + time + " >= " + nextAttackTime);
    }

    protected virtual void TakeDamage(int damage)
    {
        currentHealth -= damage;
        animator.SetTrigger("isHurt");
        if (currentHealth <= 0) { Die(); }
    }

    protected virtual void Die()
    {
        animator.SetTrigger("isDying");
        Debug.Log("dead");
    }

    protected void Respawn() {
        transform.position = respawnPoint;
    }

    protected void Respawn(Vector3 respVector)
    {
        transform.position = respVector;
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}