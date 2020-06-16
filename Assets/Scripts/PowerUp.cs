using System.Collections;
using System.Collections.Generic;
//using UnityEditor.MemoryProfiler;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    protected Collider2D playerC;
    protected Player playerObj;

    protected AudioManager audioManager;

    public int value;

    private void Start()
    {
        audioManager = AudioManager.GetInstance();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collision");
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Collision");
            playerC = collision;
            Pickup();
        }
    }

    protected virtual void Pickup()
    {
        playerObj = playerC.GetComponent<Player>();
    }

    protected void Destroyer()
    {
        Destroy(gameObject);
    }
}
