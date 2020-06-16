using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : Character
{
    public PlayerController controller;

    public float runspeed = 35f;

    float horizontalMove = 0f;
    bool jump = false;
    bool isLanded = true;

    bool isBlocking = false;
    public HealthBar healthBar;

    public int trapDamage = 10;

    bool isFallen = false;
    bool canTakeDamage = true;

    bool inCutscene = false;

    public GameObject intro;
    public GameObject ending;
    public Princess princess;

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

    protected override void Start()
    {
        base.Start();
        StartCoroutine(StartUp());
    }

    private IEnumerator StartUp()
    {
        inCutscene = true;
        //horizontalMove = 0f;
        this.enabled = false;
        controller.enabled = false;
        intro.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        intro.SetActive(false);
        controller.enabled = true;
        this.enabled = true;
        inCutscene = false;
    }

    private IEnumerator Ending()
    {
        ending.SetActive(true);
        princess.Hug();
        yield return new WaitForSeconds(1.0f);
        inCutscene = true;
        ending.SetActive(false);
        animator.SetFloat("Speed", 1);
        princess.Move();
        horizontalMove = 17f;
        yield return new WaitForSeconds(16.0f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    // Update is called once per frame
    void Update()
    {
        if (inCutscene == false) 
        {
            if (!isBlocking)
            {
                horizontalMove = Input.GetAxisRaw("Horizontal") * runspeed;

                animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

                if (Input.GetButtonDown("Jump"))
                {
                    jump = true;
                    isLanded = false;
                    animator.SetBool("isJumping", true);
                }

                if (Input.GetButtonDown("Fire1"))
                {
                    Attack();
                }
            }
            
            if (horizontalMove == 0f && isLanded)
            {
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
        }
    }

    private void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
        jump = false;
    }

    public void OnLanding()
    {
        Debug.Log("Land");
        animator.SetBool("isJumping", false);
        isLanded = true;
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
        else if (collision.CompareTag("Finish"))
        {
            StartCoroutine(Ending());
        }
    }
}
