using System.Collections;
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

    protected int currentHealth;

    protected Vector3 respawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        respawnPoint = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected void Attack()
    {
        //anim
        animator.SetTrigger("isAttacking");

        //detect
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        
        //damage
        foreach(Collider2D enemy in hitEnemies)
        {
            Debug.Log("Hit " + enemy.name);
            enemy.GetComponent<Character>().takeDamage(damage);
            //Debug.Log(currentHealth);
        }
    }

    protected void takeDamage(int damage)
    {
        currentHealth -= damage;

        animator.SetTrigger("isHurt");

        if(currentHealth <= 0) { Die(); }
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