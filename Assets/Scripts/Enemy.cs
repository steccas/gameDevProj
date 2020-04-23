using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    // Start is called before the first frame update
    /*void Start()
    {
        
    }*/

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void Die()
    {
        base.Die();
        transform.Translate(0, 0.15f, 0);
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
    }
}
