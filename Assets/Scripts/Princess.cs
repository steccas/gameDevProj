using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Princess : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator animator;

    private bool m_FacingRight = true;  // For determining which way the player is currently facing.
    private Vector3 m_Velocity = Vector3.zero;
    private Rigidbody2D m_Rigidbody2D;

    private float m_MovementSmoothing = .05f;

    private float speed;

    void Start()
    {
        speed = 300f;
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        this.enabled = false;
        this.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        
    }

    public void Move() 
    {
        //controller.Move(, false, false);
        animator.SetFloat("Speed", 1);

        Vector3 targetVelocity = new Vector2((speed * Time.fixedDeltaTime) * 10f, m_Rigidbody2D.velocity.y);
        // And then smoothing it out and applying it to the character
        m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

        // If the input is moving the player right and the player is facing left...
        if (speed > 0 && !m_FacingRight)
        {
            // ... flip the player.
            Flip();
        }
        // Otherwise if the input is moving the player left and the player is facing right...
        else if (speed < 0 && m_FacingRight)
        {
            // ... flip the player.
            Flip();
        }
    }

    public void Hug()
    {
        animator.SetTrigger("Hug");
    }

    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_FacingRight = !m_FacingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
