using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : Character
{
    public PlayerController controller;

    public float runspeed = 40f;

    float horizontalMove = 0f;
    bool jump = false;

    bool isBlocking = false;
    public HealthBar healthBar;

    public int trapDamage = 10;

    bool isFallen = false;
    bool canTakeDamage = true;

    public void AddHealth(int health)
    {
        if (currentHealth < maxHealth) currentHealth += health;
        else if ((currentHealth + health) > maxHealth) currentHealth = maxHealth;
        SyncHealthBar();
    }

    public void AddDamage(int dm)
    {
        damage += dm;
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runspeed;

        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        //if (Mathf.Abs(horizontalMove) == 0f) audioManager.Play("WalkGrass");
        //else if (Mathf.Abs(horizontalMove) == 0f) audioManager.Stop("WalkGrass");

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
            animator.SetBool("isJumping", true);
        }

        if (Input.GetButtonDown("Fire1"))
        {
            Attack();
        }

        if (Input.GetButtonDown("Fire2"))
        {
            animator.SetTrigger("isBlocking");
            audioManager.Play("SaberRise");
        }

        if (Input.GetButton("Fire2"))
        {
            isBlocking = true;
        }
        if (Input.GetButtonUp("Fire2"))
        {
            isBlocking = false;
            animator.SetTrigger("stopBlocking");
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
        audioManager.Play("Landing");
        controller.CreateDust();
    }

    protected override void Die()
    {
        base.Die();
        canTakeDamage = false;
        animator.SetBool("isDead", true);
        transform.Translate(0, -1f, 0);
        //currentHealth = maxHealth;
        //Invoke("Respawn", 3.0f);
        if (isFallen)
        {
            Respawn(0.5f);
            isFallen = false;
            Debug.Log("FallRespawn");
        }
        else
        {
            Respawn(3.0f);
            Debug.Log("NormalRespawn");
        }
        this.enabled = false;
    }

    protected override void Respawn(float time)
    {
        //animator.SetTrigger("isDying");
        base.Respawn(time);
        Invoke("Reset", time-0.2f);
        //healthBar.SetHealth(maxHealth);
    }

    protected override void TakeDamage(int damage)
    {
        if (canTakeDamage)
        {
            if (isBlocking == false)
            {
                base.TakeDamage(damage);
                SyncHealthBar();
                audioManager.Play("Hit");
            }
            else if (isBlocking == true) { audioManager.Play("SaberBlk"); }
        }  
    }

    private void SyncHealthBar()
    {
        healthBar.SetHealth(currentHealth);
    }

    private void Reset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collision");
        if (collision.CompareTag("Respawn"))
        {
            Debug.Log("Player fell");
            isFallen = true;
            Die();
        }
        else if (collision.CompareTag("Trap"))
        {
            Debug.Log("Player hit Spikes");
            TakeDamage(trapDamage);
        }
    }
}
