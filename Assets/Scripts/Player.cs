using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{

    public PlayerController controller;

    public float runspeed = 40f;

    float horizontalMove = 0f;
    bool jump = false;

    public float attackRate = 2f;
    float nextAttackTime = 0f;

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runspeed;

        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        if (Input.GetButton("Jump"))
        {
            jump = true;
            animator.SetBool("isJumping", true);
        }

        if(Time.time >= nextAttackTime)
        {
            if (Input.GetButton("Fire1"))
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
                Debug.Log("attacking");
            }
        }
    }

    private void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
        jump = false;

    }

    public void OnLanding()
    {
        animator.SetBool("isJumping", false);
    }

    protected override void Die()
    {
        base.Die();
        currentHealth = maxHealth;
        Invoke("Respawn", 0.5f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Respawn"))
        {
            Debug.Log("Player fell");
            Die();
        }
    }
}
