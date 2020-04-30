using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    public AIDestinationSetter aiMvScript;
    public AIPath aiPath;
    //public Transform detectPointAtk;
    //public LayerMask playerLayer;
    //public float detectRangeAtk;

    // Start is called before the first frame update
    /*void Start()
    {
        
    }*/

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("isMoving", aiMvScript.getDetected());
        if (Detect()) Attack();
        if (aiPath.desiredVelocity.x < -0.9f)
        {
            Debug.Log("flip");
            flip(Mathf.Abs(transform.localScale.x));
        } else if (aiPath.desiredVelocity.x > 0.9f)
        {
            flip(-Mathf.Abs(transform.localScale.x));
        }
    }

    void flip(float scale)
    {
        transform.localScale = new Vector3(scale, transform.localScale.y, transform.localScale.z);
    }

    protected override void Die()
    {
        base.Die();
        transform.Translate(0, 0.22f, 0);
        GetComponent<Collider2D>().enabled = false;
        aiMvScript.enabled = false;
        this.enabled = false;
    }

    private bool Detect()
    {
        //detect
        Collider2D detection = Physics2D.OverlapCircle(attackPoint.position, attackRange, enemyLayers);

        if (detection == null) return false;
        else
        {
            Debug.Log("Found Atk" + detection);
            return true;
        }
    }
}
